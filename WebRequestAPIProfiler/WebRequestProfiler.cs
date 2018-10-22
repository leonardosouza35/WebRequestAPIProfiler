using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebRequestAPIProfiler.Log;
using WebRequestAPIProfiler.Models;

namespace WebRequestAPIProfiler
{
    public class WebRequestProfiler
    {
        static Stopwatch stopWatch = new Stopwatch();
        public static void Init()
        {            
            
            var timesToRun = int.Parse(ConfigurationManager.AppSettings["timesToRun"]);

            Logger.Info($"Number of times to make request = {timesToRun}");

            var urlRequest = ConfigurationManager.AppSettings["RequestURL"];

            PostXML(urlRequest, timesToRun);

        }

       
        public static void PostXML(string urlRequest, int timesToRun)
        {            
            for (int i = 1; i <= timesToRun; i++)
            {
                IncrementAgentReference();

                var xmlContent = File.ReadAllText(ConfigurationManager.AppSettings["XMLFilePath"]);                

                byte[] bytes = Encoding.ASCII.GetBytes(xmlContent);

                InnerPost(bytes, urlRequest, i);
            }
         
        }

        private static void InnerPost(byte[] bytesXml, string xmlRequest, int attemptCount)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xmlRequest);

            request.ContentType = "text/xml; encoding='utf-8";
            request.ContentLength = bytesXml.Length;
            request.Method = "POST";

            Logger.Info("Start requesting in attempt -> " + attemptCount);
            stopWatch.Start();

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytesXml, 0, bytesXml.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseStream = response.GetResponseStream();
                string strResponse = new StreamReader(responseStream).ReadToEnd();
                var orderResponse = ParseResponse(strResponse);                                
                LogResult(orderResponse, attemptCount);
            }
            
            stopWatch.Stop();
        }

        private static void LogResult(CreateNewOrderResponse orderResponse, int attemptCount)
        {
            string message = string.Empty;
            if (orderResponse.HasError)
            {                
                message = $"{Environment.NewLine}";
                message += $"Finished requesting in attempt -> {attemptCount} with ERROR { Environment.NewLine}";
                message += $"Finished in {stopWatch.Elapsed}{Environment.NewLine}";
                message += $"With the following details: {Environment.NewLine}";
                message += $"ResultCodes: { orderResponse.CreateNewOrderResult.Error.ResultCodes}{Environment.NewLine}";
                message += $"ResultMessage: { orderResponse.CreateNewOrderResult.Error.ResultMessage}{Environment.NewLine}";                
                message += $"{Environment.NewLine}";
                Logger.Error(message);                
            }
            else
            {
                Logger.Info($"Finished requesting in attempt -> {attemptCount} with NO ERROR");
                Logger.Info($"Finished in { stopWatch.Elapsed}");
                Logger.Info($"With the following details...");
                Logger.Info($"OrderID: { orderResponse.CreateNewOrderResult.NewOrderResponse.OrderID}");                                
            }
        }

        private static CreateNewOrderResponse ParseResponse(string response)
        {
           
            var xns = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");
            var xDocument = XDocument.Load(new StringReader(response));

            var soapBody = xDocument.Descendants(xns + "Body").First().Descendants().First().ToString();

            XmlSerializer serializer = new XmlSerializer(typeof(CreateNewOrderResponse));
            XmlReaderSettings settings = new XmlReaderSettings();

            using(StringReader reader = new StringReader(soapBody))
            {
                using(XmlReader xmlReader = XmlReader.Create(reader,settings))
                {
                    return (CreateNewOrderResponse)serializer.Deserialize(xmlReader);                    
                }
            }            
        }

        private static void IncrementAgentReference()
        {
            var xdoc = XDocument.Load(ConfigurationManager.AppSettings["XMLFilePath"]);            
            var elements = xdoc.Root.Elements();
            foreach (var element in elements)
            {
                if (element.Name.LocalName == "Body")
                {
                    var bodyElements = element.Elements();
                    foreach (var bodyElement in bodyElements)
                    {
                        if (bodyElement.Name.LocalName == "CreateNewOrder")
                        {
                            var agentElement = bodyElement.Elements().Where(i => i.Name.LocalName == "AgentOrderReference").Single();
                            agentElement.Value = (Convert.ToInt64(agentElement.Value) + 1).ToString();

                        }
                    }
                }

            }
            xdoc.Save(ConfigurationManager.AppSettings["XMLFilePath"]);
        }
    }
}
