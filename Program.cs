using System;
using System.Collections.Generic;
using System.IO;

namespace Hang_man
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Words;

            try
            {
                Words = Word();
            }
            catch
            {
                Console.WriteLine("UNABLE TO OPEN WORDS.TXT, PLEASE MAKE SURE IT HASNT BEEN DELETED");
                Console.WriteLine();
                Console.WriteLine("The Current Programme Has Been Closed");
                Console.ReadKey();
                return;
            }
            
            string w = "";
            int len = 0;

            Console.WriteLine("Hang Man");            
            Console.WriteLine();
            foreach (string ico in Icon(9))
            {
                Console.WriteLine(ico);
            }
            Console.WriteLine();
            Console.WriteLine("[Benjamin B-L]");
            Console.WriteLine();
            Console.WriteLine("[V = 4.1.0]");
            Console.WriteLine("Better Error Handling");
            Console.WriteLine();
            Console.ReadKey();

            while (true)
            {
                Console.WriteLine("[Loading...]");
                while (len < 8)
                {
                    w = Pick(Words);
                    len = w.Length;
                }

                Console.Clear();

                len = 0;

                try
                {
                    bool b = Hang(w);
                }
                catch
                {
                    Console.WriteLine("ERROR");
                    Console.WriteLine();
                    break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("The Current Programme Has Been Closed");
            Console.ReadKey();
            return;
        }
        public static string[] Word()
        {
            List<string> Words = new List<string>();
            int i = 0;
            try
            {
                using (StreamReader sr = File.OpenText(Directory.GetCurrentDirectory() + @"\Words.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Words.Add(s);
                        i++;
                    }
                }
                return Words.ToArray();
            }
            catch
            {
                using (StreamReader sr = File.OpenText(Directory.GetCurrentDirectory() + @"/Words.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Words.Add(s);
                        i++;
                    }
                }
                return Words.ToArray();
            }
            
        }
        public static string Pick(string[] opt)
        {
            Random rand = new Random();
            int index = rand.Next(opt.Length);
            string Word = opt[index];

            return Word;
        }
        public static bool Hang(string Word = "")
        {
            bool win = false;
            int man = 0;
            int ou = 9;
            bool done = false;
            string key = " ";
            List<string> screen = new List<string>();
            List<string> left = new List<string>();

            int len = Word.Length;

            for (int i = 0; len > i; i++)
            {
                screen.Add("_");
            }

            rEnder(screen.ToArray(), man, ou, left.ToArray());

            while (!done)
            {
                key = Console.ReadLine().ToLower();

                if (Word.Contains(key))
                {
                    string[] aa = screen.ToArray();
                    int bb = aa.Length;
                    for (int i = 0; bb > i; i++)
                    {
                        if (screen[i] == key)
                        {
                            man++;
                        }
                        else if (Convert.ToString(Word[i]) == key)
                        {
                            screen[i] = key;
                        }
                    }

                    if (man >= ou)
                    {
                        win = true;
                        for (int i = 0; Word.Length > i; i++)
                        {
                            if (Convert.ToString(Word[i]) != screen[i])
                            {
                                win = false;
                                break;
                            }
                        }
                        done = true;
                    }
                    win = true;
                    for (int i = 0; Word.Length > i; i++)
                    {
                        if (Convert.ToString(Word[i]) != screen[i])
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        done = true;
                    }
                }
                else
                {
                    if (left.Contains(key))
                    {
                        man++;
                        if (man >= ou)
                        {
                            win = true;
                            for (int i = 0; Word.Length > i; i++)
                            {
                                if (Convert.ToString(Word[i]) != screen[i])
                                {
                                    win = false;
                                    break;
                                }
                            }
                            done = true;
                        }
                    }
                    else
                    {

                        if (key.Length > 1)
                        {
                            man++;
                        }
                        else
                        {

                            if (Ischar(System.Convert.ToChar(key)))
                            {
                                man++;
                                left.Add(key);
                            }
                            else
                            {
                                man++;
                            }
                        }
                        
                        if (man >= ou)
                        {
                            win = true;
                            for (int i = 0; Word.Length > i; i++)
                            {
                                if (Convert.ToString(Word[i]) != screen[i])
                                {
                                    win = false;
                                    break;
                                }
                            }
                            done = true;
                        }
                    }
                }
                rEnder(screen.ToArray(), man, ou, left.ToArray());
                if (done)
                {
                    break;
                }
            }

            End(Word, win, Icon(man));
            return win;
        }
        public static void rEnder(string[] src, int lives, int ou, string[] lefto)
        {
            Console.Clear();

            string[] ico = Icon(lives);

            foreach (string ic in ico)
            {
                Console.WriteLine(ic);
            }

            Console.WriteLine();

            Console.Write(Convert.ToString(lives) + "/" + Convert.ToString(ou));
            Console.WriteLine();
            Console.WriteLine();

            foreach (string o in src)
            {
                Console.Write(o + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            foreach (string l in lefto)
            {
                Console.Write(l + " ");
            }
            Console.WriteLine();
        }
        public static string[] Icon(int live)
        {
            string[] ico9 = {"_______","|/    |","|     O",@"|    /|\",@"|     /\",@"|\","__________"};
            string[] ico8 = { "_______", "|/    |", "|     O", @"|    /|\", @"|     ", @"|\", "__________" };
            string[] ico7 = { "_______", "|/    |", "|     O", @"|    ", @"|     ", @"|\", "__________" };
            string[] ico6 = { "_______", "|/    |", "|     ", @"|    ", @"|     ", @"|\", "__________" };
            string[] ico5 = { "_______", "|/    ", "|     ", @"|    ", @"|     ", @"|\", "__________" };
            string[] ico4 = { "", "|/    ", "|     ", @"|    ", @"|     ", @"|\", "__________" };
            string[] ico3 = { "", "|    ", "|     ", @"|    ", @"|     ", @"|\", "__________" };
            string[] ico2 = { "", "    ", "     ", @"    ", @"     ", @" \", "__________" };
            string[] ico1 = { "", "    ", "     ", @"    ", @"     ", @" ", "__________" };
            string[] ico0 = { "", "    ", "     ", @"    ", @"     ", @" ", "" };
            string[] error = ico0;

            if (live == 0)
            {
                return ico0;
            }
            if (live == 9)
            {
                return ico9;
            }
            if (live == 8)
            {
                return ico8;
            }
            if (live == 7)
            {
                return ico7;
            }
            if (live == 6)
            {
                return ico6;
            }
            if (live == 5)
            {
                return ico5;
            }
            if (live == 4)
            {
                return ico4;
            }
            if (live == 3)
            {
                return ico3;
            }
            if (live == 2)
            {
                return ico2;
            }
            if (live == 1)
            {
                return ico1;
            }

            return error;
        }
        public static void End(string Word, bool win, string[] man)
        {
            Console.Clear();
            if (win)
            {
                Console.WriteLine("You Win!!!");
                Console.WriteLine();
                foreach (string ic in man)
                {
                    Console.WriteLine(ic);
                }
                Console.WriteLine();
                Console.WriteLine("well done for guessing the Word: " + Word);
            }
            else
            {
                Console.WriteLine("You Lose :(");
                Console.WriteLine();
                foreach (string ic in man)
                {
                    Console.WriteLine(ic);
                }
                Console.WriteLine();
                Console.WriteLine("The Word was: " + Word);
            }

            Console.WriteLine();
        }
        public static bool Ischar(char lett)
        {
            bool o = false;

            char[] all = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            foreach (char let in all)
            {
                if (lett == let)
                {
                    o = true;
                    break;
                }
            }

            return o;
        }
    }
}