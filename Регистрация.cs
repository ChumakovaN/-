using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Collections;

namespace Недвижимость
{
    public partial class Регистрация : Form
    {
        readonly Class1 db = new Class1();
        public Регистрация()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var logiUser = textBox3.Text;
            var passUser = textBox4.Text;
            var fio = textBox1.Text;
            var Pochta = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string querystring = $"select * from Пользователи where Логин = '{textBox3.Text}'";
            SqlCommand cmd = new SqlCommand(querystring, db.getConnection());
            adapter.SelectCommand = cmd;

            adapter.Fill(Table);

            db.openConnection();

            if (Table.Rows.Count == 0)
            {
                SqlCommand insertCommand = new SqlCommand($"insert into Пользователи( Логин, Пароль, ФИО, Почта) values( '{logiUser}', '{passUser}' ,'{fio}', '{Pochta}')", db.getConnection());

                if (insertCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Регистрация прошла успешно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Недвижимость недвижимость = new Недвижимость();
                    недвижимость.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Такой логин уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            db.closeConnection();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Авторизация а = new Авторизация();
            а.ShowDialog();
        }

        private void Регистрация_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
