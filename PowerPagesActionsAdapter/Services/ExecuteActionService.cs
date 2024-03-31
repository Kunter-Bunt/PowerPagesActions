using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using PowerPagesActionsAdapter.Models;
using PowerPagesActionsAdapter.Plugins;
using PowerPagesCustomApiAdapter.EarlyBounds;
using System;
using System.Collections;
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
        }

        private readonly ITracingService TracingService;
        private readonly DataverseContext DataverseContext;

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

            target.MwO_Operation = ExtractConditionValue(query, MwO_PowerPagesAction.Fields.MwO_Operation);
            target.MwO_Inputs = ExtractConditionValue(query, MwO_PowerPagesAction.Fields.MwO_Inputs);
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

        private string ExtractConditionValue(QueryExpression query, string attributeName)
        {
            var conditions = GetAllConditions(query.Criteria);

            return conditions
                .FirstOrDefault(_ => _.AttributeName == attributeName)
                ?.Values.First() as string;
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

            foreach (var input in inputs)
            {
                TracingService.Trace($"Add Parameter {input.Key}: {input.Value}");
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
    }
}
