using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Недвижимость
{
    public partial class Авторизация : Form
    {
        readonly Class1 class1 = new Class1();
        public Авторизация()
        {
            InitializeComponent();

            textBox2.UseSystemPasswordChar = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var logiUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select Логин,Пароль from Пользователи where Логин = '{logiUser}' and Пароль = '{passUser}'";
            SqlCommand command = new SqlCommand(querystring, class1.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                if (logiUser == "admin")
                {
                    this.Hide();
                    Администратор p = new Администратор();
                    p.Show();
                }
                else
                {
                    this.Hide();
                    Недвижимость ps = new Недвижимость();
                    ps.Show();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Регистрация p = new Регистрация();
            p.Show();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
                textBox2.UseSystemPasswordChar = false;
            else 
                textBox2.UseSystemPasswordChar = true;
        }


        private void Авторизация_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
