using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RelationalAlgebraWinFormsApp
{

    public class Table
    {
        public string[] columnsNames;
        public List<object[]> data_obj = new List<object[]>();

        string[] Names = File.ReadAllLines("Names.txt");
        string[] Companies = File.ReadAllLines("Companies.txt");

        private static readonly Random Random = new Random();

        public Table(params string[] names)
        {
            columnsNames = new string[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                columnsNames[i] = names[i];
            }
        }

        public MainForm MainForm
        {
            get => default;
            set
            {
            }
        }

        public void FillInAutomatically()
        {
            for (int i = 0; i < 13; i++)
            {
                int id = Random.Next(1, 512);
                string name = Names[Random.Next(0, Names.Length)];
                string company = Companies[Random.Next(0, Companies.Length)];

                data_obj.Add(new object[] { id, name, company });
            }
        }

        public void FillInManual()
        {
            for (int i = 0; i < 13; i++)
            {
                data_obj.Add(new object[] { 0, "", "" });
            }
        }

        public void AddRow()
        {
            object[] row = new object[data_obj.Count > 0 ? data_obj[0].Length : 3];
            row[0] = 0;
            for (int i = 1; i < row.Length; i++)
                row[i] = "";
            data_obj.Add(row);
        }

        public void RewriteRow(object[] rowValues, int RowIdx)
        {
            if (rowValues == null || rowValues.Length == 0) throw new ArgumentException();
            int index = RowIdx;
            if (index < 0 || index >= data_obj.Count) throw new ArgumentOutOfRangeException();
            data_obj[index] = rowValues;
        }

        public void AddColumn(string columnName)
        {
            string[] newColumnNames = new string[columnsNames.Length + 1];

            Array.Copy(columnsNames, newColumnNames, columnsNames.Length);

            newColumnNames[columnsNames.Length] = columnName;

            columnsNames = newColumnNames;

            for (int i = 0; i < data_obj.Count; i++)
            {
                object[] row = data_obj[i];
                Array.Resize(ref row, columnsNames.Length);
                row[columnsNames.Length - 1] = ""; // заполнить пустой строкой
                data_obj[i] = row;
            }
        }



        public bool IsEmpty()
        {
            return data_obj.Count == 0;
        }

        public int CheckId(int id)
        {
            for (int i = 0; i < data_obj.Count; i++)
            {
                if ((int)data_obj[i][0] == id)
                {
                    return i;
                }
            }
            return -1; // ID not found
        }

        public int GetId(int excludeIndex)
        {
            for (int i = 0; i < data_obj.Count; i++)
            {
                if (i != excludeIndex)
                {
                    return (int)data_obj[i][0];
                }
            }
            return -1;
        }

        public int GetColumnIndex(string columnName)
        {
            for (int i = 0; i < columnsNames.Length; i++)
            {
                if (columnsNames[i].Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1; // Если столбец не найден, возвращаем -1
        }

        public bool HasSameColumns(Table other)
        {
            if (columnsNames.Length != other.columnsNames.Length)
            {
                return false;
            }

            for (int i = 0; i < columnsNames.Length; i++)
            {
                if (!columnsNames[i].Equals(other.columnsNames[i], StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        public List<object[]> GetRows()
        {
            return data_obj;
        }
    }
}
