using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using PowerPagesActionsAdapter.Models;
using PowerPagesActionsAdapter.Plugins;
using PowerPagesCustomApiAdapter.EarlyBounds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerPagesActionsAdapter.Services
{
    public class ExecuteActionService
    {
        public ExecuteActionService(ILocalPluginContext localPluginContext)
        {
            TracingService = localPluginContext.TracingService;
            DataverseContext = new DataverseContext(localPluginContext.InitiatingUserService);
            ParameterConversionService = new ParameterConversionService(localPluginContext);
        }

        private readonly ITracingService TracingService;
        private readonly DataverseContext DataverseContext;
        private readonly ParameterConversionService ParameterConversionService;

        public MwO_PowerPagesAction ExecuteAction(QueryExpression query)
        {
            var id = Guid.NewGuid();
            var target = new MwO_PowerPagesAction()
            {
                ["createdon"] = DateTime.UtcNow,
                Id = id,
                MwO_PowerPagesActionId = id,
                StatusCode = MwO_PowerPagesAction_StatusCode.Active
            };
            try
            {
                ExecuteActionInternal(query, target);
            }
            catch (Exception ex)
            {
                TracingService.Trace($"ERROR: {ex.Message}");
                var error = new ErrorResponse(ex.Message);

                target.MwO_Outputs = JsonConvert.SerializeObject(error, Formatting.Indented);
                target.StatusCode = MwO_PowerPagesAction_StatusCode.Error;
            }
            return target;
        }

        private void ExecuteActionInternal(QueryExpression query, MwO_PowerPagesAction target)
        {
            TracingService.Trace($"Start ExecuteActionService");

            if (query == null)
            {
                TracingService.Trace($"No Query passed, skipping");
                return;
            }

            target.MwO_Operation = ExtractConditionValue<string>(query, MwO_PowerPagesAction.Fields.MwO_Operation);
            target.MwO_Inputs = ExtractConditionValue<string>(query, MwO_PowerPagesAction.Fields.MwO_Inputs);
            if (target.MwO_Operation == null)
            {
                TracingService.Trace($"Not valid for Action execution due to missing operation, skipping");
                return;
            }

            var config = DataverseContext.MwO_PowerPagesActionConfigurationSet
                .Where(_ => _.StateCode == MwO_PowerPagesActionConfiguration_StateCode.Active)
                .Where(_ => _.MwO_Operation == target.MwO_Operation)
                .FirstOrDefault();

            Guard(config, $"No Configuration set for given Operation {target.MwO_Operation}");
            TracingService.Trace($"Retrieved Configuration");

            target.MwO_ConfigurationId = config.ToEntityReference();

            GuardAuthorization(query, target, config);

            switch (config.MwO_ActionTypeCode)
            {
                case MwO_PowerPagesActionType.CustomApi:
                    target.MwO_Outputs = ExecuteCustomApi(target, config);
                    break;
                default:
                    throw new InvalidPluginExecutionException("Invalid action type configured");
            }

            target.StatusCode = MwO_PowerPagesAction_StatusCode.Success;
            TracingService.Trace($"Done ExecuteActionService");
        }

        private T ExtractConditionValue<T>(QueryExpression query, string attributeName)
        {
            var conditions = GetAllConditions(query.Criteria);
            var obj = conditions
                .FirstOrDefault(_ => _.AttributeName == attributeName)
                ?.Values.First();
            return obj != null ? (T)obj : default;
        }

        private DataCollection<ConditionExpression> GetAllConditions(FilterExpression input)
        {
            var conditions = input.Conditions;
            foreach (var filter in input.Filters)
                conditions.AddRange(GetAllConditions(filter));
            return conditions;
        }

        private string ExecuteCustomApi(MwO_PowerPagesAction target, MwO_PowerPagesActionConfiguration config)
        {
            TracingService.Trace($"Building {config.MwO_CustomApi} Request");
            var request = new OrganizationRequest(config.MwO_CustomApi);

            Dictionary<string, object> inputs = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(target.MwO_Inputs))
                inputs = JsonConvert.DeserializeObject<Dictionary<string, object>>(target.MwO_Inputs.Trim('%'));

            if (!string.IsNullOrEmpty(config.MwO_ContactGUIdParameter) && target.MwO_ContactId != null)
                inputs[config.MwO_ContactGUIdParameter] = target.MwO_ContactId.Id;
            if (!string.IsNullOrEmpty(config.MwO_ContactReferenceParameter) && target.MwO_ContactId != null)
                inputs[config.MwO_ContactReferenceParameter] = target.MwO_ContactId;

            var convertedInputs = inputs.Select(_ => new KeyValuePair<string, object>(_.Key, ParameterConversionService.Convert(_.Value))).ToList();
            foreach (var input in convertedInputs)
            {
                TracingService.Trace($"Add Parameter {input.Key}: {input.Value} ({input.Value.GetType().Name})");
                request.Parameters[input.Key] = input.Value;
            }

            TracingService.Trace($"Executing {config.MwO_CustomApi} Request");
            var response = DataverseContext.Execute(request);
            TracingService.Trace($"Executed {config.MwO_CustomApi} Request");

            Dictionary<string, object> outputs = new Dictionary<string, object>();

            foreach (var result in response.Results)
            {
                TracingService.Trace($"Add Output {result.Key}: {result.Value}");
                outputs[result.Key] = result.Value;
            }

            return JsonConvert.SerializeObject(outputs, Formatting.Indented);
        }


        private void Guard(object tbc, string message)
        {
            if (tbc == null)
                throw new InvalidPluginExecutionException(message);
        }

        private void GuardAuthorization(QueryExpression query, MwO_PowerPagesAction target, MwO_PowerPagesActionConfiguration config)
        {
            var contactId = ExtractConditionValue<Guid?>(query, MwO_PowerPagesAction.Fields.MwO_ContactId);
            TracingService.Trace($"Called by Contact {contactId}");
            if (contactId != null && contactId != Guid.Empty)
            {
                target.MwO_ContactId = new EntityReference("contact", contactId.Value);
            }
            else if (config.MwO_IsRestrictedToAuthenticated == true || config.MwO_IsRestrictedToWebRoles == true)
                throw new InvalidPluginExecutionException("Action not allowed for anonymous user.");

            if (config.MwO_IsRestrictedToWebRoles == true)
            {
                var configRoles = DataverseContext.MwO_PowerPagesActionConfigurationSet
                    .Where(_ => _.Id == config.Id)
                    .SelectMany(_ => _.MwO_PPactionConfiguration_MsPp_WebRole)
                    .Select(_ => new PowerPageComponent { Id = _.Id })
                    .ToList();
                TracingService.Trace($"Allow Roles {string.Join(";", configRoles.Select(_ => _.Id))}");

                var contactRoles = DataverseContext.ContactSet
                    .Where(_ => _.Id == contactId)
                    .SelectMany(_ => _.PowerPageComponent_MsPp_WebRole_Contact)
                    .Select(_ => new PowerPageComponent { Id = _.Id })
                    .ToList();
                TracingService.Trace($"Caller Roles {string.Join(";", contactRoles.Select(_ => _.Id))}");

                var roleMatch = contactRoles.Any(_ => configRoles.Any(r => r.Id == _.Id));
                TracingService.Trace($"Role Match? {roleMatch}");

                if (!roleMatch)
                    throw new InvalidPluginExecutionException("Action not allowed for the current roles.");
            }
        }
    }
}
