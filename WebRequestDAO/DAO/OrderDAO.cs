using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRequestDTO;
using WebRequestLogger.Log;

namespace WebRequestDAO.DAO
{
    public class OrderDAO
    {
        public List<OrderDTO> ListOrders()
        {
            SqlConnection conn = null;
            try
            {
                DateTime inicio = DateTime.Parse(ConfigurationManager.AppSettings["DataInicioProcessamento"]);
                DateTime fim = DateTime.Parse(ConfigurationManager.AppSettings["DataFimProcessamento"]);
                var doubleOrderListNumber = int.Parse(ConfigurationManager.AppSettings["DoubleOrderListNumber"]);

                var sqlCommand = $"select OrderID, OrGivenID, OrAgentID, OrAgentCompanyID from dbo.tbOrders where OrCreateTime BETWEEN '{inicio.ToString("yyyy-MM-dd")}' and '{fim.ToString("yyyy-MM-dd")}'";

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PontualDbConnection"].ConnectionString);
                
                SqlCommand command = new SqlCommand(sqlCommand, conn);

                conn.Open();

                var dataReader = command.ExecuteReader();

                var orders = new List<OrderDTO>();
                while (dataReader.Read())
                {
                    var order = new OrderDTO();
                    order.OrderID = Convert.ToInt64(dataReader["OrderID"]);
                    order.OrAgentId = Convert.ToInt32(dataReader["OrAgentId"]);
                    order.OrAgentCompanyID = Convert.ToInt32(dataReader["OrAgentCompanyID"]);                    
                    order.OrGivenID = dataReader["OrGivenID"].ToString();

                    orders.Add(order);                
                }

                for (int i = 1; i <= doubleOrderListNumber; i++)
                {                    
                    orders.AddRange(orders);
                }
                                                
                return orders;

            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
