using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using Недвижимость;

namespace UnitTest
{
    [TestClass]
    public class FormTests
    {
        [TestMethod]
        public void TestFormVisibility()
        {
            // Создать форму
            Form form = new Form();

            // Запустить форму
            form.ShowDialog();

            // Утвердить, что форма видима
            Assert.IsFalse(form.Visible);
        }

        [TestMethod]
        public void TestControlPresence()
        {
            // Создать форму
            Авторизация form = new Авторизация();

            // Добавить кнопку на форму
            Button button = new Button();
            form.Controls.Add(button);

            // Утвердить, что кнопка присутствует на форме
            Assert.IsTrue(form.Controls.Contains(button));
        }

        [TestMethod]
        public void TestButtonClick()
        {
            // Создать форму
            Form form = new Form();

            // Добавить кнопку на форму и подписаться на ее событие Click
            Button button = new Button();
            button.Click += Button_Click;
            form.Controls.Add(button);

            // Запустить форму
            form.ShowDialog();

            // Нажать кнопку
            button.PerformClick();

            // Утвердить, что событие Click было вызвано
            Assert.IsTrue(buttonClicked);
        }

        private bool buttonClicked = false;

        private void Button_Click(object sender, EventArgs e)
        {
            buttonClicked = true;
        }

        [TestMethod]
        public void TestDataInput()
        {
            // Создать форму
            Form form = new Form();

            // Добавить текстовое поле на форму
            TextBox textBox = new TextBox();
            form.Controls.Add(textBox);

            // Ввести текст в текстовое поле
            textBox.Text = "Test";

            // Утвердить, что текст был введен в текстовое поле
            Assert.AreEqual("Test", textBox.Text);
        }

    }
}
