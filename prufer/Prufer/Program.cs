internal class Program
{
    static void Main(string[] args)
    {
        List<int> tree1 = new List<int>();
        List<int> tree2 = new List<int>();

        // Читаем данные из файла
        ReadGraphFromFile("tree.txt", tree1, tree2);

        // Создаем лист для хранения кодов Пруфера
        List<int> codPruferResult = new List<int>();

        // Выполняем кодирование Пруфера
        EncodePrufer(tree1, tree2, codPruferResult);

        // Выводим результат
        PrintPruferCodes(codPruferResult);

        // Записываем результат в файл
        WritePruferCodesToFile("CodPrufera.txt", codPruferResult);
    }

    // Метод для чтения графа из файла
    private static void ReadGraphFromFile(string fileName, List<int> tree1, List<int> tree2)
    {
        using (StreamReader sr = new StreamReader(fileName))
        {
            while (!sr.EndOfStream)
            {
                string[] array = sr.ReadLine().Split(" ");
                tree1.Add(Convert.ToInt32(array[0]));
                tree2.Add(Convert.ToInt32(array[1]));
            }
        }
    }

    // Метод для кодирования Пруфера
    private static void EncodePrufer(List<int> tree1, List<int> tree2, List<int> codPruferResult)
    {
        int a = tree1.Count - 1;

        while (codPruferResult.Count < a)
        {
            // Поиск листьев
            List<int> sheet = new List<int>();
            List<int> sheetIndex = new List<int>();
            List<int> momOfSheet = new List<int>();

            for (int i = 0; i < tree1.Count; i++)
            {
                if (!tree1.Contains(tree2[i]))
                {
                    sheet.Add(tree2[i]);
                    momOfSheet.Add(tree1[i]);
                    sheetIndex.Add(i);
                }
                else if (!tree2.Contains(tree1[i]) && tree1.Where(x => x == tree1[i]).Count() == 1)
                {
                    sheet.Add(tree1[i]);
                    momOfSheet.Add(tree2[i]);
                    sheetIndex.Add(i);
                }
            }

            // Поиск минимального листа
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

            // Добавление кода Пруфера и удаление ребра
            int indexdel = sheetIndex[minIndex];
            codPruferResult.Add(momOfSheet[minIndex]);

            tree2.RemoveAt(indexdel);
            tree1.RemoveAt(indexdel);

            sheetIndex.Clear();
            sheet.Clear();
            momOfSheet.Clear();
        }
    }

    // Метод для вывода кодов Пруфера
    private static void PrintPruferCodes(List<int> codPruferResult)
    {
        foreach (int i in codPruferResult)
        {
            Console.WriteLine(i + " ");
        }
    }

    // Метод для записи кодов Пруфера в файл
    private static void WritePruferCodesToFile(string fileName, List<int> codPruferResult)
    {
        using (StreamWriter sw = new StreamWriter(fileName, false))
        {
            foreach (var item in codPruferResult)
            {
                sw.WriteLine(item + " ");
            }
        }
    }
}