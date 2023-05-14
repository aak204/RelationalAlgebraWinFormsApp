using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationalAlgebraWinFormsApp
{
    internal class RelationalOperations
    {
        public MainForm.OperationDelegate OperationDelegate
        {
            get => default;
            set
            {
            }
        }

        public MainForm MainForm
        {
            get => default;
            set
            {
            }
        }

        public static Table Union(Table table1, Table table2)
        {
            if (table1.columnsNames.Length != table2.columnsNames.Length ||
                !Enumerable.SequenceEqual(table1.columnsNames, table2.columnsNames))
            {
                MessageBox.Show("Имена атрибутов или количество отличаются! Будут пустые ячейки", "Предупреждение",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            List<string> names = new List<string>();

            foreach (string name in table1.columnsNames)
            {
                if (!names.Contains(name))
                {
                    names.Add(name);
                }
            }
            foreach (string name in table2.columnsNames)
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
                for (int i = 0; i < table1.columnsNames.Length; i++)
                {
                    int index = names.IndexOf(table1.columnsNames[i]);
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
                for (int i = 0; i < table2.columnsNames.Length; i++)
                {
                    int index = names.IndexOf(table2.columnsNames[i]);
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
            if (table1.columnsNames.Length != table2.columnsNames.Length ||
                !Enumerable.SequenceEqual(table1.columnsNames, table2.columnsNames))
            {
                MessageBox.Show("Имена атрибутов или количество отличаются! Будут пустые ячейки", "Предупреждение",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Найдем общие столбцы по индексу
            List<int> commonColumns = new List<int>();
            for (int i = 0; i < table1.columnsNames.Length; i++)
            {
                for (int j = 0; j < table2.columnsNames.Length; j++)
                {
                    if (table1.columnsNames[i] == table2.columnsNames[j])
                    {
                        commonColumns.Add(i);
                        break;
                    }
                }
            }

            // Результат будет только с общими столбцами
            Table result = new Table(commonColumns.Select(i => table1.columnsNames[i]).ToArray());

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
            if (table1.columnsNames.Length != table2.columnsNames.Length ||
                !Enumerable.SequenceEqual(table1.columnsNames, table2.columnsNames))
            {
                MessageBox.Show("Имена атрибутов или количество отличаются! Будут пустые ячейки", "Предупреждение",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            List<string> names = new List<string>();

            if (subtractFromTable == 1)
            {
                foreach (var item in table1.columnsNames)
                {
                    names.Add(item);
                }
            }
            else
            {
                foreach (var item in table2.columnsNames)
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
            foreach (string name in table1.columnsNames)
            {
                names.Add(name + "1");
            }
            foreach (string name in table2.columnsNames)
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
                    for (int i = 0; i < table1.columnsNames.Length; i++)
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
                    for (int i = 0; i < table2.columnsNames.Length; i++)
                    {
                        if (i < row2.Length)
                        {
                            resultRow[table1.columnsNames.Length + i] = row2[i];
                        }
                        else
                        {
                            resultRow[table1.columnsNames.Length + i] = null;
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


        public static Table InnerJoin(Table table1, Table table2, string columnName)
        {
            // Внутреннее соединение
            return Join(table1, table2, columnName, "inner");
        }

        public static Table LeftJoin(Table table1, Table table2, string columnName)
        {
            // Левое соединение
            return Join(table1, table2, columnName, "left");
        }

        public static Table RightJoin(Table table1, Table table2, string columnName)
        {
            // Правое соединение
            return Join(table1, table2, columnName, "right");
        }

        private static Table Join(Table table1, Table table2, string columnName, string joinType)
        {
            // Общая логика для всех типов соединений
            if (!table1.columnsNames.Contains(columnName) || !table2.columnsNames.Contains(columnName))
            {
                MessageBox.Show($"Атрибут '{columnName}' не найден в обеих таблицах", "Предупреждение",
   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int column1Index = Array.IndexOf(table1.columnsNames, columnName);
            int column2Index = Array.IndexOf(table2.columnsNames, columnName);

            List<string> table1ColumnNames = table1.columnsNames.Where((name, index) => index != column1Index).ToList();
            List<string> table2ColumnNames = table2.columnsNames.Where((name, index) => index != column2Index).ToList();

            List<string> resultColumnNames = new List<string>();
            resultColumnNames.AddRange(table1ColumnNames.Select(name => $"{name}"));
            resultColumnNames.AddRange(table2ColumnNames.Select(name => $"{name}"));
            Table result = new Table(resultColumnNames.ToArray());

            foreach (var row1 in table1.data_obj)
            {
                object column1Value = row1[column1Index];
                bool row1Matched = false;
                foreach (var row2 in table2.data_obj)
                {
                    if (column1Value.Equals(row2[column2Index]))
                    {
                        row1Matched = true;
                        object[] resultRow = GetCombinedRow(table1, table2, row1, row2, column1Index, column2Index);
                        result.data_obj.Add(resultRow);
                    }
                }

                if (!row1Matched && joinType == "left")
                {
                    object[] resultRow = GetCombinedRow(table1, table2, row1, null, column1Index, column2Index);
                    result.data_obj.Add(resultRow);
                }
            }

            if (joinType == "right")
            {
                foreach (var row2 in table2.data_obj)
                {
                    object column2Value = row2[column2Index];
                    bool row2Matched = table1.data_obj.Any(row1 => row1[column1Index].Equals(column2Value));

                    if (!row2Matched)
                    {
                        object[] resultRow = GetCombinedRow(table1, table2, null, row2, column1Index, column2Index);
                        result.data_obj.Add(resultRow);
                    }
                }
            }

            return result;
        }

        private static object[] GetCombinedRow(Table table1, Table table2, object[] row1, object[] row2, int column1Index, int column2Index)
        {
            object[] resultRow = new object[table1.columnsNames.Length - 1 + table2.columnsNames.Length - 1];
            int resultIndex = 0;
            for (int i = 0; i < table1.columnsNames.Length; i++)
            {
                if (i != column1Index)
                {
                    resultRow[resultIndex] = row1 != null ? row1[i] : null;
                    resultIndex++;
                }
            }

            for (int i = 0; i < table2.columnsNames.Length; i++)
            {
                if (i != column2Index)
                {
                    resultRow[resultIndex] = row2 != null ? row2[i] : null;
                    resultIndex++;
                }
            }

            return resultRow;
        }


        //public static Table Select(Table table1, Table table2, int TableIdx, string columnNames)
        //{
        //    Table table = new Table("");
        //    if (TableIdx == 1)
        //        table = table1;
        //    else
        //        table = table2;

        //    // Create a new table with the selected columns
        //    Table result = new Table(columnNames);

        //    // Copy the selected data from the original table
        //    foreach (object[] row in table.data_obj)
        //    {
        //        object[] selectedData = new object[columnNames.Length];
        //        for (int i = 0; i < columnNames.Length; i++)
        //        {
        //            int index = table.GetColumnIndex(columnNames[i]);
        //            selectedData[i] = row[index];
        //        }
        //        result.data_obj.Add(selectedData);
        //    }

        //    return result;
        //}

        //public static Table Project(Table table1, Table table2, int TableIdx, string columnNames)
        //{
        //    Table table = new Table("");
        //    if (TableIdx == 1)
        //        table = table1;
        //    else
        //        table = table2;
        //    // Create a new table with the projected columns
        //    Table result = new Table(columnNames);

        //    // Copy the projected data from the original table
        //    foreach (object[] row in table.data_obj)
        //    {
        //        object[] projectedData = new object[columnNames.Length];
        //        for (int i = 0; i < columnNames.Length; i++)
        //        {
        //            int index = table.GetColumnIndex(columnNames[i]);
        //            projectedData[i] = row[index];
        //        }
        //        result.data_obj.Add(projectedData);
        //    }

        //    return result;
        //}

        public static Table Divide(Table table1, Table table2)
        {
            // Check that table2 has a subset of columns from table1
            if (!table2.columnsNames.All(column => table1.columnsNames.Contains(column)))
            {
                MessageBox.Show("Все столбцы нижнего отношения должны присутствовать в верхнем отношении", "Предупреждение",
   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Create the result table with the columns not present in table2
            string[] resultColumns = table1.columnsNames.Except(table2.columnsNames).ToArray();
            Table result = new Table(resultColumns);

            // For each unique combination of values in the resultColumns of table1
            foreach (var group in table1.data_obj.Select(row => row.Where((val, index) => !table2.columnsNames.Contains(table1.columnsNames[index])).ToArray()).Distinct())
            {
                // For each row in table2
                bool isMaximal = table2.data_obj.All(row2 =>
                {
                    // Create a combined row with the values from group and row2
                    var combinedRow = new object[table1.columnsNames.Length];
                    int i = 0;
                    foreach (string column in table1.columnsNames)
                    {
                        if (table2.columnsNames.Contains(column))
                        {
                            combinedRow[i] = row2[Array.IndexOf(table2.columnsNames, column)];
                        }
                        else
                        {
                            combinedRow[i] = group[Array.IndexOf(resultColumns, column)];
                        }
                        i++;
                    }

                    // Check if this combined row is present in table1
                    return table1.data_obj.Any(row1 => row1.SequenceEqual(combinedRow));
                });

                // If the group is maximal (i.e., it can be combined with every row in table2 and still be present in table1), add it to the result
                if (isMaximal)
                {
                    result.data_obj.Add(group);
                }
            }

            return result;

        }
    }
}
