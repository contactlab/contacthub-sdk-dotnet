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
                HttpWebResponse re = (HttpWebResponse)request.GetResponse();
                re.Headers["Status"] = re.StatusCode.ToString();
                return re;
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                e.Response.Headers["Status"] = e.Message.ToString();
                return e.Response;
            }
        }
    }
}
