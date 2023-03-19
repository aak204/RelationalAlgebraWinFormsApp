using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RelationalAlgebraWinFormsApp
{
    internal class Table
    {
        private List<Tuple<int, string, string>> rows;
        private static readonly string[] Names = {
    "Иванов Иван Иванович",
    "Петров Петр Петрович",
    "Сидорова Елена Викторовна",
    "Козлова Анна Андреевна",
    "Васильев Андрей Сергеевич",
    "Громов Денис Владимирович",
    "Королева Ольга Анатольевна",
    "Лебедев Александр Иванович",
    "Романова Ирина Дмитриевна",
    "Федоров Сергей Анатольевич"
};

        private static readonly string[] Companies = {
    "ООО \"Рога и копыта\"",
    "ЗАО \"Русский газ\"",
    "ОАО \"Газпром\"",
    "ПАО \"Сбербанк\"",
    "ООО \"Газпром нефть\"",
    "ПАО \"Лукойл\"",
    "ООО \"Роснефть\"",
    "АО \"Транснефть\"",
    "ООО \"РусГидро\"",
    "ПАО \"Магнит\""
};
        private static readonly Random Random = new Random();

        public Table()
        {
            rows = new List<Tuple<int, string, string>>();
        }

        public void FillInAutomatically()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = Random.Next(1, 512);
                string name = Names[Random.Next(0, Names.Length)];
                string company = Companies[Random.Next(0, Companies.Length)];
                rows.Add(new Tuple<int, string, string>(id, name, company));
            }
        }

        public void FillInManual()
        {
            for (int i = 0; i < 10; i++)
            {
                rows.Add(new Tuple<int, string, string>(0, null, null));
            }
        }

        public void AddRow()
        {
            rows.Add(new Tuple<int, string, string>(0, null, null));
        }


        public void RewriteRow(int id, string name, string company, int index)
        {
            rows[index] = new Tuple<int, string, string>(id, name, company);
        }

        public bool IsEmpty()
        {
            return rows.Count == 0;
        }

        public int CheckId(int id)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i].Item1 == id)
                {
                    return i;
                }
            }
            return -1; // ID not found
        }

        public int GetId(int excludeIndex)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (i == excludeIndex)
                {
                    return i;
                }
            }
            return -1; 
        }

        public List<Tuple<int, string, string>> GetRows()
        {
            return rows;
        }
    }
}
