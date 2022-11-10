using System.Data.SQLite;

namespace Library
{
    internal class DBHandler
    {
        public static void CreateDatabase(string name, string path)
        {
            if(name.Length > 3 && name.Substring(name.Length-4) != ".db")
                SQLiteConnection.CreateFile(path + $"\\{name}.db");
            else
            {
                SQLiteConnection.CreateFile(path + $"\\{name}");
            }
        }

        public static void Setup(string path)
        {
            string data_cs = "DataSource=" + path + "\\data.db; Version=3;";

            using var data_con = new SQLiteConnection(data_cs, true);
            data_con.Open();

            using var data_cmd = new SQLiteCommand(data_con);

            data_cmd.CommandText = @"CREATE TABLE users(id INTEGER PRIMARY KEY, name TEXT, position TEXT, salt TEXT, password TEXT)";
            data_cmd.ExecuteNonQuery();

            data_cmd.CommandText = @"CREATE TABLE books(id INTEGER PRIMARY KEY, name TEXT, category TEXT, ean INTEGER, available BOOLEAN, lendTo INTEGER)";
            data_cmd.ExecuteNonQuery();

            data_cmd.CommandText = @"CREATE TABLE customers(id INTEGER PRIMARY KEY, name TEXT, code INTEGER)";
            data_cmd.ExecuteNonQuery();
        }
    }


}
