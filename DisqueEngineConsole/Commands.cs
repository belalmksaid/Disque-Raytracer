using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DisqueEngineConsole
{
    public static class C
    {
        public static void Print(object s)
        {
            Console.WriteLine(s);
        }
    }
    public static class Commands
    {
        static Dictionary<string, Command> bases = new Dictionary<string, Command>();
        static bool called = false;
        public static T[] Split<T>(T[] array, int index)
        {
            if(array.Length == 0)
            {
                return array;
            }
           return array.Skip(index).ToArray();
        }

        public static void Execute(string cmd)
        {
            Regex r = new Regex(@"[ ]{ 2, }", RegexOptions.None);
            string processed = r.Replace(cmd, String.Empty);
            string[] list = processed.Split(new string[] { " "}, StringSplitOptions.RemoveEmptyEntries);
            //C.Print(list.Length);
            if (list.Length > 0)
            {
                if (bases.ContainsKey(list[0]))
                {
                    bases[list[0]].Execute(Commands.Split(list, 1));
                }
                else
                {
                    Console.WriteLine("Command is empty.");
                }
            }
        }

        static void createCommands()
        {
            createCommand("exit", null, delegate (string[] d, dynamic obj)
            {
                Environment.Exit(0);
            });
            createCommand("replicate", null, delegate (string[] d, dynamic obj)
            {
                string path = Assembly.GetExecutingAssembly().Location; Process.Start(path);
            });
            createCommand("set", null,delegate (string[] d, dynamic obj)
            {
                int semiIndex = -1;
                string conc = "";
                for (int i = 0; i < d.Length; i++)
                {
                    conc += d[i] + (i == d.Length - 1 ? "" : " ");
                }
                semiIndex = conc.IndexOf(";");
                if (semiIndex != -1)
                {
                    if (conc.Contains("="))
                    {
                        string[] split2 = conc.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        if (split2.Length == 2)
                        {
                            if (!Memory.ContainsKey(split2[0].Replace(" ", "")))
                            {
                                Memory.Add(split2[0].Replace(" ", ""), split2[1].Substring(0, split2[1].Length - 1));
                            }
                            else
                            {
                                Memory[split2[0].Replace(" ", "")] = split2[1].Substring(0, split2[1].Length - 1);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value to assign.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No '=' used to assign a value");
                    }
                }
                else
                {
                    Console.WriteLine("No semicolon was used.");
                }
            });
            createCommand("get", null, delegate (string[] d, dynamic obj)
             {
                if (d.Length > 0)
                {
                    if (Memory.ContainsKey(d[0]))
                    {
                        Console.WriteLine(Memory[d[0]]);
                    }
                    else
                    {
                        Console.WriteLine("Datum does not exist");
                    }
                }
                else
                {
                    Console.WriteLine("Empty command.");
                }
            });
            createCommand("show", null, delegate (string[] d, dynamic obj)
            {
                
            }).Add(new Command("memory", delegate (string[] d, dynamic obj)
            {
                foreach (KeyValuePair<string, string> pair in Memory)
                {
                    C.Print(pair.Key + "                     " + pair.Value);
                }
            }, null));
            (createCommand("runFunction", new Dictionary<string, Func<string[], int>>(), delegate (string[] d, dynamic obj) {
                if (d.Length > 0)
                {
                    if (obj.ContainsKey(d[0]))
                    {
                        obj[d[0]](Commands.Split(d, 1));
                    }
                }
            }).MemoryObject as Dictionary<string, Func<string[], int>>).Add("booleanSpeed", delegate(string[] d) {
                Stopwatch s = new Stopwatch();
                s.Start();
                float j = 0;
                long time = 0;
                float timeSum = 0;
                var addA = new Func<float, float, float>((a, b) => { return a + b; });
                var addB = new Func<float, float, float>((a, b) => { if (a > b) return a + b; return b + a; });
                for (int k = 0; k < 100; k++)
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        j = addA(i, i + 1);
                    }
                    s.Stop();
                    time = s.ElapsedMilliseconds;
                    s.Reset();
                    s.Start();
                    for (int i = 0; i < 1000000; i++)
                    {
                        j = addB(i, i + 1);
                    }
                    s.Stop();
                    time = s.ElapsedMilliseconds - time;
                    timeSum += time;
                    Console.WriteLine(time);
                }
                Console.WriteLine("Total: " + timeSum / 100.0);
                return 0;
            });
        }

        static Command createCommand(string name, dynamic obj, Command.CommandDelegate del) {
            Command a = new Command(name, del, obj);
            bases.Add(name, a);
            return a;
        }

        public static Dictionary<string, string> Memory = new Dictionary<string, string>();

        public static List<string> CommandMemory = new List<string>();

        public static int UpIndex = -1;

        public static void Read()
        {
            if(!called)
            {
                called = true;
                createCommands();
                while(true)
                {
                    string s = Console.ReadLine();
                    CommandMemory.Add(s);
                    UpIndex = CommandMemory.Count - 1;
                    Execute(s);
                    //if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                    //{
                    //    Console.CursorLeft -= 1;
                    //    Console.Write(CommandMemory[UpIndex]);
                    //    Console.CursorLeft -= 1;
                    //    UpIndex = (UpIndex == 0 ? 0 : UpIndex - 1);
                    //}
                }
            }
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
    public class Command
    {
        Dictionary<string, Command> children = new Dictionary<string, Command>();
        public object MemoryObject;
        string _name = "";
        public delegate void CommandDelegate(string[] cmd, dynamic obj);
        CommandDelegate action;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Command(string name, CommandDelegate act, object obj)
        {
            _name = name;
            action = act;
            MemoryObject = obj;
        }

        public Command(string name, CommandDelegate act, object obj, params Command[] child)
        {
            _name = name;
            action = act;
            MemoryObject = obj;
            Add(child);
        }

        public void Execute(string[] param)
        {
            if (param.Length > 0)
            {
                if (children.ContainsKey(param[0]))
                {
                    children[param[0]].Execute(Commands.Split(param, 1));
                }
                else
                {
                    implementAction(param);
                }
            }
            else
            {
                implementAction(Commands.Split(param, 1));
            }
        }
        
        void implementAction(string[] p)
        {
            if(action != null)
            {
                action(p, MemoryObject);
            }
            else
            {
                Console.WriteLine("This command has no action.");
            }

        }

        public void Add(params Command[] child)
        {
            for (int i = 0; i < child.Length; i++)
            {
                children.Add(child[i].Name, child[i]);
            }
        }

        public CommandDelegate Action
        {
            get { return action; }
            set { action = value; }
        }
    }
}
