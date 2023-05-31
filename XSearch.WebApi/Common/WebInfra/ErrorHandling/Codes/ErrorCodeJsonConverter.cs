

using Newtonsoft.Json;
using XSearch.WebApi.Common.Errors;

namespace XSearch.WebApi.Common.WebInfra.ErrorHandling.Codes
{
  public class ErrorCodeJsonConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType)
      => objectType == typeof(ErrorCode);

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
      var value = (string)reader.Value!;
      return ErrorCode.Create(value!);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
      var errorCode = (ErrorCode)value!;
      serializer.Serialize(writer, errorCode.Value);
    }
  }
}
