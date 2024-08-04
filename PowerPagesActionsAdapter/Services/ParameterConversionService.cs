using Microsoft.Xrm.Sdk;
using Newtonsoft.Json.Linq;
using PowerPagesActionsAdapter.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerPagesActionsAdapter.Services
{
    public class ParameterConversionService
    {
        public ParameterConversionService(ILocalPluginContext localPluginContext)
        {
            TracingService = localPluginContext.TracingService;
        }

        private readonly ITracingService TracingService;

        public object Convert(object value)
        {
            TracingService.Trace($"ParameterConversionService Input {value.GetType().FullName}: {value}");
            var output = ConvertInternal(value);
            TracingService.Trace($"ParameterConversionService Output {output.GetType().FullName}: {output}");
            return output;
        }

        private object ConvertInternal(object value)
        {
            switch (value)
            {
                case JObject jObject:
                    return ConvertJObject(jObject);
                case JArray jArray:
                    return jArray.Select(_ => _.ToString()).ToArray();
                case long jLong:
                    return (int)jLong;
                case string jString:
                    return ConvertString(jString);
                case JValue jValue:
                    return jValue?.Value.ToString();
                default:
                    return value;
            }
        }

        private object ConvertString(string jString)
        {
            if (jString.ToLower().EndsWith("m") && decimal.TryParse(jString.TrimEnd('m', 'M'), out decimal d))
                return d;
            else if (Guid.TryParse(jString, out var id))
                return id;
            else
                return jString;
        }

        private object ConvertJObject(JObject jObject)
        {
            var money = jObject.GetValue("Money")?.ToObject<decimal>();
            var optionSetValue = jObject.GetValue("OptionSetValue")?.ToObject<int>();
            var entityName = jObject.GetValue("EntityName")?.ToString();
            var logicalName = jObject.GetValue("LogicalName")?.ToString();
            var id = jObject.GetValue("Id")?.ToObject<Guid>();
            if (jObject.ContainsKey("Attributes"))
                return ConvertToEntity(logicalName, id, jObject.GetValue("Attributes")?.ToObject<JObject>());
            else if (!string.IsNullOrEmpty(logicalName) && id != null)
                return new EntityReference(logicalName, id.Value);
            else if (!string.IsNullOrEmpty(entityName))
                return ConvertToEntityCollection(entityName, jObject);
            else if (money != null)
                return new Money(money.Value);
            else if (optionSetValue != null)
                return new OptionSetValue(optionSetValue.Value);
            else
                return jObject;
        }

        private EntityCollection ConvertToEntityCollection(string entityName, JObject jObject)
        {
            var entities = jObject.GetValue("Entities")?.ToObject<JArray>();
            TracingService.Trace($"ConvertToEntityCollection: {entities.Count} Entities");
            var entityList = new List<Entity>();
            foreach (var entity in entities)
            {
                var ent = entity.ToObject<JObject>();
                var logicalName = ent.GetValue("LogicalName")?.ToString();
                var attributes = ent.GetValue("Attributes")?.ToObject<JObject>();
                var id = ent.GetValue("Id")?.ToObject<Guid>();
                entityList.Add(ConvertToEntity(logicalName, id, attributes));
            }
            return new EntityCollection(entityList)
            {
                EntityName = entityName,
            };
        }

        private Entity ConvertToEntity(string logicalName, Guid? id, JObject attributes)
        {
            TracingService.Trace($"ConvertToEntity: {id}({logicalName}; {attributes.Count} Attributes)");
            var ent = new Entity(logicalName);
            foreach (var attribute in attributes)
            {
                var value = Convert(attribute.Value);
                ent[attribute.Key] = value;
            }
            if (id != null)
                ent.Id = id.Value;
            return ent;
        }
    }
}
