using RelationalAlgebraWinFormsApp;

namespace TestProject1
{
    public class Tests
    {
        private Table table1;
        private Table table2;

        [SetUp]
        public void Setup()
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

        [Test]
        public void TestUnion()
        {
            // Выполняем объединение двух таблиц.
            var result = RelationalOperations.Union(table1, table2);

            // Убедимся, что результат ненулевой и содержит все уникальные строки из обеих таблиц.
            Assert.IsNotNull(result);
            Assert.That(result.data_obj.Count, Is.EqualTo(4));
        }

        [Test]
        public void TestIntersection()
        {
            // Выполняем пересечение двух таблиц.
            var result = RelationalOperations.Intersection(table1, table2);

            // Убедимся, что результат ненулевой и содержит строки, которые присутствуют в обеих таблицах.
            Assert.IsNotNull(result);
            Assert.That(result.data_obj.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestDifference()
        {
            // Выполняем разность двух таблиц.
            var result = RelationalOperations.Difference(table1, table2);

            // Убедимся, что результат ненулевой и содержит строки, которые присутствуют только в первой таблице.
            Assert.IsNotNull(result);
            Assert.That(result.data_obj.Count, Is.EqualTo(1));
        }

    }
}