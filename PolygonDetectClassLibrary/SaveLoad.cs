using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonDetectClassLibrary
{
    internal class SaveLoad
    {
        internal bool Save(String FileName, Points points)
        {
            Point[] arrayPoints = points.ToArray();
            DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
            DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
            dt.TableName = "DataList"; // название таблицы
            dt.Columns.Add("Number"); // название колонок
            dt.Columns.Add("X");
            dt.Columns.Add("Y");
            ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

            for (int i = 0; i < arrayPoints.Length; i++) // пока в dataGridView1 есть строки
            {
                DataRow row = ds.Tables["DataList"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                row["Number"] = (i + 1).ToString();  //в столбец этой строки заносим данные из первого столбца dataGridView1
                row["X"] = arrayPoints[i].X.ToString(); // то же самое со вторыми столбцами
                row["Y"] = arrayPoints[i].Y.ToString(); //то же самое с третьими столбцами
                ds.Tables["DataList"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
            }
            ds.WriteXml(FileName);

            return true;
        }

        public int[,] Open(String FileName, Points points)
        {
            Point[] arrayPoints = points.ToArray();

            DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
            ds.ReadXml(FileName); // записываем в него XML-данные из файла

            int[,] XY = new int[ds.Tables["DataList"].Rows.Count, 2];

            for(int i = 0; i< XY.GetLength(0); i++) // пока в dataGridView1 есть строки
            {
                DataRow[] itemXY = ds.Tables["DataList"].Select();
                XY[i,0] = Convert.ToInt32(itemXY[i].ItemArray[1]); // 1-x
                XY[i, 1] = Convert.ToInt32(itemXY[i].ItemArray[2]); // 2-y
            }

            return XY;
        }
    }
}
