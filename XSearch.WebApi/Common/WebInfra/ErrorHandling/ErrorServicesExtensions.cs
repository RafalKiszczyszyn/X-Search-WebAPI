

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using XSearch.WebApi.Common.Errors;
using XSearch.WebApi.Common.WebInfra.ErrorHandling.Filters;
using XSearch.WebApi.Common.WebInfra.ErrorHandling.Translation;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling
{
  internal static class ErrorServicesExtensions
  {
    public static IServiceCollection ConfigureErrorHandling(this IServiceCollection services)
    {
      services.Add(new ServiceDescriptor(
        serviceType: typeof(ExceptionFilter),
        implementationType: typeof(ExceptionFilter),
        lifetime: ServiceLifetime.Scoped));

      services.AddMvc(config =>
        config.Filters.AddService<ExceptionFilter>());

      services.ConfigureInvalidModelResponseFactory();
      services.AddTransient<IHttpStatusCodeDictionary, HttpStatusCodeDictionary>();

      return services;
    }

    
    private static void ConfigureInvalidModelResponseFactory(this IServiceCollection services)
    {
      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.InvalidModelStateResponseFactory = context =>
        {
          var errorResponse = CreateFromModelState(context.ModelState);
          var result = new BadRequestObjectResult(errorResponse);
          return result;
        };
      });
    }

    private static ErrorResponseDto CreateFromModelState(ModelStateDictionary modelState)
    {
      var details =
        modelState
          .SelectMany(CreateErrorDetailsFromModelStateEntry)
          .ToList();

      var error = new ErrorDto(
        code: ErrorCode.InvalidRequest,
        message: "Invalid request", details: details);

      return new ErrorResponseDto(error);
    }

    private static List<ErrorDto> CreateErrorDetailsFromModelStateEntry(KeyValuePair<string, ModelStateEntry> modelStateItem) =>
      modelStateItem.Value.Errors
        .Select(error => error.Exception != null
          ? new ErrorDto(ErrorCode.ServerFailure, error.Exception.Message, target: modelStateItem.Key)
          : new ErrorDto(ErrorCode.ValidationError, error.ErrorMessage, target: modelStateItem.Key))
        .ToList();
  }
}
