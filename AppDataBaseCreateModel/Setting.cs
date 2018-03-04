using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDataBaseCreateModel
{
    public partial class Setting : Form
    {
        Ocph.DAL.MyConfiguration config = new Ocph.DAL.MyConfiguration();
        public Setting()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Servertxt.Text = config.Server;
            this.DatabaseTxt.Text = config.Database;
            this.UserTxt.Text = config.User;
            this.PasswordTxt.Text = config.Password;
            this.porttxt.Text = config.Port.ToString(); 
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Model = new DatabaseSetting {   Database = this.DatabaseTxt.Text, Password=PasswordTxt.Text, Port=porttxt.Text,
            Server=Servertxt.Text, UserName=UserTxt.Text};
            try
            {
                    config.UpdateKey("Server", this.Servertxt.Text);
                    config.UpdateKey("Database", this.DatabaseTxt.Text);
                    config.UpdateKey("User", this.UserTxt.Text);
                    config.UpdateKey("Password", this.PasswordTxt.Text);
                    config.UpdateKey("Port", this.porttxt.Text);
                    MessageBox.Show("Configuration Saved");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public DatabaseSetting Model { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Model = new DatabaseSetting {   Database = this.DatabaseTxt.Text, Password=PasswordTxt.Text, Port=porttxt.Text,
            Server=Servertxt.Text, UserName=UserTxt.Text};
            var constr = string.Format("Server={0};database={1};UID={2};password={3};Port={4};CharSet=utf8;Persist Security Info=True",
              Model.Server, Model.Database, Model.UserName, Model.Password, Model.Port);
            using(var db = new ContextTest(constr))
            {
                try
                {
                db.Open();
                    MessageBox.Show("Connection Is Ok");
                    db.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
