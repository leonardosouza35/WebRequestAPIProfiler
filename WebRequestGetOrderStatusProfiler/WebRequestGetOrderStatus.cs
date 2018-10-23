using System;
using System.Threading.Tasks;
using System.Xml;
using WebRequestDAO.DAO;
using WebRequestLogger.Log;

namespace WebRequestGetOrderStatusProfiler
{
    public enum RunModeEnum
    {
        Simple,
        Parallel
    }

    public class WebRequestGetOrderStatus
    {
        public static void Init(RunModeEnum runMode)
        {
            var orders = new OrderDAO().ListOrders();
                        
            Logger.Info("Numbers of Orders to Call API = " + orders.Count + " Orders");

            if (runMode == RunModeEnum.Parallel)
                RunParallel(orders);
            else
                RunSimple(orders);
        }

        private static void RunParallel(System.Collections.Generic.List<WebRequestDTO.OrderDTO> orders)
        {
            XmlNode nodeReturn;
            var api = new OrderAPI.mtsSoapClient();

            Parallel.ForEach(orders, (o) =>
            {
                nodeReturn = api.GetOrderStatus(new OrderAPI.AuthHeader() { UserName = "pay0102ausr", Password = "yHp37mQQ!8" },
                                o.OrAgentId.ToString(), o.OrAgentCompanyID.ToString(), o.OrGivenID, o.OrderID.ToString());

                Logger.Info("Get Order Status Of Order: " + o.OrderID.ToString());
                if (nodeReturn.Name.Trim() == "Error")
                {
                    Logger.Error("Returns Error: ");
                    Logger.Error(nodeReturn.InnerText);
                }
                else
                {
                    Logger.Info("Ok Returns");
                }
            });
        }

        private static void RunSimple(System.Collections.Generic.List<WebRequestDTO.OrderDTO> orders)
        {
            XmlNode nodeReturn;

            var api = new OrderAPI.mtsSoapClient();
            foreach (var o in orders)
            {
                nodeReturn = api.GetOrderStatus(new OrderAPI.AuthHeader() { UserName = "pay0102ausr", Password = "yHp37mQQ!8" },
                                o.OrAgentId.ToString(), o.OrAgentCompanyID.ToString(), o.OrGivenID, o.OrderID.ToString());

                Logger.Info("Get Order Status Of Order: " + o.OrderID.ToString());
                if (nodeReturn.Name.Trim() == "Error")
                {
                    Logger.Error("Returns Error: ");
                    Logger.Error(nodeReturn.InnerText);
                }
                else
                {
                    Logger.Info("Ok Returns");
                }
            }
            
        }
    }
}