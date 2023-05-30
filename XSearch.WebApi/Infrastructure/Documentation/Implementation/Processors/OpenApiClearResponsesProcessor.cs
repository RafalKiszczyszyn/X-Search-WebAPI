//  Copyright © Titian Software Ltd

using System.Reflection;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using XSearch.WebApi.Infrastructure.Documentation.Attributes;

namespace XSearch.WebApi.Infrastructure.Documentation.Implementation.Processors
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