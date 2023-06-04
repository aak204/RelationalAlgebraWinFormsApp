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
            // �������� ��� ������� ������� � ��������� ������� ����� ������ ������.
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
            // ��������� ����������� ���� ������.
            var result = RelationalOperations.Union(table1, table2);

            // ��������, ��� ��������� ��������� � �������� ��� ���������� ������ �� ����� ������.
            Assert.IsNotNull(result);
            Assert.That(result.data_obj.Count, Is.EqualTo(4));
        }

        [Test]
        public void TestIntersection()
        {
            // ��������� ����������� ���� ������.
            var result = RelationalOperations.Intersection(table1, table2);

            // ��������, ��� ��������� ��������� � �������� ������, ������� ������������ � ����� ��������.
            Assert.IsNotNull(result);
            Assert.That(result.data_obj.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestDifference()
        {
            // ��������� �������� ���� ������.
            var result = RelationalOperations.Difference(table1, table2);

            // ��������, ��� ��������� ��������� � �������� ������, ������� ������������ ������ � ������ �������.
            Assert.IsNotNull(result);
            Assert.That(result.data_obj.Count, Is.EqualTo(1));
        }

    }
}