using System;
using Griffin.WebServer;
using Griffin.WebServer.Modules;
using static WPS_Server.Config;

namespace WPS_Server
{
    public class HTTP_Module : IWorkerModule
    {
        public void BeginRequest(IHttpContext context)
        {
            Console.WriteLine("HTTP - " + context.Request.Uri.ToString());
        }

        public void EndRequest(IHttpContext context)
        {

        }

        public void HandleRequestAsync(IHttpContext context, Action<IAsyncModuleResult> callback)
        {
            callback(new AsyncModuleResult(context, HandleRequest(context)));
        }

        public ModuleResult HandleRequest(IHttpContext context)
        {
            return ModuleResult.Continue;
        }
    }

    public class HTTPS_Module : IWorkerModule
    {
        public void BeginRequest(IHttpContext context)
        {
            Console.WriteLine( "HTTPS - " +    context.Request.Uri.ToString());
        }

        public void EndRequest(IHttpContext context)
        {
            context.Response.ReasonPhrase = "Moved Permanently";
            context.Response.AddHeader("Location", "http://" + GetLocalIPAddress() + "/");
            context.Response.StatusCode = 301;
        }

        public void HandleRequestAsync(IHttpContext context, Action<IAsyncModuleResult> callback)
        {
            callback(new AsyncModuleResult(context, HandleRequest(context)));
        }

        public ModuleResult HandleRequest(IHttpContext context)
        {
            return ModuleResult.Continue;
        }

    }

}
