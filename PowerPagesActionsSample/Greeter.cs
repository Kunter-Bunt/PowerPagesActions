using Microsoft.Xrm.Sdk;
using System;

namespace PowerPagesActionsSample
{
    /// <summary>
    /// 
    /// </summary>
    public class Greeter : PluginBase
    {
        public Greeter()
            : base(typeof(Greeter))
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

            context.InputParameters.TryGetValue("Text", out string text);
            localPluginContext.Trace($"Text: {text}");

            context.InputParameters.TryGetValue("ContactId", out Guid? contactId);
            localPluginContext.Trace($"ContactId: {contactId}");

            context.InputParameters.TryGetValue("ContactReference", out EntityReference contactReference);
            localPluginContext.Trace($"ContactReference: {contactReference?.Id}({contactReference?.LogicalName})");

            if (contactReference != null)
            {
                var contact = service.Retrieve(contactReference.LogicalName, contactReference.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet("fullname"));
                localPluginContext.Trace($"Fullname: {contact["fullname"]}");
                text = text.Replace("{FullName}", contact["fullname"].ToString());
            }
            else
            {
                localPluginContext.Trace($"No Contact");
                text = text.Replace("{FullName}", "Anonymous");
            }

            if (contactId != null)
            {
                localPluginContext.Trace($"Id: {contactId}");
                text = text.Replace("{Id}", contactId.ToString());
            }
            else
            {
                localPluginContext.Trace($"No Id");
                text = text.Replace("{Id}", "not present");
            }

            context.OutputParameters["Text"] = text;
        }
    }
}
