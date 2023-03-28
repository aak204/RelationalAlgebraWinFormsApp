using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RelationalAlgebraWinFormsApp
{
    internal class RelationalOperations
    {

        public static Table Union(Table table1, Table table2)
        {
            List<string> names = new List<string>();

            foreach (string name in table1.columsNames)
            {
                if (!names.Contains(name))
                {
                    names.Add(name);
                }
            }
            foreach (string name in table2.columsNames)
            {
                if (!names.Contains(name))
                {
                    names.Add(name);
                }
            }

            Table result = new Table(names.ToArray());

            foreach (object[] row in table1.data_obj)
            {
                object[] resultRow = new object[names.Count];
                for (int i = 0; i < table1.columsNames.Length; i++)
                {
                    int index = names.IndexOf(table1.columsNames[i]);
                    if (index != -1 && i < row.Length)
                    {
                        resultRow[index] = row[i];
                    }
                }
                result.data_obj.Add(resultRow);
            }

            foreach (object[] row in table2.data_obj)
            {
                object[] resultRow = new object[names.Count];
                for (int i = 0; i < table2.columsNames.Length; i++)
                {
                    int index = names.IndexOf(table2.columsNames[i]);
                    if (index != -1 && i < row.Length)
                    {
                        resultRow[index] = row[i];
                    }
                }
                result.data_obj.Add(resultRow);
            }

            return result;
        }

        public static Table Intersection(Table table1, Table table2)
        {
            // Найдем общие столбцы по индексу
            List<int> commonColumns = new List<int>();
            for (int i = 0; i < table1.columsNames.Length; i++)
            {
                for (int j = 0; j < table2.columsNames.Length; j++)
                {
                    if (table1.columsNames[i] == table2.columsNames[j])
                    {
                        commonColumns.Add(i);
                        break;
                    }
                }
            }

            // Результат будет только с общими столбцами
            Table result = new Table(commonColumns.Select(i => table1.columsNames[i]).ToArray());

            // Найдем совпадающие строки в обеих таблицах
            foreach (object[] row1 in table1.data_obj)
            {
                foreach (object[] row2 in table2.data_obj)
                {
                    bool match = true;
                    for (int i = 0; i < commonColumns.Count; i++)
                    {
                        int index = commonColumns[i];
                        if (!row1[index].Equals(row2[index]))
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        object[] resultRow = new object[commonColumns.Count];
                        for (int i = 0; i < commonColumns.Count; i++)
                        {
                            int index = commonColumns[i];
                            resultRow[i] = row1[index];
                        }
                        result.data_obj.Add(resultRow);
                    }
                }
            }

            return result;
        }

        public static Table Difference(Table table1, Table table2, int subtractFromTable)
        {
            List<string> names = new List<string>();

            if (subtractFromTable == 1)
            {
                foreach (var item in table1.columsNames)
                {
                    names.Add(item);
                }
            }
            else
            {
                foreach (var item in table2.columsNames)
                {
                    names.Add(item);
                }
            }

            Table result = new Table(names.ToArray());
            foreach (var row1 in table1.data_obj)
            {
                if (IsEmptyRow(row1))
                {
                    continue;
                }

                bool found = false;
                foreach (var row2 in table2.data_obj)
                {
                    if (row1.SequenceEqual(row2))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found && subtractFromTable == 1)
                {
                    result.data_obj.Add(row1);
                }
            }

            if (subtractFromTable == 2)
            {
                foreach (var row2 in table2.data_obj)
                {
                    if (IsEmptyRow(row2))
                    {
                        continue;
                    }

                    bool found = false;
                    foreach (var row1 in table1.data_obj)
                    {
                        if (row2.SequenceEqual(row1))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        result.data_obj.Add(row2);
                    }
                }
            }

            return result;
        }



        public static Table CartesianProduct(Table table1, Table table2)
        {
            // Create the result table with the combined column names
            List<string> names = new List<string>();
            foreach (string name in table1.columsNames)
            {
                names.Add(name + "1");
            }
            foreach (string name in table2.columsNames)
            {
                names.Add(name + "2");
            }
            Table result = new Table(names.ToArray());

            // Find the Cartesian product of the two tables
            foreach (var row1 in table1.data_obj)
            {
                if (IsEmptyRow(row1))
                {
                    continue;
                }
                foreach (var row2 in table2.data_obj)
                {
                    if (IsEmptyRow(row2))
                    {
                        continue;
                    }

                    object[] resultRow = new object[names.Count];
                    for (int i = 0; i < table1.columsNames.Length; i++)
                    {
                        if (i < row1.Length)
                        {
                            resultRow[i] = row1[i];
                        }
                        else
                        {
                            resultRow[i] = null;
                        }
                    }
                    for (int i = 0; i < table2.columsNames.Length; i++)
                    {
                        if (i < row2.Length)
                        {
                            resultRow[table1.columsNames.Length + i] = row2[i];
                        }
                        else
                        {
                            resultRow[table1.columsNames.Length + i] = null;
                        }
                    }
                    result.data_obj.Add(resultRow);
                }
            }

            return result;
        }

        private static bool IsEmptyRow(object[] row)
        {
            foreach (var cell in row)
            {
                if (cell != null && !string.IsNullOrEmpty(cell.ToString()))
                {
                    return false;
                }
            }
            return true;
        }


    }
}
