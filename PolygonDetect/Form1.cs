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
        
        //       List<Size> arrayPF = new List<Size>();


        public Form1()
        {
            InitializeComponent();
            pictureBox1.Width = 1196;
            pictureBox1.Height = 681;
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);//Создаём рисунок
            //bmp будет храниться в оперативной памяти, не на HDD.
            l = new Logic(bmp, dataGridView1, pictureBox1); // Инициализируем объект отвечающий за логику программы
            
          
           // d = new Draw(bmp, dataGridView1, pictureBox1); // Инициализируем объект отвечающий за отрисовку данных на форме
            pictureBox1.Width = 824;
            pictureBox1.Height = 421;

            ElementsEnebleTrueFalse(false);


        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (selectorDo)
            {
                case ("Draw"):
                    l.Drawing(e.X, e.Y);
                    break;

                case ("Test"):
                    l.Calculate(e.X, e.Y);
                    break;

                default:
                    break;

            }
        }

        private void buttonDraw_Click(object sender, EventArgs e)
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
                l.ClearDate();
                ElementsEnebleTrueFalse(true);
            }
        }



        private void buttonTest_Click(object sender, EventArgs e)
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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectorDo == "Draw" || selectorDo == "Test")
            {
                textBoxX.Text = e.X.ToString();
                textBoxY.Text = e.Y.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (selectorDo == "Draw")
                l.StopDrawing();

            selectorDo = "0";
            ElementsEnebleTrueFalse(false);
            SaveFileDialog saveFileDialog = new SaveFileDialog(); // открытие окна для праметров сохранения файла
            saveFileDialog.Filter = "XML files(.xml)|*.xml|all Files(*.*)|*.*"; // задаем тип сохраняемого файла
            saveFileDialog.FileName = "Шаблон"; // задаем название сохраняемого файла
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                l.Save(saveFileDialog.FileName);
        }

        private void buttonInput_Click(object sender, EventArgs e)
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

                default:
                    break;

            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (selectorDo == "Draw")
                l.StopDrawing();

            selectorDo = "0";
            ElementsEnebleTrueFalse(false);

            OpenFileDialog openFileDialog = new OpenFileDialog(); // открытие окна для праметров сохранения файла
            openFileDialog.Filter = "XML files(.xml)|*.xml|all Files(*.*)|*.*"; // задаем тип сохраняемого файла
            openFileDialog.FileName = "Шаблон"; // задаем название сохраняемого файла
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                l.Open(openFileDialog.FileName);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            l.ClearDate();
            selectorDo = "0";
            ElementsEnebleTrueFalse(false);
        }

        private void ElementsEnebleTrueFalse(bool enable)
        {
            if (enable)
            {
                textBoxX.Text = null;//подсказка
                textBoxY.Text = null;//подсказка
                
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
