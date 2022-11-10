using System.Configuration;
using System.Data.SQLite;

namespace Library
{
    public partial class Reg_Customer_Form : Form
    {
        public Reg_Customer_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text != String.Empty && this.textBox2.Text != String.Empty)
            {
                //Get most recent Database row
                string cs = "Data Source=" + ConfigurationManager.AppSettings["data_db_path"] + "; Version=3;";
                SQLiteConnection con = new SQLiteConnection(cs);
                con.Open();

                SQLiteCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM customers ORDER BY id DESC LIMIT 1";
                using SQLiteDataReader rdr = com.ExecuteReader();

                int id = 0;
                while(rdr.Read())
                {
                    id = rdr.GetInt16(0);
                }
                rdr.Close();

                //Insert into Database with incremented id
                com.CommandText = $"INSERT INTO customers(id,name,code) VALUES({id+1},'{this.textBox1.Text}','{this.textBox2.Text}')";
                com.ExecuteNonQuery();

                MessageBox.Show("Kunde registriert.");
            }
        }
    }
}
