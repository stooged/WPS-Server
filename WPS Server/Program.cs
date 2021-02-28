using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Net;
using DNS.Server;
using Griffin.WebServer;
using Griffin.WebServer.Files;
using System.ComponentModel;
using System.ServiceProcess;
using System.IO;
using static WPS_Server.Config;
using DNS.Protocol;
using static DNS.Server.DnsServer;

namespace WPS_Server
{
    class Program
    {

        static string ServerIP = GetLocalIPAddress(); 
        static MasterFile masterFile = new MasterFile();
        static DnsServer dnsServer = new DnsServer(masterFile, PublicDNS);
        static BackgroundWorker bwThread = new BackgroundWorker();


         static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new ServerSVC()
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {

                dnsServer.Responded += DnsServer_Responded;
                dnsServer.Errored += DnsServer_Error;

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


                Console.WriteLine("Server IP: " + ServerIP);

                ModuleManager httpManager = new ModuleManager();
                httpManager.Add(module);
                httpManager.Add(new HTTP_Module());
                HttpServer httpServer = new HttpServer(httpManager);
                httpServer.Start(IPAddress.Any, 80);
                Console.WriteLine("HTTP Server Started");



                ModuleManager httpsManager = new ModuleManager();
                httpsManager.Add(module);
                httpsManager.Add(new HTTPS_Module());
                HttpServer httpsServer = new HttpServer(httpsManager);
                X509Certificate2 certificate = new X509Certificate2(AppDomain.CurrentDomain.BaseDirectory + "\\cert.pfx", "server");
                httpsServer.Start(IPAddress.Any, 443, certificate);
                Console.WriteLine("HTTPS Server Started");


                Console.ReadLine();
            }

            }



        protected static void DnsThread(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("DNS Server Started");
            SetDnsRedirects(masterFile);
            try
            {
                ServiceController service = new ServiceController("sharedaccess"); //kill ics service to free dns port 53
                service.Stop();
                Thread.Sleep(1000);
            }
            catch (Exception ex) { Console.WriteLine("DNS - ERROR: " + ex.Message); }
            dnsServer.Listen();
        }

        static void DnsServer_Responded(object sender, RespondedEventArgs e)
        {
            for (var i = 0; i < e.Response.AnswerRecords.Count; i++)
            {
                Console.WriteLine(e.Response.AnswerRecords[i].ToString());
            };
        }

        static void DnsServer_Error(object sender, ErroredEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }


    }
}
