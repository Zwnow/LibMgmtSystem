using System.Configuration;
using System.Data.SQLite;

namespace Library
{
    public partial class Reg_Book_Form : Form
    {
        public Reg_Book_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(StateHandler.loggedIn)
            {
                //Get most recent Database row
                string cs = "Data Source=" + ConfigurationManager.AppSettings["data_db_path"] + "; Version=3;";
                SQLiteConnection con = new SQLiteConnection(cs);
                con.Open();

                SQLiteCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM books ORDER BY id DESC LIMIT 1";
                using SQLiteDataReader rdr = com.ExecuteReader();

                int id = 0;

                while (rdr.Read())
                {
                    id = rdr.GetInt16(0);
                }
                rdr.Close();

                //Insert into Database with incremented id
                com.CommandText = 
                    $"INSERT INTO books(id, name, category, ean, available, lendTo) " +
                    $"VALUES({id + 1},'{this.textBox1.Text}','{this.textBox2.Text}',{Int32.Parse(this.textBox3.Text)},{this.checkBox1.Checked},{Int32.Parse(this.textBox4.Text)})";
                com.ExecuteNonQuery();

                MessageBox.Show("Buch registriert.");
            }
        }
    }
}
