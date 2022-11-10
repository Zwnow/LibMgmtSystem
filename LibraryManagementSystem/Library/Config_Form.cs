using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Library
{
    public partial class Config_Form : Form
    {
        public Config_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pwd = this.textBox3.Text;
            var salt = PasswordControl.GenerateSalt();


            if(this.textBox2.Text == this.textBox3.Text && this.textBox1.Text != "")
            {
                //Insert Key User into Database
                string cs = "Data Source="+ConfigurationManager.AppSettings["data_db_path"]+"; Version=3;";
                SQLiteConnection con = new SQLiteConnection(cs, true);
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                string[] result = PasswordControl.GenerateHashedPassword(this.textBox3.Text);
                cmd.CommandText = $"INSERT INTO users(id, name, position, salt, password) " +
                                  $"VALUES(" +
                                  $"1," +
                                  $"'{this.textBox1.Text}'," +
                                  $"'Admin'," +
                                  $"'{result[0]}'," +
                                  $"'{result[1]}')";

                cmd.ExecuteNonQuery();

                //Close Config Form and Launch Loadup Form
                for(int i = 0; i<Application.OpenForms.Count; i++)
                {
                    Form f = Application.OpenForms[i];
                    if (f == this)
                        f.Close();
                }
            }
            else
            {
                MessageBox.Show("Bitte prüfen Sie die Eingaben.");
            }
        }




    }
}
