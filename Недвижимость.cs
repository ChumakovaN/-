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

namespace Недвижимость
{
    public partial class Недвижимость : Form
    {
        Class1 db = new Class1();
        public Недвижимость()
        {
            InitializeComponent();
            numericUpDown1.Maximum = 0;
        }

        private void Home()
        {
            try
            {
                using (SqlCommand command = new SqlCommand($"select Год,Цена from {comboBox1.Text} where Область = '{comboBox4.Text}' and Этаж = '{numericUpDown1.Value}' and КолКомнат = '{comboBox2.Text}'", db.getConnection()))
                {
                    db.openConnection();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader["Год"].ToString());
                            listBox2.Items.Add(reader["Цена"].ToString());
                            chart1.Series[0].Points.AddXY(reader["Год"], reader["Цена"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке программ: {ex.Message}");
            }
        }

        private void Spros()
        {
            try
            {
                using (SqlCommand command = new SqlCommand($"select Спрос.Вид, AVG({comboBox1.Text}.Спрос) as Спрос from {comboBox1.Text}, Спрос where Область = '{comboBox4.Text}' and Этаж = '{numericUpDown1.Value}' and КолКомнат = '{comboBox2.Text}' and Спрос.id = {comboBox1.Text}.Спрос Group By Спрос.Вид", db.getConnection()))
                {
                    db.openConnection();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            textBox1.Text = reader["Вид"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке программы: {ex.Message}");
            }
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                chart1.Series[0].Points.Clear();
                Home();
                Spros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке программ: {ex.Message}");
            }
        }

        private void Недвижимость_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
