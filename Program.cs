using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static ExamNP.Checker;

namespace ExamNP
{
    class Program
    {
        public static string[] urls;
        public static Checker checker = new();
        static void Main(string[] args)
        {
            //   Checker checker = new();
            do
            {
                Console.Clear();
                // Console.WriteLine("Exam NP");
                Console.WriteLine("Хотите посмотреть демонстрацию?\nДа нажмите 'Y'\nНет нажмите'N' ");
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    checker.WaitChecking = new string[] {

                "http://be-x.narod.ru/tank.wml",
                "http://www.knecka.com",
                "http://www.knecka.com/kneka",
                "http://serg.adonis.ua/WeatherLink/OutsideTempHistory.gif",
                "https://domain.com/wp-admin/plugins.php",
                "http://wrong.com",
                "http://example.com/ttt.php",
                "http://google.corm",
                "https://www.google.corm/",
                "http://stats.vk-portal.net",//---------
                "https://google.com/brr.php",
                "https://www.microsoft.com",
                "https://id.cisco.com",
                "https://kherson.itstep.org",
                "https://metanit.com",
                "https://store.steampowered.com",
                "https://www.virtualbox.org",
                "https://desktop.telegram.org",
                "https://www.ubisoft.com",
                "https://habr.com",
                "https://www.work.ua",
                "https://rabota.ua",
                "https://jobs.dou.ua",
                "https://djinni.co",
                "https://www.w3schools.com",
                "https://github.com",
                "https://ru.reactjs.org",
                "https://learn.javascript.ru",
                "https://developer.mozilla.org",
                "https://coderoad.ru",
                "https://ostfilm.org",
                "https://filmweb.pl",
                "http://wrong.com",
                "http://example.com",
                "http://seasonvar.ru"
                }.ToList();

                    checker.CheckUrl();

                    //+++++
                    ShowAll();
                    //-----

                    WhatCodeSelect();
                }
                else
                {
                    urls = new string[CountMas()];

                    AddUrl(urls);

                    checker.WaitChecking = urls.ToList();

                    checker.CheckUrl();
                    //+++++
                    ShowAll();
                    //-----

                    WhatCodeSelect();
                }
                Console.WriteLine("Хотите проверить еще? Да нажмите 'Y' Нет нажмите'N'");

            } while (Console.ReadKey(true).Key == ConsoleKey.Y);

        }

        private static void WhatCodeSelect()
        {
            Console.WriteLine("Хотите сами выбрать StatusCode?\nДа нажмите 'Y'\nНет нажмите'N' ");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                Console.Clear();
                Show(Select((Enum.GetValues(typeof(HttpStatusCode))), 5, 5, "Выберите HttpStatusCode. Для выбора нерабочих Esc"));

            }
            else
            {
                Show(HttpStatusCode.OK);
                Show(HttpStatusCode.InternalServerError);
                Show(HttpStatusCode.NotFound);

                Show(HttpStatusCode.Forbidden);
                Show(HttpStatusCode.BadRequest);
                Show(HttpStatusCode.FailedDependency);
                Show(HttpStatusCode.Found);

                Show(HttpStatusCode.Moved);
                Show(0);//  not working link

            }
        }

        public static void Show( HttpStatusCode code)
        {
            var list = checker.GetListStatusCode(code);

            Console.WriteLine($"\n {list.Count} {code}: ({(int)code})");

            foreach (var item in list)
            {
                Console.WriteLine($"{item.url} --> {item.httpStatusCode}");
            }
        }

        public static void ShowAll()
        {
            foreach (var item in checker.IsChecked)
            {
                Console.WriteLine($"{item.url} --> {item.httpStatusCode}");
            }
        }

        public static HttpStatusCode Select(Array masT, int x, int y, string text = "")
        {
            string str = "";
            try
            {
                ConsoleKey Key;
                int i = 0;
                while (true)
                {
                    if (i == masT.Length)
                    {
                        i = 0;
                    }
                    if (i < 0)
                    {
                        i = masT.Length - 1;
                    }

                    str = $"{(int)masT.GetValue(i)} {masT.GetValue(i)}";


                    Console.SetCursorPosition(x, y);
                    Console.Write(text);

                    Console.SetCursorPosition(x, y + 1);
                    Console.Write(str);

                    Key = Console.ReadKey(true).Key;

                    Console.SetCursorPosition(x, y + 1);
                    //Remove previous string
                    for (int j = 0; j < str.Length; j++)
                    {
                        Console.Write(" ");
                    }

                    switch (Key)
                    {
                        case ConsoleKey.Enter:
                            return (HttpStatusCode)masT.GetValue(i);

                        case ConsoleKey.Escape:
                            return 0;

                        case ConsoleKey.Spacebar: i++; break;
                        case ConsoleKey.UpArrow: i++; break;
                        case ConsoleKey.DownArrow: i--; break;
                        case ConsoleKey.LeftArrow: i++; break;
                        case ConsoleKey.RightArrow: i--; break;
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return default;
            }
        }
        private static string[] AddUrl(string[] urls)
        {


            for (int i = 0; i < urls.Length; i++)
            {
                urls[i] = GetUrl(i + 1); ;
            }

            return urls;
        }

        private static string GetUrl(int i)
        {
            string url = string.Empty;
            do
            {
                Console.WriteLine($"Введите url {i} сайта");
                url = Console.ReadLine();

            } while (!Uri.IsWellFormedUriString(url, UriKind.Absolute));
            return url;
        }

        private static int CountMas()
        {
            int attempts = 4;
            do
            {
                attempts--;
                Console.WriteLine("Введите количество сайтов которые хотите проверить?");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Введите пожалуйста целое число. Осталось попыток {attempts}");
                Console.ResetColor();

            } while (attempts >0);

            Environment.Exit(0);
            return default;
        }
    }
}
