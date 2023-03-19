using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RelationalAlgebraWinFormsApp
{
    internal class RelationalOperations
    {

        public static List<Tuple<int, string, string>> Union(List<Tuple<int, string, string>> table1, List<Tuple<int, string, string>> table2)
        {
            List<Tuple<int, string, string>> result = new List<Tuple<int, string, string>>();

            HashSet<Tuple<int, string, string>> seenRows = new HashSet<Tuple<int, string, string>>();


            foreach (Tuple<int, string, string> row in table1)
            {
                if (!string.IsNullOrEmpty(row.Item2) && !string.IsNullOrEmpty(row.Item3) &&
                    seenRows.Add(row)) 
                {
                    result.Add(row);
                }
            }


            foreach (Tuple<int, string, string> row in table2)
            {
                if (!string.IsNullOrEmpty(row.Item2) && !string.IsNullOrEmpty(row.Item3) &&
                    seenRows.Add(row)) 
                {
                    result.Add(row);
                }
            }

            return result;
        }

        public static List<Tuple<int, string, string>> Intersection(List<Tuple<int, string, string>> table1, List<Tuple<int, string, string>> table2)
        {
            List<Tuple<int, string, string>> result = new List<Tuple<int, string, string>>();
            foreach (Tuple<int, string, string> row1 in table1)
            {
                foreach (Tuple<int, string, string> row2 in table2)
                {
                    if (row1.Item1 == row2.Item1 && row1.Item2 == row2.Item2 && row1.Item3 == row2.Item3
                        && !string.IsNullOrEmpty(row1.Item2) && !string.IsNullOrEmpty(row1.Item3))
                    {
                        result.Add(row1);
                        break;
                    }
                }
            }

            return result;
        }

        public static List<Tuple<int, string, string>> Difference(List<Tuple<int, string, string>> table1, List<Tuple<int, string, string>> table2)
        {

            List<Tuple<int, string, string>> result = new List<Tuple<int, string, string>>();
            foreach (Tuple<int, string, string> row1 in table1)
            {
                if (!string.IsNullOrEmpty(row1.Item2) && !string.IsNullOrEmpty(row1.Item3))
                {
                    bool found = false;
                    foreach (Tuple<int, string, string> row2 in table2)
                    {
                        if (row1.Item1 == row2.Item1 && row1.Item2 == row2.Item2 && row1.Item3 == row2.Item3)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        result.Add(row1);
                    }
                }
            }
            return result;
        }

        public static List<Tuple<int, string, string, int, string, string>> CartesianProduct(List<Tuple<int, string, string>> table1, List<Tuple<int, string, string>> table2)
        {
            List<Tuple<int, string, string, int, string, string>> result = new List<Tuple<int, string, string, int, string, string>>();

            foreach (Tuple<int, string, string> row1 in table1)
            {
                if (row1.Item1 == 0 && row1.Item2 == null && row1.Item3 == null)
                    continue;
                foreach (Tuple<int, string, string> row2 in table2)
                {
                    if (row2.Item1 == 0 && row2.Item2 == null && row2.Item3 == null)
                        continue;
                    int idR3 = row1.Item1;
                    string nameR3 = row1.Item2;
                    string companyR3 = row1.Item3;
                    int idR4 = row2.Item1;
                    string nameR4 = row2.Item2;
                    string companyR4 = row2.Item3;
                    result.Add(new Tuple<int, string, string, int, string, string>(idR3, nameR3, companyR3, idR4, nameR4, companyR4));
                }
            }

            return result;
        }

    }
}
