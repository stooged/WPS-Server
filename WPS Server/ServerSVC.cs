using System;
using System.ComponentModel;
using System.ServiceProcess;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Net;
using DNS.Server;
using Griffin.WebServer;
using Griffin.WebServer.Files;
using System.IO;
using static WPS_Server.Config;

namespace WPS_Server
{

    public partial class ServerSVC : ServiceBase
    {

        static string ServerIP = GetLocalIPAddress();
        static MasterFile masterFile = new MasterFile();
        static DnsServer dnsServer = new DnsServer(masterFile, PublicDNS);
        static BackgroundWorker bwThread = new BackgroundWorker();


        public ServerSVC()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            bwThread.DoWork += DnsThread;
            bwThread.RunWorkerAsync();


            if (!Directory.Exists(ServerDirectory))
            {
                Directory.CreateDirectory(ServerDirectory);
                File.WriteAllText(ServerDirectory + @"\index.html", DefaultIndexHtml);
            }

            DiskFileService fileService = new DiskFileService("/", ServerDirectory);
            FileModule module = new FileModule(fileService)
            {
                AllowFileListing = false,
            };


            ModuleManager httpManager = new ModuleManager();
            httpManager.Add(module);
            httpManager.Add(new HTTP_Module());
            HttpServer httpServer = new HttpServer(httpManager);
            httpServer.Start(IPAddress.Any, 80);


            ModuleManager httpsManager = new ModuleManager();
            httpsManager.Add(module);
            httpsManager.Add(new HTTPS_Module());
            HttpServer httpsServer = new HttpServer(httpsManager);
            X509Certificate2 certificate = new X509Certificate2(AppDomain.CurrentDomain.BaseDirectory + "\\cert.pfx", "server");
            httpsServer.Start(IPAddress.Any, 443, certificate);

        }

        protected override void OnStop()
        {
        }

        protected static void DnsThread(object sender, DoWorkEventArgs e)
        {
            SetDnsRedirects(masterFile);
            try
            {
                ServiceController service = new ServiceController("sharedaccess"); //kill ics service to free dns port 53
                service.Stop();
                Thread.Sleep(1000);
            }
            catch (Exception) { }

            dnsServer.Listen();
        }



    }
}
