using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSearch.WebApi.Common.Errors
{
  internal sealed class ErrorException : Exception
  {
    /// <summary>
    /// Error that caused <see cref="ErrorException"/>.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates instance of <see cref="ErrorException"/>.
    /// </summary>
    /// <param name="error">Error to create exception from.</param>
    public ErrorException(Error error)
      : base(CreateErrorMessage(error))
    {
      Error = error;
    }

    private static string CreateErrorMessage(Error error)
    {
      return "Error occurred:" + Environment.NewLine + error.ToString().Indent();
    }
  }
}
