using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Hosting.Server;
using Microsoft.AspNet.Owin;
using Microsoft.Framework.Configuration;
using Nowin;
using Microsoft.Extensions.Configuration;

namespace NowinServer {

    public class NowinServerFactory : IServerFactory
    {
        private Func<IFeatureCollection, Task> _callback;

        private Task HandleRequest(IDictionary<string, object> env)
        {
            return _callback(new OwinFeatureCollection(env));
        }

        public IDisposable Start(IFeatureCollection serverFeatures, Func<IFeatureCollection, Task> application)
        {
            var builder = ServerBuilder.New()
                                       .SetAddress(IPAddress.Any)
                                       .SetPort(5001)
                                       .SetOwinApp(OwinWebSocketAcceptAdapter.AdaptWebSockets(HandleRequest));
            _callback = application;
            var server = builder.Build();
            server.Start();
            return server;
        }

        public IFeatureCollection Initialize(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            return new FeatureCollection();
        }
    }
}



//public class NowinServerFactory : IServerFactory {

//    private Func<IFeatureCollection, Task> callback;

//    IFeatureCollection Initialize(IConfiguration configuration) {
//        // adapt aspnet to owin app;
//        var owinApp = OwinWebSocketAcceptAdapter.AdaptWebSockets(HandleRequest);

//        // Get server info, write to console.
//        var server = configuration["server"];
//        var serverUrls = configuration["server.urls"];
//        Console.WriteLine("Owin server is: {0}, listening at {1}", server, serverUrls);
//        // parse ip address and port.
//        var uri = new Uri(serverUrls, UriKind.Absolute);
//        IPAddress ip;
//        if (!IPAddress.TryParse(uri.Host, out ip)) {
//            if (uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)) {
//                ip = IPAddress.Parse("127.0.0.1");
//            }
//            else {
//                ip = IPAddress.Any;
//            }
//        }
//        var port = uri.Port;

//        // build nowin server;
//        var builder = ServerBuilder.New()
//            .SetAddress(ip)
//            .SetPort(port)
//            .SetOwinApp(owinApp);

//        var serverInfo = new NowinServerInformation(builder);
//        return serverInfo;
//    }

//    private Task HandleRequest(IDictionary<string, object> env) {
//        return callback(new OwinFeatureCollection(env));
//    }

//    IDisposable Start(IFeatureCollection serverFeatures, Func<IFeatureCollection, Task> application) {
//        var info = (NowinServerInformation)serverInformation;
//        var server = info.Builder.Build();
//        callback = application;
//        server.Start();
//        return server;
//    }

//}
