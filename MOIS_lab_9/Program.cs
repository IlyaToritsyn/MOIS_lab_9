using System;
using System.Collections.Generic;
using System.IO;
using ClassLibrary;

namespace MOIS_lab_9
{
    //9. Дан текстовый файл, содержащий на каждой строчке одно слово, состоящее из букв латинского алфавита.
    //С использованием коллекции Dictionary написать программу, позволяющую закодировать каждое слово с помощью азбуки Морзе и вывести на экран в закодированном виде
    //(каждое слово на отдельной строке)

    /// <summary>
    /// Вспомогательный класс.
    /// </summary>
    public static class MyClass
    {
        /// <summary>
        /// Ввод целого числа с клавиатуры.
        /// </summary>
        /// <param name="message">Сообщение для ввода</param>
        /// <returns>Целое число, введённое с клавиатуры</returns>
        public static int InputInteger(string message)
        {
            bool isParsed = false;

            int N = 0;

            while (!isParsed)
            {
                Console.WriteLine(message);

                isParsed = int.TryParse(Console.ReadLine(), out N);

                Console.WriteLine();
            }

            return N;
        }

        /// <summary>
        /// Получение списка файлов с расширением .txt из заданной директории.
        /// </summary>
        /// <param name="directoryPath">Директория</param>
        /// <param name="txtFiles">Список файлов с расширением .txt</param>
        public static void GetTxtFiles(string directoryPath, out List<string> txtFiles)
        {
            txtFiles = new List<string>();

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string[] files = Directory.GetFiles(directoryPath);

            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(files[i]);
                    if (fileInfo.Extension == ".txt")
                    {
                        txtFiles.Add(fileInfo.Name);
                    }
                }
            }

            if (txtFiles.Count == 0)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                throw new Exception("В папке, находящейся по пути:\n\t" + directoryInfo.FullName + " -\nнет ни одного файла с расширением .txt.");
            }
        }

        /// <summary>
        /// Вывод элементов строкового списка в консоль.
        /// </summary>
        /// <param name="list">Строковый список.</param>
        public static void OutputConsole(List<string> list)
        {
            foreach (string str in list)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                string directoryPath = @".\Texts\";
                string selectedFile;

                int fileNumber;

                List<string> words = new List<string>();

                try
                {
                    MyClass.GetTxtFiles(directoryPath, out List<string> txtFiles);

                    Console.WriteLine("Список доступных файлов:");

                    foreach (string file in txtFiles)
                    {
                        Console.WriteLine(txtFiles.IndexOf(file) + 1 + ". " + file);
                    }

                    Console.WriteLine();

                    fileNumber = MyClass.InputInteger("Введите номер нужного файла или 0, чтобы обновить список:");

                    if (fileNumber == 0)
                    {
                        continue;
                    }

                    try
                    {
                        if ((fileNumber < 1) || (fileNumber > txtFiles.Count))
                        {
                            throw new Exception("Введён неверный номер файла: " + fileNumber + ".");
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\n");

                        continue;
                    }

                    selectedFile = txtFiles[fileNumber - 1];

                    Console.WriteLine("Выбран файл: " + selectedFile + "\n");

                    using (StreamReader reader = new StreamReader(directoryPath + selectedFile))
                    {
                        Console.WriteLine("Считываю файл...\n");

                        string str; //Текущая строка файла.

                        while ((str = reader.ReadLine()) != null)
                        {
                            str = str.Trim().ToUpper();

                            //Если строка пустая, то пропускаем её.
                            if (str.Length == 0)
                            {
                                continue;
                            }

                            Console.WriteLine(str);

                            for (int i = 0; i < str.Length; i++)
                            {
                                if (!((str[i] >= 'A') && (str[i] <= 'Z')))
                                {
                                    if (str[i] == ' ' || str[i] == '\t')
                                    {
                                        Console.WriteLine();

                                        throw new Exception("Более 1 слова на строке. На строке должно быть только 1 слово.");
                                    }

                                    Console.WriteLine();

                                    throw new Exception("Недопустимый символ: " + str[i]);
                                }
                            }

                            words.Add(str);
                        }

                        if (words.Count == 0)
                        {
                            throw new Exception("Файл " + selectedFile + " пуст - ничего не закодировано.");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Файл успешно считан.\n");
                    }

                    WorkWithMorseCode.Encode(words, out List<string> encodedWords);

                    Console.WriteLine("Содержимое файла, закодированное азбукой Морзе:");
                    MyClass.OutputConsole(encodedWords);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }

                Console.WriteLine("Нажмите любую клавишу, чтобы запустить программу заново.\n");

                Console.ReadKey(true);
            }
        }
    }
}
