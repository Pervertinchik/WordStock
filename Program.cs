using System;
using System.IO;


namespace WordStock
{
    class Program
    {
        private static bool is_first = true; //Для первого раза, чтобы вывести приветствие

        static void Main()
        {
            if (is_first)
            {
                Console.WriteLine("Добро пожаловать в игру \"Виселица\". Вам нужно угадать загаданное компьютером слово. \nВсего будет 6 попыток. Необходимо будет вводить по одной букве из слова\n");
                is_first = false;
            }
            Hang hang = new Hang();


            while (hang.get_tries > 0)
            {
                char letter = ' ';
                try
                {
                    letter = Convert.ToChar(Console.ReadLine());
                }

                catch
                {
                    Console.WriteLine("Что-то пошло не так");
                    continue;
                }

                hang.Try(letter); //Проверка есть ли символ в строке

                {
                    if (hang.is_again)
                    {
                        Main();
                        break;
                    }

                    else if (hang.is_finish)
                    {
                        Finish();
                        break;

                    }
                }

            }







        }


        private static void Finish()
        {
            Console.ReadKey();
        }




    }




    public class Hang
    {
        private string file_name_file = "WordsStockRus.txt"; //Название файла
        private string full_path; //Путь к файлу

        private string correct_name; //Строка загаданноо слова
        public string get_correct_name { get { return correct_name; } }

        private char[] correct_name_char; //Массив из символов загаданного слова

        private char[] finding_name; //Угадываемый массив из символов, сколько угадали

        private int tries = 6; //Количество попыток

        public bool is_again = false; //Начать ли новую игру

        public bool is_finish = false; //Закончена ли игра



        public int get_tries { get { return tries; } }



        public Hang()
        {
            full_path = Path.GetFullPath(file_name_file); //Получение пути к файлу


            Random rand = new Random();
            var all_lines = File.ReadAllLines(full_path); //Получить доступ ко всем строкам файла
            var line_number = rand.Next(0, all_lines.Length); //Выбрать случайный номер строки
            correct_name = all_lines[line_number]; //Выбрать случайную строчку

            finding_name = new char[correct_name.Length]; //Создать массив
            correct_name_char = new char[correct_name.Length]; //Создать массив


            Console.WriteLine();
            for (int n = 0; n < finding_name.Length; n++) //Сделать массив угадываемого слова из значков "_"
            {
                finding_name[n] = '_';
            }


            foreach (char aaa in finding_name) //Выводим угадываемый массив
            {

                Console.Write(aaa);
                Console.Write(" ");

            }



            for (int n2 = 0; n2 < finding_name.Length; n2++) //Сделать массив символов из строки
            {
                correct_name_char[n2] = correct_name[n2];
            }


            Console.WriteLine();



        }

        public void Try(char aa) //Проверка есть ли символ в строке
        {
            bool is_right = false; //Переменная, которая отвечает за то, что угадал букву хотя бы в одном месте
            Console.WriteLine();



            for (int n = 0; n < finding_name.Length; n++) //Проверка  нет ли уже этого элемента
            {

                if (finding_name[n] == aa)
                {
                    Console.WriteLine("Этот символ уже есть");

                }


            }

            for (int n = 0; n < correct_name.Length; n++) //Приравнивание элемента в строке, если совпадает с символом
            {

                if (correct_name_char[n] == aa)
                {
                    finding_name[n] = aa;
                    is_right = true;

                }


            }




            if (!is_right)
            {
                Console.WriteLine("\nТакого символа нет");
                tries--;
                if (tries == 0)
                {
                    Lose();
                }

                else if (tries != 0)
                {
                    Fall();
                }
            }







            foreach (char aaa in finding_name)
            {

                Console.Write(aaa);
                Console.Write(" ");

            }
            Console.WriteLine();

            bool is_win = Test();  //Проверка все ли символы угадали


            if (is_win)
            {
                Win();
            }

        }

        private bool Test() //Проверка на то, угадали ли слово
        {
            bool is_win = true;


            for (int n = 0; n < correct_name.Length; n++)
            {

                if (correct_name_char[n] != finding_name[n])  //Приравнивание элемента в строке, если совпадает с символом
                {
                    is_win = false;

                }


            }


            return is_win;
        }

        private void Fall()
        {
            Console.WriteLine($"Количество оставшихся попыток: {tries} \n");
        }


        private void Lose()
        {

            Console.WriteLine($"У вас закончились попытки, слово было: {correct_name}");
            Again();
        }

        private void Win()
        {

            Console.WriteLine($"Вы угадали слово!");
            Again();
        }

        private void Again()
        {
            Console.WriteLine("Попробовать ещё раз?. 1 - Да. 2 - Нет");
            try
            {
                int quest = Convert.ToInt32(Console.ReadLine());
                if (quest == 1)
                {
                    is_again = true;
                }

                else if (quest == 2)
                {
                    is_finish = true;
                }

            }

            catch
            {

                Again();
            }
        }




    }
}