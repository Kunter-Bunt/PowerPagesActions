﻿using Microsoft.Xrm.Sdk;
using PowerPagesActionsAdapter.Plugins;
using PowerPagesCustomApiAdapter.EarlyBounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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

        public void ExecuteAction(MwO_PowerPagesAction target)
        {
            try
            {
                ExecuteActionInternal(target);
            }
            catch (Exception ex)
            {
                target.MwO_Outputs = ex.Message; //TODO JSON
            }
        }

        private void ExecuteActionInternal(MwO_PowerPagesAction target)
        {
            TracingService.Trace($"Start ExecuteActionService");

            Guard(target.MwO_Operation, "No Operation given");
            var config = DataverseContext.MwO_PowerPagesActionConfigurationSet
                .Where(_ => _.StateCode == MwO_PowerPagesActionConfiguration_StateCode.Active)
                .Where(_ => _.MwO_Operation == target.MwO_Operation)
                .FirstOrDefault();

            Guard(config, $"No Configuration set for given Operation {target.MwO_Operation}");

            target.MwO_ConfigurationId = config.ToEntityReference();

            switch (config.MwO_ActionTypeCode)
            {
                case MwO_PowerPagesActionType.CustomApi: 
                    ExecuteCustomApi(target, config); 
                    break;
                default:
                    throw new InvalidPluginExecutionException("Invalid action type configured");
            }

            TracingService.Trace($"Done ExecuteActionService");
        }

        private Dictionary<string, object> ExecuteCustomApi(MwO_PowerPagesAction target, MwO_PowerPagesActionConfiguration config)
        {
            TracingService.Trace($"Building {config.MwO_CustomApi} Request");
            var request = new OrganizationRequest(config.MwO_CustomApi);

            Dictionary<string, object> inputs = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(target.MwO_Inputs))
                inputs = JsonSerializer.Deserialize<Dictionary<string, object>>(target.MwO_Inputs);

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

            return outputs;
        }
        

        private void Guard(object tbc, string message)
        {
            if (tbc == null)
                throw new InvalidPluginExecutionException(message);
        }
    }
}
