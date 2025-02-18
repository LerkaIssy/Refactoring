using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> sourceVertices = new List<int>();
        List<int> destinationVertices = new List<int>();

        // Читаем данные из файла
        ReadGraphFromFile("tree.txt", sourceVertices, destinationVertices);

        // Создаем лист для хранения кодов Пруфера
        List<int> pruferCodes = new List<int>();

        // Выполняем кодирование Пруфера
        EncodePrufer(sourceVertices, destinationVertices, pruferCodes);

        // Выводим результат
        PrintPruferCodes(pruferCodes);

        // Записываем результат в файл
        WritePruferCodesToFile("CodPrufera.txt", pruferCodes);
    }

    // Метод для чтения графа из файла
    private static void ReadGraphFromFile(string fileName, List<int> sourceVertices, List<int> destinationVertices)
    {
        using (StreamReader sr = new StreamReader(fileName))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    throw new FileLoadException("Не удалось прочитать строку из файла.");
                }

                string[] array = line.Split(" ");
                if (array.Length != 2)
                {
                    throw new FileLoadException("Неверный формат данных в файле.");
                }

                sourceVertices.Add(Convert.ToInt32(array[0]));
                destinationVertices.Add(Convert.ToInt32(array[1]));
            }
        }
    }


    // Метод для кодирования Пруфера
    private static void EncodePrufer(List<int> sourceVertices, List<int> destinationVertices, List<int> pruferCodes)
    {
        int numberOfEdges = sourceVertices.Count - 1;

        while (pruferCodes.Count < numberOfEdges)
        {
            // Поиск листьев
            List<int> leafVertices = new List<int>();
            List<int> leafIndices = new List<int>();
            List<int> leafParents = new List<int>();

            for (int i = 0; i < sourceVertices.Count; i++)
            {
                if (!sourceVertices.Contains(destinationVertices[i]))
                {
                    leafVertices.Add(destinationVertices[i]);
                    leafParents.Add(sourceVertices[i]);
                    leafIndices.Add(i);
                }
                else if (!destinationVertices.Contains(sourceVertices[i]) && sourceVertices.Where(x => x == sourceVertices[i]).Count() == 1)
                {
                    leafVertices.Add(sourceVertices[i]);
                    leafParents.Add(destinationVertices[i]);
                    leafIndices.Add(i);
                }
            }

            // Поиск минимального листа
            int minIndex = 0;
            int minimum = leafVertices[minIndex];

            for (int j = 0; j < leafVertices.Count; j++)
            {
                if (minimum > leafVertices[j])
                {
                    minimum = leafVertices[j];
                    minIndex = j;
                }
            }

            // Добавление кода Пруфера и удаление ребра
            int indexToDelete = leafIndices[minIndex];
            pruferCodes.Add(leafParents[minIndex]);

            destinationVertices.RemoveAt(indexToDelete);
            sourceVertices.RemoveAt(indexToDelete);

            leafIndices.Clear();
            leafVertices.Clear();
            leafParents.Clear();
        }
    }

    // Метод для вывода кодов Пруфера
    private static void PrintPruferCodes(List<int> pruferCodes)
    {
        foreach (int code in pruferCodes)
        {
            Console.WriteLine(code + " ");
        }
    }

    // Метод для записи кодов Пруфера в файл
    private static void WritePruferCodesToFile(string fileName, List<int> pruferCodes)
    {
        using (StreamWriter sw = new StreamWriter(fileName, false))
        {
            foreach (var code in pruferCodes)
            {
                sw.WriteLine(code + " ");
            }
        }
    }
}