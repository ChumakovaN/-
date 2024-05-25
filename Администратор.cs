using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Недвижимость
{
    public partial class Администратор : Form
    {
        Class1 db = new Class1();
        public Администратор()
        {
            InitializeComponent();
            CreateColumns();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Область", "Область");
            dataGridView1.Columns.Add("Этаж", "Этаж");
            dataGridView1.Columns.Add("КолКомнат", "Кол-во комнат");
            dataGridView1.Columns.Add("Год", "Год");
            dataGridView1.Columns.Add("Цена", "Цена");
        }

        private void CreateRows(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetString(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), record.GetInt32(4));
        }

        private void Appartment(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"select Область, Этаж, КолКомнат, Год, Цена from Квартира";

            SqlCommand cmd = new SqlCommand(query, db.getConnection());

            db.openConnection();

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CreateRows(dgw, sqlDataReader);
            }
            sqlDataReader.Close();
            db.closeConnection();
        }

        private void Home(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"select Область, Этаж, КолКомнат, Год, Цена from Дом";

            SqlCommand cmd = new SqlCommand(query, db.getConnection());

            db.openConnection();

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CreateRows(dgw, sqlDataReader);
            }
            sqlDataReader.Close();
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p();
        }

        public void p()
        {
            var r = new Random();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string querystring = $"select * from {comboBox1.Text}";
            SqlCommand cmd = new SqlCommand(querystring, db.getConnection());
            adapter.SelectCommand = cmd;

            adapter.Fill(Table);

            db.openConnection();

            if (Table.Rows.Count == 0)
            {
                SqlCommand insertCommand = new SqlCommand($"insert into {comboBox1.Text}(Область, Этаж, КолКомнат, Год, Цена,Спрос) values( '{comboBox4.Text}', '{numericUpDown1.Value}' ,'{comboBox2.Text}', '{textBox1.Text}', '{textBox2.Text}', '{r.Next(1, 4)}')", db.getConnection());

                if (insertCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные добавлены!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            db.closeConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Appartment(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home(dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                numericUpDown1.Minimum = 1;
                numericUpDown1.Maximum = 3;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                numericUpDown1.Minimum = 1;
                numericUpDown1.Maximum = 9;
            }
            else
            {
                numericUpDown1.Minimum = 0;
                numericUpDown1.Maximum = 0;
            }
        }

        private void Администратор_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Заполните пустые поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Updates();
            }
        }

        public void Updates()
        {
            string query = $"update {comboBox1.Text} set Год = '{textBox1.Text}', Цена = '{textBox2.Text}' where КолКомнат = '{comboBox2.Text}' and Область = '{comboBox4.Text}' and Год = '{textBox1.Text}'";

            SqlCommand command = new SqlCommand(query, db.getConnection());

            try
            {
                db.openConnection();

                int rowsAffected = command.ExecuteNonQuery();

                db.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
    }
}
