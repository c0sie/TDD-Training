using System.Web;

namespace Training.Common.Web
{
    public class ServerVariables : IServerVariables
    {
        public string ClientIP => HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        public string Port => HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

        public string Protocol => HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];

        public string ServerName => HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

        public string Host => HttpContext.Current.Request.Url.Host;
    }
}
