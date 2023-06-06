using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationalAlgebraWinFormsApp
{
    public class RelationalOperations
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

        public class LambdaComparer<T> : IEqualityComparer<T>
        {
            private readonly Func<T, T, bool> _lambdaComparer; // функция для сравнения двух объектов типа T
            private readonly Func<T, int> _lambdaHash; // функция для вычисления хэш-кода объекта типа T

            // конструктор, принимающий одну функцию-сравнитель и присваивающий функции-хэшу значение по умолчанию
            public LambdaComparer(Func<T, T, bool> lambdaComparer) :
                this(lambdaComparer, o => 0)
            {
            }

            // конструктор, принимающий обе функции-сравнитель и хэш
            public LambdaComparer(Func<T, T, bool> lambdaComparer, Func<T, int> lambdaHash)
            {
                if (lambdaComparer == null) // проверка на null для функции-сравнителя
                    throw new ArgumentNullException("lambdaComparer");
                if (lambdaHash == null) // проверка на null для функции-хэша
                    throw new ArgumentNullException("lambdaHash");

                _lambdaComparer = lambdaComparer;
                _lambdaHash = lambdaHash;
            }

            // реализация интерфейсного метода Equals
            public bool Equals(T x, T y)
            {
                return _lambdaComparer(x, y);
            }

            // реализация интерфейсного метода GetHashCode
            public int GetHashCode(T obj)
            {
                return _lambdaHash(obj);
            }
        }


        public static Table Union(Table table1, Table table2) // функция объединения двух таблиц
        {
            // проверка на равенство количества и названий столбцов
            if (table1.columnsNames.Length != table2.columnsNames.Length ||
                !Enumerable.SequenceEqual(table1.columnsNames, table2.columnsNames))
            {
                MessageBox.Show("Имена атрибутов или количество отличаются! Будут пустые ячейки.", "Предупреждение",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // создание списка названий столбцов
            List<string> names = new List<string>();

            // добавление названий столбцов из первой и второй таблиц
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

            // создание новой таблицы с полученными названиями столбцов
            Table result = new Table(names.ToArray());

            // добавление строк из первой и второй таблиц в результирующую таблицу
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

            return result; // возврат результата
        }

        public static Table Intersection(Table table1, Table table2) // функция пересечения двух таблиц
        {
            // проверка на равенство количества и названий столбцов
            if (table1.columnsNames.Length != table2.columnsNames.Length ||
                !Enumerable.SequenceEqual(table1.columnsNames, table2.columnsNames))
            {
                MessageBox.Show("Имена атрибутов или количество отличаются! Будут только общие столбцы.", "Предупреждение",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // поиск общих столбцов
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

            // создание таблицы только с общими столбцами
            Table result = new Table(commonColumns.Select(i => table1.columnsNames[i]).ToArray());

            // поиск и добавление общих строк
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

            return result; // возврат результата
        }

        public static Table Difference(Table table1, Table table2) // функция вычисления разности двух таблиц
        {
            // проверка на равенство количества и названий столбцов
            if (table1.columnsNames.Length != table2.columnsNames.Length ||
                !Enumerable.SequenceEqual(table1.columnsNames, table2.columnsNames))
            {
                MessageBox.Show("Имена атрибутов или количество отличаются! Атрибуты будут из первого отношения.", "Предупреждение",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // создание списка названий столбцов из первой таблицы
            List<string> names = new List<string>();

            foreach (var item in table1.columnsNames)
            {
                names.Add(item);
            }

            // создание таблицы для результата
            Table result = new Table(names.ToArray());

            // вычисление разности
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

                if (!found)
                {
                    result.data_obj.Add(row1);
                }
            }

            return result; // возврат результата
        }

        // Функция для получения декартова произведения двух таблиц.
        public static Table CartesianProduct(Table table1, Table table2)
        {
            // Создаем список для хранения имен колонок новой таблицы.
            List<string> names = new List<string>();
            // Проходим по именам колонок первой таблицы.
            foreach (string name in table1.columnsNames)
            {
                // Добавляем имя колонки к именам новой таблицы с суффиксом "1".
                names.Add(name + "1");
            }
            // Аналогично для второй таблицы, но с суффиксом "2".
            foreach (string name in table2.columnsNames)
            {
                names.Add(name + "2");
            }
            // Создаем новую таблицу с полученными именами колонок.
            Table result = new Table(names.ToArray());

            // Проходим по всем строкам первой таблицы.
            foreach (var row1 in table1.data_obj)
            {
                // Если строка пуста, пропускаем ее.
                if (IsEmptyRow(row1))
                {
                    continue;
                }
                // Проходим по всем строкам второй таблицы.
                foreach (var row2 in table2.data_obj)
                {
                    // Если строка пуста, пропускаем ее.
                    if (IsEmptyRow(row2))
                    {
                        continue;
                    }

                    // Создаем новую строку для результатирующей таблицы.
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
                    // Добавляем строку в результатирующую таблицу.
                    result.data_obj.Add(resultRow);
                }
            }

            // Возвращаем результат.
            return result;
        }

        // Функция для проверки, является ли строка пустой.
        private static bool IsEmptyRow(object[] row)
        {
            // Проходим по всем элементам строки.
            foreach (var cell in row)
            {
                // Если ячейка не пуста, возвращаем false.
                if (cell != null && !string.IsNullOrEmpty(cell.ToString()))
                {
                    return false;
                }
            }
            // Если все ячейки пустые, возвращаем true.
            return true;
        }

        // Функция для выполнения внутреннего соединения двух таблиц.
        public static Table InnerJoin(Table table1, Table table2, string columnName)
        {
            // Вызываем общую функцию Join с типом "inner".
            return Join(table1, table2, columnName, "inner");
        }

        // Функция для выполнения левого соединения двух таблиц.
        public static Table LeftJoin(Table table1, Table table2, string columnName)
        {
            // Вызываем общую функцию Join с типом "left".
            return Join(table1, table2, columnName, "left");
        }

        // Функция для выполнения правого соединения двух таблиц.
        public static Table RightJoin(Table table1, Table table2, string columnName)
        {
            // Вызываем общую функцию Join с типом "right".
            return Join(table1, table2, columnName, "right");
        }

        // Функция для выполнения полного соединения двух таблиц.
        public static Table FullJoin(Table table1, Table table2, string columnName)
        {
            if (!table1.columnsNames.Contains(columnName) || !table2.columnsNames.Contains(columnName))
            {
                MessageBox.Show($"Атрибут '{columnName}' не найден в обеих таблицах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int column1Index = Array.IndexOf(table1.columnsNames, columnName);
            int column2Index = Array.IndexOf(table2.columnsNames, columnName);

            List<string> table1ColumnNames = table1.columnsNames.ToList();
            List<string> table2ColumnNames = table2.columnsNames.ToList();

            List<string> resultColumnNames = new List<string>();
            resultColumnNames.AddRange(table1ColumnNames);
            resultColumnNames.AddRange(table2ColumnNames);
            Table result = new Table(resultColumnNames.ToArray());

            foreach (var row1 in table1.data_obj)
            {
                object column1Value = row1[column1Index];
                bool row1Matched = table2.data_obj.Any(row2 => row2[column2Index].Equals(column1Value));

                if (row1Matched)
                {
                    foreach (var row2 in table2.data_obj)
                    {
                        if (column1Value.Equals(row2[column2Index]))
                        {
                            object[] resultRow = GetCombinedRowFull(table1, table2, row1, row2, column1Index, column2Index);
                            result.data_obj.Add(resultRow);
                        }
                    }
                }
                else
                {
                    object[] resultRow = GetCombinedRowFull(table1, table2, row1, null, column1Index, column2Index);
                    result.data_obj.Add(resultRow);
                }
            }

            foreach (var row2 in table2.data_obj)
            {
                object column2Value = row2[column2Index];
                bool row2Matched = table1.data_obj.Any(row1 => row1[column1Index].Equals(column2Value));

                if (!row2Matched)
                {
                    object[] resultRow = GetCombinedRowFull(table1, table2, null, row2, column1Index, column2Index);
                    result.data_obj.Add(resultRow);
                }
            }

            return result;
        }


        // Функция для создания результата полного соединения.
        private static object[] GetCombinedRowFull(Table table1, Table table2, object[] row1, object[] row2, int column1Index, int column2Index)
        {
            // Создаем новую строку для результатирующей таблицы.
            object[] resultRow = new object[table1.columnsNames.Length + table2.columnsNames.Length];
            int resultIndex = 0;

            for (int i = 0; i < table1.columnsNames.Length; i++)
            {
                resultRow[resultIndex] = row1 != null ? row1[i] : null;
                resultIndex++;
            }

            for (int i = 0; i < table2.columnsNames.Length; i++)
            {
                resultRow[resultIndex] = row2 != null ? row2[i] : null;
                resultIndex++;
            }

            // Возвращаем полученную строку.
            return resultRow;
        }

        private static Table Join(Table table1, Table table2, string columnName, string joinType)
        {
            // Общая логика для всех типов соединений
            // Проверяем наличие указанного столбца в обеих таблицах
            if (!table1.columnsNames.Contains(columnName) || !table2.columnsNames.Contains(columnName))
            {
                MessageBox.Show($"Атрибут '{columnName}' не найден в обеих таблицах.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Определяем индексы указанных столбцов в каждой таблице
            int column1Index = Array.IndexOf(table1.columnsNames, columnName);
            int column2Index = Array.IndexOf(table2.columnsNames, columnName);

            // Получаем имена столбцов каждой таблицы, за исключением указанного столбца
            List<string> table1ColumnNames = table1.columnsNames.Where((name, index) => index != column1Index).ToList();
            List<string> table2ColumnNames = table2.columnsNames.Where((name, index) => index != column2Index).ToList();

            // Создаем список имен столбцов результирующей таблицы
            List<string> resultColumnNames = new List<string>();
            resultColumnNames.AddRange(table1ColumnNames.Select(name => $"{name}"));
            resultColumnNames.AddRange(table2ColumnNames.Select(name => $"{name}"));

            // Создаем результирующую таблицу
            Table result = new Table(resultColumnNames.ToArray());

            // Итерируем по каждой строке первой таблицы
            foreach (var row1 in table1.data_obj)
            {
                // Получаем значение в указанном столбце
                object column1Value = row1[column1Index];
                // Используем для определения, нашел ли текущая строка соответствие во второй таблице
                bool row1Matched = false;

                // Итерируем по каждой строке второй таблицы
                foreach (var row2 in table2.data_obj)
                {
                    // Если значения в указанных столбцах совпадают
                    if (column1Value.Equals(row2[column2Index]))
                    {
                        // Обозначаем, что текущая строка нашла соответствие
                        row1Matched = true;
                        // Получаем комбинированную строку из двух таблиц
                        object[] resultRow = GetCombinedRow(table1, table2, row1, row2, column1Index, column2Index);
                        // Добавляем комбинированную строку в результирующую таблицу
                        result.data_obj.Add(resultRow);
                    }
                }

                // Если текущая строка не нашла соответствия и тип соединения - левое, добавляем эту строку в результат
                if (!row1Matched && joinType == "left")
                {
                    object[] resultRow = GetCombinedRow(table1, table2, row1, null, column1Index, column2Index);
                    result.data_obj.Add(resultRow);
                }
            }

            // Если тип соединения - правое, итерируем по каждой строке второй таблицы
            if (joinType == "right")
            {
                foreach (var row2 in table2.data_obj)
                {
                    // Получаем значение в указанном столбце
                    object column2Value = row2[column2Index];
                    // Используем для определения, нашла ли текущая строка соответствие в первой таблице
                    bool row2Matched = table1.data_obj.Any(row1 => row1[column1Index].Equals(column2Value));

                    // Если текущая строка не нашла соответствия, добавляем эту строку в результат
                    if (!row2Matched)
                    {
                        object[] resultRow = GetCombinedRow(table1, table2, null, row2, column1Index, column2Index);
                        result.data_obj.Add(resultRow);
                    }
                }
            }

            // Возвращаем результирующую таблицу
            return result;
        }

        // Этот метод объединяет две строки из двух таблиц в одну строку
        private static object[] GetCombinedRow(Table table1, Table table2, object[] row1, object[] row2, int column1Index, int column2Index)
        {
            // Создаем новую строку для результатирующей таблицы
            object[] resultRow = new object[table1.columnsNames.Length - 1 + table2.columnsNames.Length - 1];
            int resultIndex = 0;

            // Добавляем все значения из первой строки в результирующую строку, за исключением значения в указанном столбце
            for (int i = 0; i < table1.columnsNames.Length; i++)
            {
                if (i != column1Index)
                {
                    resultRow[resultIndex] = row1 != null ? row1[i] : null;
                    resultIndex++;
                }
            }

            // Добавляем все значения из второй строки в результирующую строку, за исключением значения в указанном столбце
            for (int i = 0; i < table2.columnsNames.Length; i++)
            {
                if (i != column2Index)
                {
                    resultRow[resultIndex] = row2 != null ? row2[i] : null;
                    resultIndex++;
                }
            }

            // Возвращаем результирующую строку
            return resultRow;
        }

        // Этот метод возвращает подмножество таблицы, в котором все значения указанного столбца удовлетворяют заданному условию
        public static Table Selection(Table table, string columnName, string operatorType, string threshold)
        {
            // Проверяем, существует ли указанный столбец в таблице
            if (!table.columnsNames.Contains(columnName))
            {
                MessageBox.Show($"Атрибут '{columnName}' не существует в таблице.");
                return null;
            }

            // Получаем индекс столбца
            int columnIndex = Array.IndexOf(table.columnsNames, columnName);

            // Создаем новую таблицу с теми же именами столбцов
            Table result = new Table(table.columnsNames);

            // Итерируем по каждой строке исходной таблицы
            foreach (Object[] row in table.data_obj)
            {
                if (columnName == "ID") // если это числовое поле
                {
                    int cellValue = Int32.Parse(row[columnIndex].ToString());
                    int thresholdValue = Int32.Parse(threshold);

                    switch (operatorType)
                    {
                        case ">":
                            if (cellValue > thresholdValue)
                                result.AddRowFun(row);
                            break;

                        case "<":
                            if (cellValue < thresholdValue)
                                result.AddRowFun(row);
                            break;

                        case "=":
                            if (cellValue == thresholdValue)
                                result.AddRowFun(row);
                            break;

                        case ">=":
                            if (cellValue >= thresholdValue)
                                result.AddRowFun(row);
                            break;

                        case "<=":
                            if (cellValue <= thresholdValue)
                                result.AddRowFun(row);
                            break;

                        default:
                            MessageBox.Show($"Недопустимый оператор: '{operatorType}'.");
                            return null;
                    }
                }
                else // для всех остальных полей используется только оператор '='
                {
                    if (operatorType == "=" && row[columnIndex].ToString() == threshold)
                    {
                        result.AddRowFun(row);
                    }
                    else if (operatorType != "=")
                    {
                        MessageBox.Show($"Недопустимый оператор: '{operatorType}' для нечислового столбца.");
                        return null;
                    }
                }
            }


            return result;
        }


        // Этот метод возвращает проекцию таблицы по указанным столбцам
        public static Table Projection(Table table, params string[] columnNames)
        {
            // Проверка на наличие атрибутов в исходной таблице
            foreach (string columnName in columnNames)
            {
                if (!table.columnsNames.Contains(columnName))
                {
                    MessageBox.Show($"Атрибут '{columnName}' не найден в таблице.", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            // Создание новой таблицы только с указанными атрибутами
            Table result = new Table(columnNames);

            // Добавляем в новую таблицу только значения из указанных столбцов
            foreach (var row in table.data_obj)
            {
                List<object> resultRow = new List<object>();
                foreach (string columnName in columnNames)
                {
                    int columnIndex = Array.IndexOf(table.columnsNames, columnName);
                    resultRow.Add(row[columnIndex]);
                }

                result.data_obj.Add(resultRow.ToArray());
            }

            // Возвращаем результирующую таблицу
            return result;
        }


        // Этот метод выполняет операцию деления таблицы1 на таблицу2
        public static Table Divide(Table table1, Table table2)
        {
            if (!table2.columnsNames.All(column => table1.columnsNames.Contains(column)))
            {
                MessageBox.Show("Все столбцы второго отношения должны присутствовать в первом отношении.", "Предупреждение",
   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Создаем таблицу результатов с колонками, отсутствующими в таблице table2
            string[] resultColumns = table1.columnsNames.Except(table2.columnsNames).ToArray();
            Table result = new Table(resultColumns);

            // Для каждой уникальной комбинации значений в resultColumns таблицы1
            foreach (var group in table1.data_obj.Select(row => row.Where((val, index) => !table2.columnsNames.Contains(table1.columnsNames[index])).ToArray()).Distinct())
            {
                // Для каждой строки в таблице2
                bool isMaximal = table2.data_obj.All(row2 =>
                {
                    // Создаем комбинированную строку со значениями из group и row2
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

                    // Проверяем, присутствует ли данная объединенная строка в таблице1
                    return table1.data_obj.Any(row1 => row1.SequenceEqual(combinedRow));
                });

                // Если группа максимальна (т.е. может быть объединена с каждой строкой таблицы2 и все еще присутствует в таблице1), добавьте ее к результату
                if (isMaximal)
                {
                    result.data_obj.Add(group);
                }
            }
            // Удалить дубликаты из таблицы результатов
            result.data_obj = result.data_obj
                .Distinct(new LambdaComparer<object[]>((x, y) => x.SequenceEqual(y)))
                .ToList();

            return result;

        }

    }
}