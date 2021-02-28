using DNS.Server;
using System;
using System.Net;
using System.Net.Sockets;


namespace WPS_Server
{
    class Config
    {

        public static string PublicDNS = "8.8.8.8"; // 1.1.1.1
        public static string ServerDirectory = AppDomain.CurrentDomain.BaseDirectory + "htdocs";
        public static string DefaultIndexHtml = "<!DOCTYPE html><html><head><style>body { background-color: #1451AE;color: #ffffff;font-size: 14px; font-weight: bold; margin: 0 0 0 0.0; padding: 0.4em 0.4em 0.4em 0.6em;}</style></head><center><br><br><br><br><br><br>Add html files to: " + ServerDirectory + "</center></html>";


        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return IPAddress.Any.ToString();
        }


        public static void SetDnsRedirects(MasterFile mF)
        {
            mF.AddIPAddressResourceRecord("manuals.playstation.net", GetLocalIPAddress());
            mF.AddIPAddressResourceRecord("fjp01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fus01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fau01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fuk01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("feu01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fkr01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fsa01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("ftw01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fru01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fmx01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("fcn01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("djp01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dus01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dau01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("duk01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("deu01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dkr01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dsa01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dtw01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dru01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dmx01.ps5.update.playstation.net", "127.0.0.1");
            mF.AddIPAddressResourceRecord("dcn01.ps5.update.playstation.net", "127.0.0.1");
        }

    }
}
