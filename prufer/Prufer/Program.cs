namespace Prufer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<int> tree1 = new List<int>();
            List<int> tree2 = new List<int>();

            using (StreamReader sr = new StreamReader("tree.txt"))
            {
                while (sr.EndOfStream != true)
                {
                    string[] array = sr.ReadLine().Split(" ");
                    tree1.Add(Convert.ToInt32(array[0]));
                    tree2.Add(Convert.ToInt32(array[1]));
                }
            }

            List<int> sheet = new List<int>();
            List<int> sheetIndex = new List<int>();
            List<int> momOfSheet = new List<int>();
            List<int> codPruferResult = new List<int>();
            int a = tree1.Count - 1;

            while (codPruferResult.Count < a)
            {
                for (int i = 0; i < tree1.Count; i++)
                {
                    if (tree1.Contains(tree2[i]) != true)
                    {
                        sheet.Add(tree2[i]);
                        momOfSheet.Add(tree1[i]);
                        sheetIndex.Add(i);
                    }
                    else if (tree2.Contains(tree1[i]) != true && tree1.Where(x => x == tree1[i]).Count() ==1) 
                    {
                        sheet.Add(tree1[i]);
                        momOfSheet.Add(tree2[i]);
                        sheetIndex.Add(i);
                    }
                }

                int minIndex = 0;
                int minimum = sheet[minIndex];

                for (int j = 0; j < sheet.Count; j++)
                {
                    if (minimum > sheet[j])
                    {
                        minimum = sheet[j];
                        minIndex = j;
                    }
                }

                int indexdel = sheetIndex[minIndex];
                codPruferResult.Add(momOfSheet[minIndex]);
                
                
                tree2.RemoveAt(indexdel);
                tree1.RemoveAt(indexdel);

                sheetIndex.Clear();
                sheet.Clear();
                momOfSheet.Clear();
            }
            foreach (int i in codPruferResult)
            {
                Console.WriteLine(i + " ");
            }
            using (StreamWriter sw = new StreamWriter("CodPrufera.txt", false))
            {
                foreach (var item in codPruferResult)
                {
                    sw.WriteLine(item + " ");
                }
            }
        }
    }
}