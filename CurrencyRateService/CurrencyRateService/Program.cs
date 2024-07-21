using CurrrencyRatesService;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Xml;

namespace CurrencyRateService
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //For Debug

            //CurrencyRateService service = new CurrencyRateService();
            //service.OnDebug();
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);


            if (args.Length == 0)
            {
                ServiceBase.Run(new CurrencyRateService());
            }
            else
            {
                if (args.Length == 1)
                {
                    if (args[0] == "-install")
                    {
                        ExecuteCommandAsAdmin($"c:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil.exe -i \"{Assembly.GetExecutingAssembly().Location}\"");
                        CreateXML();
                    }
                    else if(args[0] == "-uninstall")
                    {
                        ExecuteCommandAsAdmin($"c:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil.exe -u \"{Assembly.GetExecutingAssembly().Location}\"");
                    }
                    else
                    {
                        return;
                    }
                }

                ServiceController sc = new ServiceController("CurrencyRateService");

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-out" && i + 1 < args.Length)
                    {
                        Config.RevriteConfig(null, args[i + 1], Assembly.GetExecutingAssembly()
                            .Location.ToString()
                            .Replace("CurrencyRateService.exe", "AppSettings.xml"));
                        Thread.Sleep(200);
                        sc.ExecuteCommand(128); 
                    }
                    else if (args[i] == "-fmt" && i + 1 < args.Length)
                    {
                        switch (args[i + 1])
                        {
                            case "xml": 
                                sc.ExecuteCommand(129); // Command 129 to change data format to xml
                                break;
                            case "csv":
                                sc.ExecuteCommand(130); // Command 130 to change data format to csv
                                break;
                            case "json":
                                sc.ExecuteCommand(131); // Command 131 to change data format to json
                                break;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        static private void CreateXML()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement("Config");
            xmlDoc.AppendChild(root);

            XmlElement fetchInterval = xmlDoc.CreateElement("FetchInterval");
            fetchInterval.InnerText = "36";

            XmlElement outputFormat = xmlDoc.CreateElement("OutputFormat");
            outputFormat.InnerText = "xml";

            XmlElement outputFilePath = xmlDoc.CreateElement("OutputFilePath");
            outputFilePath.InnerText = $"{Assembly.GetExecutingAssembly().Location.Replace("CurrencyRateService.exe", "Result")}";

            root.AppendChild(fetchInterval);
            root.AppendChild(outputFormat);
            root.AppendChild(outputFilePath);

            Logger.Instance().LogInfo($"{Assembly.GetExecutingAssembly().Location.Replace("CurrencyRateService.exe", "Result")}");

            xmlDoc.Save($"{Assembly.GetExecutingAssembly().Location.Replace("CurrencyRateService.exe", "AppSettings.xml")}");
        }

        static private void ExecuteCommandAsAdmin(string command)
        {
            // Create a new process start info
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "cmd.exe";
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.Arguments = $"/c {command}";
            processStartInfo.Verb = "runas"; // Request administrator privileges

            // Starting process
            Process.Start(processStartInfo);
        }
    }
}
