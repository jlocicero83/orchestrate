using System.Web;

namespace Orchestrate.Common.Extensions
{
  public static class QueryStringParametersExtensions
  {
    public static string ToQueryString(this QueryStringParameters obj)
    {
      var queryParams = new List<string>();

      if (!string.IsNullOrEmpty(obj.Fields))
        queryParams.Add($"fields={HttpUtility.UrlEncode(obj.Fields)}");

      if (!string.IsNullOrEmpty(obj.Filter))
        queryParams.Add($"fields={HttpUtility.UrlEncode(obj.Filter)}");

      queryParams.Add($"includeTotalCount={obj.IncludeTotalCount.ToString().ToLower()}");

      return $"?{string.Join("&", queryParams)}";
    }
  }
}
