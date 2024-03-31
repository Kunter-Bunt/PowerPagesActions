using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PowerPagesActionsAdapter.Services;

namespace PowerPagesActionsAdapter.Plugins
{
    public class ExecuteAction : PluginBase
    {
        public ExecuteAction(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(ExecuteAction))
        {
        }

        private const string QueryName = "Query";

        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            var service = new ExecuteActionService(localPluginContext);
            var context = localPluginContext.PluginExecutionContext;
            if (context.InputParameters.Contains(QueryName))
            {
                localPluginContext.Trace($"Query Type: {context.InputParameters[QueryName].GetType()}");
                QueryExpression query = null;
                if (context.InputParameters[QueryName] is QueryExpression queryExpression)
                    query = queryExpression;
                else if (context.InputParameters[QueryName] is FetchExpression fetchExpression)
                {
                    localPluginContext.Trace($"Converting FetchXML: {fetchExpression.Query}");
                    FetchXmlToQueryExpressionRequest fetchXmlToQueryExpressionRequest = new FetchXmlToQueryExpressionRequest()
                    {
                        FetchXml = fetchExpression.Query
                    };
                    FetchXmlToQueryExpressionResponse fetchXmlToQueryExpressionResponse = (localPluginContext.InitiatingUserService.Execute(fetchXmlToQueryExpressionRequest) as FetchXmlToQueryExpressionResponse);
                    query = fetchXmlToQueryExpressionResponse.Query;
                    localPluginContext.Trace($"Converted FetchXML");
                }
                else
                    return;

                var result = service.ExecuteAction(query);
                if (result != null && result.StatusCode != PowerPagesCustomApiAdapter.EarlyBounds.MwO_PowerPagesAction_StatusCode.Active)
                {
                    EntityCollection collection = context.OutputParameters["BusinessEntityCollection"] as EntityCollection;
                    localPluginContext.Trace($"BusinessEntityCollection Type: {collection?.GetType()}");
                    localPluginContext.Trace($"BusinessEntityCollection old Count: {collection.Entities.Count}({collection.EntityName})");
                    collection.Entities.Clear();
                    collection.Entities.Add(result.ToEntity<Entity>());
                    localPluginContext.Trace($"BusinessEntityCollection new Count: {collection.Entities.Count}({collection.EntityName})");
                }
            }
            else
                localPluginContext.Trace("No query!");
        }
    }
}
