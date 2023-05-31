using System.Reflection;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using XSearch.WebApi.Common.WebInfra.Documentation.Attributes;

namespace XSearch.WebApi.Common.WebInfra.Documentation.Implementation.Processors
{
  internal class OpenApiClearResponsesProcessor : IOperationProcessor
  {
    public bool Process(OperationProcessorContext context)
    {
      if (context.MethodInfo.GetCustomAttributes<OpenApiClearResponsesAttribute>().Any())
      {
        context.OperationDescription.Operation.Responses.Clear();
      }

      return true;
    }
  }
}