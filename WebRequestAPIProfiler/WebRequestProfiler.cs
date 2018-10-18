using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WebRequestAPIProfiler.Log;

namespace WebRequestAPIProfiler
{
    public class WebRequestProfiler
    {
        public static void Init()
        {
            Logger.Info("Process Started");

            var xmlPath = File.ReadAllText(ConfigurationManager.AppSettings["XMLFilePath"]);
            
            byte[] bytes = Encoding.ASCII.GetBytes(xmlPath);

            var timesToRun = 2;

            PostXML(bytes, xmlPath, timesToRun);

            Logger.Info("Process Finished");
        }

        public static void PostXML(byte[] bytesXml, string xmlRequest, int timesToRun)
        {            
            for (int i = 0; i < timesToRun; i++)
            {                
                InnerPost(bytesXml, xmlRequest);
            }
         
        }

        private static void InnerPost(byte[] bytesXml, string xmlRequest)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xmlRequest);

            request.ContentType = "text/xml; encoding='utf-8";
            request.ContentLength = bytesXml.Length;
            request.Method = "POST";

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytesXml, 0, bytesXml.Length);
            requestStream.Close();


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseStream = response.GetResponseStream();
                string strResponse = new StreamReader(responseStream).ReadToEnd();                
            }
        }
    }
}
