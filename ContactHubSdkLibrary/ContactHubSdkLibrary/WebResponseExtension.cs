using System;
using System.Net;

namespace ContactHubSdkLibrary
{

    public static class WebRequestExtensions
    {
        /// <summary>
        /// WebResponse custom with error handling
        /// </summary>
        public static WebResponse GetResponseWithoutException(this WebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                int sc = (int)response.StatusCode;
                response.Headers["StatusCode"] = sc.ToString();
                response.Headers["Status"] = response.StatusCode.ToString();
                return response;
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)e.Response;
                    int sc = (int)response.StatusCode;
                    e.Response.Headers["StatusCode"] = sc.ToString();
                    e.Response.Headers["Status"] = response.StatusCode.ToString();

                }

                //                e.Response.Headers["Status"] = e.Message.ToString();
                return e.Response;
            }
        }
    }
}
