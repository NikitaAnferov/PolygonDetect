using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using PolygonDetectClassLibrary;

namespace PolygonDetect
{
    public partial class Form1 : Form
    {
        String selectorDo = "0";
        Logic l;
        int pictureHeight;
        int pictureWidth;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);//Создаём рисунок bmp будет храниться в оперативной памяти, не на HDD.
            l = new Logic(bmp, dataGridView1, pictureBox1); // Инициализируем объект отвечающий за логику программы
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

            ElementsEnebleTrueFalse(false);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureWidth = pictureBox1.Width;
            pictureHeight = pictureBox1.Height;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                switch (selectorDo)
                {
                    case ("Draw"):
                        l.Drawing(e.X, e.Y);
                        break;

                    case ("Test"):
                        l.Calculate(e.X, e.Y);
                        break;
                }
            
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            try
            {
                string drawClick = "Draw";
                if (selectorDo == drawClick)
                {
                    selectorDo = "0";
                    l.StopDrawing();
                    ElementsEnebleTrueFalse(false);

                }
                else
                {
                    selectorDo = drawClick;
                    l.ClearData();
                    ElementsEnebleTrueFalse(true);
                    textBoxX.Focus();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void buttonTest_Click(object sender, EventArgs e)
        {
            try
            {
                string testClick = "Test";
                if (selectorDo == testClick)
                {
                    selectorDo = "0";
                    ElementsEnebleTrueFalse(false);
                }

                else
                {
                    l.StopDrawing();
                    selectorDo = testClick;
                    ElementsEnebleTrueFalse(true);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (selectorDo == "Draw" || selectorDo == "Test")
                {
                    textBoxX.Text = e.X.ToString();
                    textBoxY.Text = e.Y.ToString();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectorDo == "Draw")
                    l.StopDrawing();

                selectorDo = "0";
                ElementsEnebleTrueFalse(false);

                SaveFileDialog saveFileDialog = l.LoadSaveFileDialog();

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    l.Save(saveFileDialog.FileName);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxX.Text != "" && textBoxY.Text != "")
                {
                    int X = Convert.ToInt32(textBoxX.Text);
                    int Y = Convert.ToInt32(textBoxY.Text);

                    switch (selectorDo)
                    {
                        case ("Draw"):
                            l.Drawing(X, Y);
                            break;

                        case ("Test"):
                            l.Calculate(X, Y);
                            break;
                    }

                    textBoxX.Clear();
                    textBoxY.Clear();
                    textBoxX.Focus();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    e.Handled = true;
                    buttonInput_Click(sender, e);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectorDo == "Draw")
                    l.StopDrawing();

                selectorDo = "0";
                ElementsEnebleTrueFalse(false);

                OpenFileDialog openFileDialog = l.LoadOpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    l.Open(openFileDialog.FileName);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                l.ClearData();
                selectorDo = "0";
                ElementsEnebleTrueFalse(false);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxX_KeyPress(object sender, KeyPressEventArgs e)
        {
            try { ValidatorValue(e); }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void textBoxY_KeyPress(object sender, KeyPressEventArgs e)
        {
            try { ValidatorValue(e); }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxX_KeyUp(object sender, KeyEventArgs e)
        {
            try { VlidatorValueMax(textBoxX, pictureWidth); }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxY_KeyUp(object sender, KeyEventArgs e)
        {
            try { VlidatorValueMax(textBoxY, pictureHeight); }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ValidatorValue(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\r')
                {
                    e.Handled = true;
                    SelectNextControl(ActiveControl, true, true, true, true);
                }
            }
        }
        private void VlidatorValueMax(TextBox textBox, int max)
        {
            if (textBox.Text != "")
                if (Convert.ToInt32(textBox.Text) > max)
                    textBox.Text = max.ToString();

            textBox.SelectionStart = textBox.Text.Length;
        }

        private void ElementsEnebleTrueFalse(bool enable)
        {
            if (enable)
            {
                textBoxX.Text = null;
                textBoxY.Text = null;
            }
            else
            {
                textBoxX.Text = "X";//подсказка
                textBoxY.Text = "Y";//подсказка
            }

            textBoxX.Enabled = enable;
            textBoxY.Enabled = enable;
            buttonInput.Enabled = enable;

        }
    }
}
