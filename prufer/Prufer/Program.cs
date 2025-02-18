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
    private static void ReadGraphFromFile(string fileName, List<int> tree1, List<int> tree2)
    {
        try
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string[] array = sr.ReadLine().Split(" ");
                    if (array.Length != 2)
                    {
                        throw new FormatException("Строка должна содержать два числа.");
                    }
                    tree1.Add(Convert.ToInt32(array[0]));
                    tree2.Add(Convert.ToInt32(array[1]));
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {fileName} не найден.");
        }
        catch (IOException)
        {
            Console.WriteLine($"Ошибка при чтении файла {fileName}.");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Ошибка при преобразовании строки в число: {ex.Message}");
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
    private static void WritePruferCodesToFile(string fileName, List<int> codPruferResult)
    {
        try
        {
            File.WriteAllLines(fileName, codPruferResult.Select(x => x.ToString()));
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка при записи в файл {fileName}: {ex.Message}");
        }
    }
}