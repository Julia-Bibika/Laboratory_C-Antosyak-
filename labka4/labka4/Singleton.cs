using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace labka4
{
    internal class Singleton
    {
        private static SQLiteConnection conn = null;
        private Singleton()
        {
        }
        public static SQLiteConnection GetInstance()
        {
            if (conn == null)
            {
                conn = new SQLiteConnection(@"URI=file:test.db");
                conn.Open();
            }
            Console.WriteLine("conected");
            return conn;
        }
        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
