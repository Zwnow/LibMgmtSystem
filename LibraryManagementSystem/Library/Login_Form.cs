using System.Configuration;
using System.Data.SQLite;

namespace Library
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text == "" || this.textBox2.Text == "")
            {
                MessageBox.Show("Bitte überprüfen Sie die eingegebenen Daten.");
            }
            else 
            {
                string cs = "Data Source=" + ConfigurationManager.AppSettings["data_db_path"] + "; Version=3;";
                SQLiteConnection con = new SQLiteConnection(cs, true);
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM users WHERE name = '{this.textBox1.Text}'";
                using SQLiteDataReader rdr = cmd.ExecuteReader();


                int id = 0;
                string username = "";
                string position = "";
                string salt = "";
                string password = "";
                
                while (rdr.Read())
                {
                    id = rdr.GetInt16(0);
                    username = rdr.GetString(1);
                    position = rdr.GetString(2);
                    salt = rdr.GetString(3);
                    password = rdr.GetString(4);
                }
                rdr.Close();

                if(PasswordControl.ValidatePassword(this.textBox2.Text,salt,password))
                {
                    StateHandler.loggedIn = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unbekannter Nutzer.");
                }
            }
        }
    }
}
