using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrate.Common.ApiClients
{
  public class ApiResponse<T>
  {
    public T Body { get; set; }

    public HttpStatusCode HttpStatusCode { get; set; }

    public bool HasErrors => HttpStatusCode != HttpStatusCode.OK;

    public string ErrorMessage { get; set; }
  }
}
