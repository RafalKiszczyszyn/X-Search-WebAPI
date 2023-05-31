

using System.Reflection;
using NJsonSchema;
using NJsonSchema.Generation;
using XSearch.WebApi.Common.Errors;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling
{
  /// <summary>
  /// Schema processor for rewriting <see cref="ErrorDto"/> definition in OpenAPI documentation, so the errorCode is still string and innerError is any object.
  /// </summary>
  public class ErrorSchemaProcessor : ISchemaProcessor
  {
    private static readonly string s_valuePropertyName = nameof(ErrorCode.Value).ToLower();
    private static readonly List<PropertyInfo> s_propertiesOfJsonSchemaProperty = GetProperties<JsonSchemaProperty>();

    private static List<PropertyInfo> GetProperties<T>() =>
      typeof(T).GetProperties()
        .Where(x => x.CanRead && x.CanWrite)
        .ToList();

    /// <summary>
    /// Processes provided <see cref="SchemaProcessorContext"/>.
    /// </summary>
    /// <param name="context"></param>
    public void Process(SchemaProcessorContext context)
    {
      RewriteProperty(context, "code");
      ChangeTypeToObject(context, "innerError");
    }

    private static void RewriteProperty(SchemaProcessorContext context, string propertyName)
    {
      var propertyTypeSchema = GetPropertySchema(context, propertyName);

      if (propertyTypeSchema != null && propertyTypeSchema.ActualSchema.Properties.Any())
      {
        var innerValueTypeSchema = ExtractInnerValueTypeSchema(propertyTypeSchema);

        var description = propertyTypeSchema.Description;

        OverwriteAllProperties(propertyTypeSchema, innerValueTypeSchema);

        propertyTypeSchema.Description = description;
      }
    }

    private static void ChangeTypeToObject(SchemaProcessorContext context, string propertyName)
    {
      var propertyTypeSchema = GetPropertySchema(context, propertyName);

      if (propertyTypeSchema != null)
      {
        context.Schema.ActualSchema.Properties.Remove(propertyName);
        var property = new JsonSchemaProperty
        {
          Description = propertyTypeSchema.Description,
          IsNullableRaw = true
        };
        context.Schema.ActualSchema.Properties.Add(propertyName, property);
      }
    }

    private static JsonSchemaProperty? GetPropertySchema(
      SchemaProcessorContext context,
      string propertyName)
    {
      var valueFound =
        context.Schema.ActualSchema.Properties.TryGetValue(propertyName, out var propertySchema);

      return valueFound
        ? propertySchema
        : null;
    }

    private static JsonSchemaProperty ExtractInnerValueTypeSchema(JsonSchema propertySchema)
    {
      var updateValueTypeSchema = propertySchema.ActualSchema;
      return updateValueTypeSchema.Properties[s_valuePropertyName];
    }

    private static void OverwriteAllProperties(JsonSchemaProperty schemaToOverwrite, JsonSchemaProperty schemaWithNewValues)
    {
      foreach (var propertyInfo in s_propertiesOfJsonSchemaProperty)
      {
        var newValue = propertyInfo.GetValue(schemaWithNewValues);
        propertyInfo.SetValue(schemaToOverwrite, newValue);
      }
    }
  }
}
