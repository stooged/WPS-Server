# WPS Server
 Windows

 a simple windows webserver that can be run as a desktop application or installed as a background service that runs on windows startup.<br>
 the server contains a dns server to redirect requests to the server or block them by redirecting to the localhost.<br>
 all https requests are redirected to the http index page.<br><br>
 
 the currect server configuration is setup for the ps5 to redirect the user guide to the server and also allow internet access to the ps5 to allow the ps5 webbrowser to access youtube/google etc.<br>
 there is no html included so if you want to browse sites you will have to setup a page to display links to those sites so you can navigate to them with the ps5 browser.<br><br>
 
 the project uses .NET 4.6.1 and was compiled in visual studio 2019.<br>
 it uses the <a href="https://github.com/jgauffin/Griffin.WebServer">Griffin.Webserver</a> nuget package and the <a href="https://github.com/kapetan/dns">DNS</a> repo<br>
 