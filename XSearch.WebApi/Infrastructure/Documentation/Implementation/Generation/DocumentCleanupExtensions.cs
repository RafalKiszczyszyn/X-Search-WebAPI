//  Copyright © Titian Software Ltd

using NJsonSchema;
using NSwag;

namespace XSearch.WebApi.Infrastructure.Documentation.Implementation.Generation
{
  internal static class DocumentCleanupExtensions
  {
    public static void FixAndCleanup(
      this OpenApiDocument document,
      IReadOnlyCollection<OpenApiParameterKind> notNullableParameterKinds)
    {
      document.ReplaceOneOfWithAllOfOnSingleProperties();
      document.RemoveNullablePropertiesFromNotNullableParameters(notNullableParameterKinds);
      document.RemoveEmptyDescriptionFromComponents();
      document.Operations.RemoveSecurityNoteExtensionData();
    }

    private static void ReplaceOneOfWithAllOfOnSingleProperties(this OpenApiDocument document)
    {
      var elementProperties = document.Definitions
        .Select(definition => definition.Value)
        .SelectMany(value => value.Properties);

      var oneOfProperties = document.Definitions
        .SelectMany(definition => definition.Value.OneOf)
        .SelectMany(value => value.Properties);

      var allOfProperties = document.Definitions
        .SelectMany(definition => definition.Value.AllOf)
        .SelectMany(value => value.Properties);

      var properties = elementProperties
        .Concat(oneOfProperties)
        .Concat(allOfProperties)
        .Select(property => property.Value);

      foreach (var property in properties)
      {
        if (property.OneOf.Count == 1 && property.OneOf.Single().Reference != null)
        {
          var newProperty = new JsonSchemaProperty
          {
            Reference = property.OneOf.Single().Reference
          };
          property.AllOf.Add(newProperty);
          property.OneOf.Clear();
        }
      }
    }

    private static void RemoveNullablePropertiesFromNotNullableParameters(this OpenApiDocument document, IReadOnlyCollection<OpenApiParameterKind> notNullableParameterKinds)
    {
      var nullableParams = document.Operations
        .SelectMany(operation => operation.Operation.Parameters)
        .Where(parameter => notNullableParameterKinds.Contains(parameter.Kind))
        .Select(parameter => parameter.Schema)
        .Where(schema => schema.IsNullableRaw == true);

      foreach (var param in nullableParams)
      {
        param.IsNullableRaw = null;
      }
    }

    private static void RemoveEmptyDescriptionFromComponents(this OpenApiDocument document)
    {
      var components = document.Components.Schemas.Values
        .Where(value => value.IsEnumeration && string.IsNullOrEmpty(value.Description))
        .ToList();

      foreach (var component in components)
      {
        component.Description = null;
      }
    }

    private static void RemoveSecurityNoteExtensionData(this IEnumerable<OpenApiOperationDescription> operations)
    {
      foreach (var operation in operations)
      {
        operation.Operation.ExtensionData.Remove(ExtensionDataKeys.SecurityNote);
      }
    }
  }
}
