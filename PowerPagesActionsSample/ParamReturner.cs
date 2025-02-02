using Microsoft.Xrm.Sdk;
using System;
using System.IdentityModel.Metadata;

namespace PowerPagesActionsSample
{
    /// <summary>
    /// 
    /// </summary>
    public class ParamReturner : PluginBase
    {
        public ParamReturner()
            : base(typeof(ParamReturner))
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

            foreach (var param in context.InputParameters)
            {
                localPluginContext.Trace($"InputParameters: {param.Key} = {Print(param.Value)}");
                context.OutputParameters[param.Key] = param.Value;
            }
        }

        private string Print(object value) => value switch
        {
            int val => val.ToString(),
            float val => val.ToString(),
            decimal val => val.ToString(),
            bool val => val.ToString(),
            string val => val,
            string[] val => string.Join(";", val),
            DateTime val => val.ToString(),
            Guid val => val.ToString(),
            OptionSetValue val => val.Value.ToString(),
            Money val => val.Value.ToString(),
            EntityReference val => $"{val.Id}({val.LogicalName})",
            Entity val => $"{val.Id}({val.LogicalName}, {val.Attributes.Count} Attributes)",
            EntityCollection val => PrintEntityCollection(val),
            _ => $"{value?.GetType().Name}: {value}",
        };

        private string PrintEntityCollection(EntityCollection collection)
        {
            var str = $"{collection.EntityName}";
            foreach (var entity in collection.Entities)
            {
                str += $"\n{entity.Id}: {entity.Attributes.Count} Attributes";
            }
            return str;
        }
    }
}
