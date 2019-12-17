using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hang_man
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words;

            try
            {
                words = word();
            }
            catch
            {
                Console.WriteLine("UNABLE TO OPEN WORDS.TXT, PLEASE MAKE SURE IT HASNT BEEN DELETED");
                Console.ReadKey();
                return;
            }
            
            string w = "";
            int len = 0;

            Console.WriteLine("Hang Man");            
            Console.WriteLine();
            foreach (string ico in icon(12))
            {
                Console.WriteLine(ico);
            }
            Console.WriteLine("[Benjamin B-L]");
            Console.WriteLine();
            Console.WriteLine("[V2.3.0]");
            Console.WriteLine("Better Error Handling");
            Console.WriteLine();
            Console.ReadKey();

            while (true)
            {
                Console.WriteLine("[Loading...]");
                while (len < 8)
                {
                    w = pick(words);
                    len = w.Length;
                }

                Console.Clear();

                len = 0;

                bool b = hang(w);

                Console.ReadKey();
                Console.Clear();
            }
        }
        public static string[] word()
        {
            List<string> words = new List<string>();
            int i = 0;
            using (StreamReader sr = File.OpenText(Directory.GetCurrentDirectory() + @"\words.txt"))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    words.Add(s);
                    i++;
                }
            }
            return words.ToArray();
        }
        public static string pick(string[] opt)
        {
            Random rand = new Random();
            int index = rand.Next(opt.Length);
            string word = opt[index];

            return word;
        }
        public static bool hang(string word)
        {
            bool win = false;
            int man = 0;
            int ou = 12;
            bool done = false;
            string key;
            int count = 0;
            bool rend = false;
            List<string> screen = new List<string>();
            List<string> left = new List<string>();

            int len = word.Length;

            for (int i = 0; len > i; i++)
            {
                screen.Add("_");
            }

            rend = false;
            count = 0;

            while (!rend & count < 10)
            {
                rend = render(screen.ToArray(), man, ou, left.ToArray());
                count++;
            }

            if (!rend)
            {
                Console.WriteLine("FAILED TO RENDER");
            }

            while (!done)
            {
                key = Console.ReadLine();

                if (word.Contains(key))
                {
                    string[] aa = screen.ToArray();
                    int bb = aa.Length;
                    for (int i = 0; bb > i; i++)
                    {
                        if (screen[i] == key)
                        {
                            man++;
                        }
                        else if (Convert.ToString(word[i]) == key)
                        {
                            screen[i] = key;
                        }
                    }

                    if (man >= ou)
                    {
                        win = true;
                        for (int i = 0; word.Length > i; i++)
                        {
                            if (Convert.ToString(word[i]) != screen[i])
                            {
                                win = false;
                                break;
                            }
                        }
                        done = true;
                    }
                    win = true;
                    for (int i = 0; word.Length > i; i++)
                    {
                        if (Convert.ToString(word[i]) != screen[i])
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
                            for (int i = 0; word.Length > i; i++)
                            {
                                if (Convert.ToString(word[i]) != screen[i])
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
                            man++;
                            left.Add(key);
                        }
                        
                        if (man >= ou)
                        {
                            win = true;
                            for (int i = 0; word.Length > i; i++)
                            {
                                if (Convert.ToString(word[i]) != screen[i])
                                {
                                    win = false;
                                    break;
                                }
                            }
                            done = true;
                        }
                    }
                }
                rend = false;
                count = 0;

                while (!rend & count < 10)
                {
                    rend = render(screen.ToArray(), man, ou, left.ToArray());
                    count++;
                }

                if (!rend)
                {
                    Console.WriteLine("FAILED TO RENDER");
                }
                
                if (done)
                {
                    break;
                }
            }

            end(word, win, icon(man));
            return win;
        }
        public static bool render(string[] src, int lives, int ou, string[] left)
        {
            try
            {
                Console.Clear();

                string[] ico = icon(lives);

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
                foreach (string l in left)
                {
                    Console.Write(l + " ");
                }
                Console.WriteLine();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string[] icon(int live)
        {
            string[] ico12 = {"_______","|/    |","|     O",@"|    /|\",@"|     /\",@"|\","__________"};
            string[] ico11 = { "_______", "|/    |", "|     O", @"|    /|\", @"|      \", @"|\", "__________" };
            string[] ico10 = { "_______", "|/    |", "|     O", @"|    /|\", @"|     ", @"|\", "__________" };
            string[] ico9 = { "_______", "|/    |", "|     O", @"|    /|", @"|     ", @"|\", "__________" };
            string[] ico8 = { "_______", "|/    |", "|     O", @"|    /", @"|     ", @"|\", "__________" };
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
            if (live == 12)
            {
                return ico12;
            }
            if (live == 11)
            {
                return ico11;
            }
            if (live == 10)
            {
                return ico10;
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
        public static void end(string word, bool win, string[] man)
        {
            Console.Clear();
            if (win)
            {
                Console.WriteLine("You Win!!!");
                Console.WriteLine();
                foreach (string icon in man)
                {
                    Console.WriteLine(icon);
                }
                Console.WriteLine();
                Console.WriteLine("well done for guessing the word: " + word);
            }
            else
            {
                Console.WriteLine("You Loose :(");
                Console.WriteLine();
                foreach (string icon in man)
                {
                    Console.WriteLine(icon);
                }
                Console.WriteLine();
                Console.WriteLine("The word was: " + word);
            }
        }
    }
}