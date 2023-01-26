using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aLibrary
{
    public static class dbop
    {
        public static string connString = "server=localhost;" +
            "database=alibrary; " +
            "uid=root;" +
            "pwd=;";

        public static DataTable sendquery(string srQuery)
        {
            DataTable dtReturnTable = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    using (MySqlDataAdapter DA = new MySqlDataAdapter(srQuery, connection))
                    {
                        DA.Fill(dtReturnTable);
                    }
                }

            }
            catch (Exception E)
            {
                logSQLErrors(srQuery, E);
            }

            return dtReturnTable;
        }
        public static int sendexecute(string sql)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                logSQLErrors(sql, e);
            }
            return -1;
        }



        private static void logSQLErrors(string srQuery, Exception E)
        {
            File.AppendAllText("SQLerrors.txt", "SQL error: Query= " + srQuery + " Error: " + E?.Message + "\r\n" + E?.InnerException?.ToString() + "\r\n\r\n");
        }

        //https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
        public static string newP;
        public static string sifrele(string myMsg)
        {
            newP = string.Empty;
            var msgBytes = Encoding.ASCII.GetBytes(myMsg);
            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(msgBytes);
            foreach (byte item in hash)
            {
                newP += item.ToString("x2");
            }
            return newP;
        }
    }
}
