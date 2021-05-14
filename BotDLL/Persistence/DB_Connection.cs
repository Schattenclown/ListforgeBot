using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BotDLL
{
    class DB_Connection
    {
        private static string token = "";
        private static int virgin = 0;
        public static void SetDB()
        {
            Connections connections = Connections.GetConnections();
            token = connections.MySqlConStr;
        }
        public static MySqlConnection OpenDB()
        {
            if (virgin == 0)
                SetDB(); virgin = 69;
            MySqlConnection connection = new MySqlConnection(token);
            do
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    DebugLog.Log("ERROR: DB_Connection.OpenDB" + ex.Message);
                    Thread.Sleep(10);
                }
            } while (connection == null);

            return connection;
        }
        public static void CloseDB(MySqlConnection connection)
        {
            connection.Close();
        }
        public static void ExecuteNonQuery(String sql, bool notification)
        {
            MySqlConnection connection = OpenDB();
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            int ret = sqlCommand.ExecuteNonQuery();
            if (ret != -1 && notification == true )
                MessageBox.Show("Worked!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseDB(connection);
        }
        public static MySqlDataReader ExecuteReader(String sql, MySqlConnection connection)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            try
            {
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                return sqlDataReader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }
    }
}
