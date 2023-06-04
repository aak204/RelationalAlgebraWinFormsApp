using Microsoft.VisualStudio.TestTools.UnitTesting;
using RelationalAlgebraWinFormsApp;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class TestMethodsOperations
    {
        private Table table1;
        private Table table2;
        private Table table1_new;
        private Table table2_new;
        [TestInitialize]
        public void TestInitialize()
        {
            // Создадим два объекта таблицы с заданными данными перед каждым тестом.
            table1 = new Table("ID", "Name", "Company");
            table1.data_obj.AddRange(new object[][] {
            new object[] { 1, "Alice", "CompanyA" },
            new object[] { 2, "Bob", "CompanyB" },
            new object[] { 3, "Charlie", "CompanyC" },
        });

            table2 = new Table("ID", "Name", "Company");
            table2.data_obj.AddRange(new object[][] {
            new object[] { 2, "Bob", "CompanyB" },
            new object[] { 3, "Charlie", "CompanyC" },
            new object[] { 4, "David", "CompanyD" },
        });
        }

        [TestMethod]
        public void TestUnion()
        {
            // Выполняем объединение двух таблиц.
            var result = RelationalOperations.Union(table1, table2);

            // Убедимся, что результат ненулевой и содержит все уникальные строки из обеих таблиц.
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.data_obj.Count);
        }

        [TestMethod]
        public void TestIntersection()
        {
            // Выполняем пересечение двух таблиц.
            var result = RelationalOperations.Intersection(table1, table2);

            // Убедимся, что результат ненулевой и содержит строки, которые присутствуют в обеих таблицах.
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.data_obj.Count);
        }

        [TestMethod]
        public void TestDifference()
        {
            // Выполняем разность двух таблиц.
            var result = RelationalOperations.Difference(table1, table2);

            // Убедимся, что результат ненулевой и содержит строки, которые присутствуют только в первой таблице.
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.data_obj.Count);
        }

        [TestMethod]
        public void TestCartesianProduct()
        {
            var resultTable = RelationalOperations.CartesianProduct(table1, table2);

            Assert.AreEqual(table1.data_obj.Count * table2.data_obj.Count, resultTable.data_obj.Count);
        }

        [TestMethod]
        public void TestInnerJoin()
        {
            var resultTable = RelationalOperations.InnerJoin(table1, table2, "ID");

            Assert.AreEqual(2, resultTable.data_obj.Count);
        }

        [TestMethod]
        public void TestLeftJoin()
        {
            var resultTable = RelationalOperations.LeftJoin(table1, table2, "ID");

            Assert.AreEqual(3, resultTable.data_obj.Count);
        }

        [TestMethod]
        public void TestRightJoin()
        {
            var resultTable = RelationalOperations.RightJoin(table1, table2, "ID");

            Assert.AreEqual(3, resultTable.data_obj.Count);
        }

        [TestMethod]
        public void TestFullJoin()
        {
            var resultTable = RelationalOperations.FullJoin(table1, table2, "ID");

            Assert.AreEqual(4, resultTable.data_obj.Count);
        }

        [TestMethod]
        public void TestSelection()
        {
            var resultTable = RelationalOperations.Selection(table1, "ID", ">", "1");

            Assert.AreEqual(2, resultTable.data_obj.Count);
        }

        [TestMethod]
        public void TestProjection()
        {
            var resultTable = RelationalOperations.Projection(table1, "ID", "Name");

            Assert.AreEqual(3, resultTable.data_obj.Count);
            Assert.AreEqual(2, resultTable.columnsNames.Length);
        }

        [TestMethod]
        public void TestDivide()
        {
            // Создадим два объекта таблицы с заданными данными перед каждым тестом.
            table1_new = new Table("ID", "Name", "Company");
            table1_new.data_obj.AddRange(new object[][] {
    new object[] { 1, "Alice", "CompanyA" },
    new object[] { 1, "Bob", "CompanyA" },
    new object[] { 2, "Charlie", "CompanyA" },
    new object[] { 2, "David", "CompanyA" },
});

            table2_new = new Table("ID", "Name");
            table2_new.data_obj.AddRange(new object[][] {
    new object[] { 1, "Alice" },
    new object[] { 1, "Bob" },
    new object[] { 2, "Charlie" },
    new object[] { 2, "David" },
});

            var expectedTable = new Table("Company");
            expectedTable.data_obj.AddRange(new object[][] {
    new object[] { "CompanyA" },
});

            var resultTable = RelationalOperations.Divide(table1_new, table2_new);

            // Проверяем, что количество строк совпадает
            Assert.AreEqual(expectedTable.data_obj.Count, resultTable.data_obj.Count);

            // Проверяем, что каждая строка в результате присутствует в ожидаемой таблице
            foreach (var row in resultTable.data_obj)
            {
                Assert.IsTrue(expectedTable.data_obj.Any(expectedRow => expectedRow.SequenceEqual(row)));
            }
        }

    }
}
