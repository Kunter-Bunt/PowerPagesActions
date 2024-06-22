using Microsoft.Xrm.Sdk;
using System;

namespace PowerPagesActionsSample
{
    /// <summary>
    /// 
    /// </summary>
    public class GreeterBound : PluginBase
    {
        public GreeterBound()
            : base(typeof(GreeterBound))
        {
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;
            var service = localPluginContext.PluginUserService;

            context.InputParameters.TryGetValue("Target", out EntityReference reference);
            localPluginContext.Trace($"reference: {reference?.Id}({reference?.LogicalName})");

            string text;
            if (reference != null)
            {
                var contact = service.Retrieve(reference.LogicalName, reference.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet("fullname"));
                localPluginContext.Trace($"Fullname: {contact["fullname"]}");
                text = $"You are {contact["fullname"]}";
            }
            else
            {
                localPluginContext.Trace($"No Contact");
                text = $"You are nobody";
            }

            context.OutputParameters["Text"] = text;
        }
    }
}
