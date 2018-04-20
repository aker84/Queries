using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Queriess
{
    class Program
    {
        static string connectionDB = "server=localhost;user=root;database=Keksik;password=1234;SslMode=none;";
        static MySqlConnection MySQlConnection = new MySqlConnection(connectionDB);
        public static int Main(string[] args)
        {
            GetMaterialResidue();
            return 0;
        }

        public static List<string[]> ShowOrders_reg()
        {
            try
            {
                MySQlConnection.Open();
                MySqlCommand showOrders = new MySqlCommand("SELECT m.material, o.totalCost, o.startDate, o.finishDate, o.delivAddress, o.delivType, o.delivStatus, o.delivFullStatus, o.trackNumb, o.hashNumb AS m_material FROM orders o INNER JOIN materials m ON o.idMaterials=m.idMaterials", MySQlConnection);
                List<string[]> orders_list = new List<string[]>();
                MySqlDataReader sqlReader = showOrders.ExecuteReader();
                while (sqlReader.Read())
                {
                    orders_list.Add(new string[10]);
                    orders_list[orders_list.Count - 1][0] = sqlReader[0].ToString();
                    orders_list[orders_list.Count - 1][1] = sqlReader[1].ToString();
                    orders_list[orders_list.Count - 1][2] = sqlReader[2].ToString();
                    orders_list[orders_list.Count - 1][3] = sqlReader[3].ToString();
                    orders_list[orders_list.Count - 1][4] = sqlReader[4].ToString();
                    orders_list[orders_list.Count - 1][5] = sqlReader[5].ToString();
                    orders_list[orders_list.Count - 1][6] = sqlReader[6].ToString();
                    orders_list[orders_list.Count - 1][7] = sqlReader[7].ToString();
                    orders_list[orders_list.Count - 1][8] = sqlReader[8].ToString();
                    orders_list[orders_list.Count - 1][9] = sqlReader[9].ToString();
                }
                MySQlConnection.Close();
                sqlReader.Close();
                return orders_list;
            }
            catch
            {
                MySQlConnection.Close();
                return null;
            }  
        }

        public static string[] GetMaterialData() //Возвращается одна строчка из бд с инфой о мате
        {
            try
            {
                MySQlConnection.Open();
                string[] materialData = new string[2];
                MySqlCommand getMaterialData = new MySqlCommand("SELECT material, costPerCube FROM materials WHERE idMaterials=@idMaterials", MySQlConnection);
                //Добавить AND residue > x в запрос, если нид материал в наличии
                getMaterialData.Parameters.AddWithValue("idMaterials", reply_material.idMaterials); //тут у меня реплая нет, но там должно робить
                MySqlDataReader sqlReader = getMaterialData.ExecuteReader();
                while (sqlReader.Read())
                {
                    materialData[0] = sqlReader[0].ToString();
                    materialData[1] = sqlReader[1].ToString();
                }
                MySQlConnection.Close();
                sqlReader.Close();
                return materialData;
            }catch
            {
                MySQlConnection.Close();
                return null;
            }
        }
        public static float GetMaterialResidue()
        {
            try
            {
                MySQlConnection.Open();
                MySqlCommand getMaterialResidue = new MySqlCommand("SELECT residue FROM materials WHERE idMaterials=@idMaterials", MySQlConnection);
                getMaterialResidue.Parameters.AddWithValue("idMaterials", reply_material.idMaterials);
                float MaterialResidue = (float)getMaterialResidue.ExecuteScalar();
                MySQlConnection.Close();
                return MaterialResidue;
            }
            catch
            {
                MySQlConnection.Close();
                return 0;
            }
        }

    }
}
