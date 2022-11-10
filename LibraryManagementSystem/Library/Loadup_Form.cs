using System.Windows.Forms;

namespace Library
{
    public partial class Loadup_Form : Form
    {
        public Loadup_Form()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormCollection forms = Application.OpenForms;
            bool formIsOpen = false;

            foreach(Form form in forms)
            {
                if(form.Name == "Login_Form")
                {
                    formIsOpen = true;
                }
            }

            if(!formIsOpen && !StateHandler.loggedIn)
            {
                Form login_form = new Login_Form();
                login_form.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Bereits eingeloggt.");
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Reg_Customer_Form reg = new Reg_Customer_Form();
            reg.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Reg_Book_Form reg = new Reg_Book_Form();
            reg.Show();
        }
    }
}