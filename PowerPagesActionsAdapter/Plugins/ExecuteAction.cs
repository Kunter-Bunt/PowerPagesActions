using Microsoft.Xrm.Sdk;
using PowerPagesActionsAdapter.Services;
using PowerPagesCustomApiAdapter.EarlyBounds;
using System;

namespace PowerPagesActionsAdapter.Plugins
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public class ExecuteAction : PluginBase<MwO_PowerPagesAction>
    {
        public ExecuteAction(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExecuteAction))
        {

        }

        // Entry point for custom business logic execution
        protected override void ExecutePlugin(ILocalPluginContext localPluginContext, MwO_PowerPagesAction target)
        {
            var service = new ExecuteActionService(localPluginContext);
            service.ExecuteAction(target);
        }
    }
}
