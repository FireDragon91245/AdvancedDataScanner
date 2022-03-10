using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdvancedDataScanner
{
    public class Program
    {
        //Main Scann Variables Containing every information
        static public ConcurrentDictionary<int, Scanner> scans = new ConcurrentDictionary<int, Scanner>();
        static public ConcurrentDictionary<int, DataStruct> datas = new ConcurrentDictionary<int, DataStruct>();
        static public Save save = new Save();

        static private int lastID = 0;
        static private Boolean exit = false;
        public static readonly ConcurrentDictionary<int, Names> names = new ConcurrentDictionary<int, Names>();
        private static readonly Search Search = new Search();

        static private void Main ()
        {
            while (!exit)
            {
                Commands(Console.ReadLine());
            }
        }

        static private void Commands (string arg)
        {
            if (arg.StartsWith("addfast"))
            {
                String[] args = arg.Split(" ");
                if (args.Length > 2)
                {
                    if (Directory.Exists(args[1]))
                    {
                        lastID++;
                        //Create a new instance of every object
                        scans.TryAdd(lastID, new Scanner());
                        datas.TryAdd(lastID, new DataStruct());
                        names.TryAdd(lastID, new Names());

                        //init Stuff
                        datas[lastID].InitStruct();
                        scans[lastID].Init(args[1], lastID);
                        names[lastID].Type = "Type: Fast";
                        names[lastID].Name = "Name: " + args[2];
                        names[lastID].StartPath = "StartPath: " + args[1];
                        names[lastID].Status = "Status: Processing";


                        ThreadPool.QueueUserWorkItem(cal => scans[lastID].RunScannFast());
                    }
                }
            }
            else if (arg.StartsWith("add"))
            {
                String[] args = arg.Split(" ");
                if (args.Length > 2)
                {
                    args[1] = args[1].Replace("~#", " ");
                    if (Directory.Exists(args[1]))
                    {
                        lastID++;
                        //Create a new instance of every object
                        scans.TryAdd(lastID, new Scanner());
                        datas.TryAdd(lastID, new DataStruct());
                        names.TryAdd(lastID, new Names());

                        //init Stuff
                        datas[lastID].InitStruct();
                        scans[lastID].Init(args[1], lastID);
                        names[lastID].Type = "Type: Normal";
                        names[lastID].Name = "Name: " + args[2];
                        names[lastID].StartPath = "StartPath: " + args[1];
                        names[lastID].Status = "Status: Processing";

                        ThreadPool.QueueUserWorkItem(cal => scans[lastID].RunScann());
                    }
                }
            }
            else if (arg.StartsWith("list"))
            {
                Console.WriteLine("== Qued Scanns ==");
                foreach (int key in datas.Keys)
                {
                    Console.WriteLine("=> ID: " + key + ", " + names[key].Type + ", " + names[key].Status + " ," + names[key].StartPath + ", " + names[key].Name);
                }
                Console.WriteLine("== Qued Scanns ==");
                Console.WriteLine("-} Active Threads: " + ThreadPool.ThreadCount);
            }
            else if (arg.StartsWith("info"))
            {
                if (arg.Length > 5)
                {
                    arg = arg[5..];
                    if (int.TryParse(arg, out int selID))
                    {
                        if (datas.ContainsKey(selID))
                        {
                            Console.WriteLine("== Summery of id " + selID + " ==");
                            Console.WriteLine("Folders: " + datas[selID].stor.stats["FolderCount"]);
                            Console.WriteLine("Files: " + datas[selID].stor.stats["FileCount"]);
                            Console.WriteLine("File(s) Size: " + datas[selID].stor.stats["FileSize"]);
                            Console.WriteLine("Protected Folder: " + datas[selID].stor.stats["FolderError"]);
                            Console.WriteLine("Protected File: " + datas[selID].stor.stats["FileError"]);
                            Console.WriteLine("== Summery of id " + selID + " ==");
                        }
                    }
                }
            }
            else if (arg.StartsWith("type"))
            {
                String[] args = arg.Split(' ');
                if (args.Length > 3)
                {
                    if (int.TryParse(args[1], out int selID))
                    {
                        if (datas[selID].stor.datein.ContainsKey(args[2]))
                        {
                            if (int.TryParse(args[3], out int selItem))
                            {
                                if (datas[selID].stor.datein[args[2]].ContainsKey(selItem))
                                {
                                    FileInfo fi = new FileInfo(datas[selID].stor.datein[args[2]][selItem]);
                                    try
                                    {
                                        Console.WriteLine("== Info for Item " + selItem + " in " + args[2] + " ==");
                                        Console.WriteLine(args[2] + " " + selItem + " => File Name: " + fi.Name);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Extension: " + fi.Extension);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Size: " + fi.Length + " bytes");
                                        Console.WriteLine(args[2] + " " + selItem + " => File Path: " + fi.FullName);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Creation Time: " + fi.CreationTime);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Last Accessed: " + fi.LastAccessTime);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Last Written: " + fi.LastWriteTime);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Atributes: " + fi.Attributes);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Stil Exists: " + fi.Exists);
                                        Console.WriteLine(args[2] + " " + selItem + " => File Read Only: " + fi.IsReadOnly);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("ERROR: File Inaccessible");
                                    }
                                    Console.WriteLine("== Info for Item " + selItem + " in " + args[2] + " ==");
                                }
                            }
                        }
                    }
                }
                else if (args.Length > 2)
                {
                    if (int.TryParse(args[1], out int selID))
                    {
                        if (args[2] == "list")
                        {
                            Console.WriteLine("== Avaidable list Datatypes ==");
                            foreach (String key in datas[selID].stor.datein.Keys)
                            {
                                Console.WriteLine("=> " + key);
                            }
                            Console.WriteLine("== Avaidable list Datatypes ==");
                        }
                        else if (datas[selID].stor.datein.ContainsKey(args[2]))
                        {
                            Console.WriteLine("== List of " + args[2] + " datatypes ==");
                            foreach (int val in datas[selID].stor.datein[args[2]].Keys)
                            {
                                Console.WriteLine(val + "=> " + datas[selID].stor.datein[args[2]][val]);
                            }
                            Console.WriteLine("== List of " + args[2] + " datatypes ==");
                        }
                    }
                }
            }
            else if (arg.StartsWith("get"))
            {
                if (arg.Length > 4)
                {
                    arg = arg[4..];
                    if (int.TryParse(arg, out int selID))
                    {
                        if (datas.ContainsKey(selID))
                        {
                            Console.WriteLine("=====", Console.ForegroundColor = ConsoleColor.Red);
                            Console.Write("Found ", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.Write(datas[selID].stor.stats["FileCount"] + " ", Console.ForegroundColor = ConsoleColor.Cyan);
                            Console.WriteLine("Files!", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.Write("Found ", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.Write(datas[selID].stor.stats["FolderCount"] + " ", Console.ForegroundColor = ConsoleColor.Cyan);
                            Console.WriteLine("Folders!", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.WriteLine("=====", Console.ForegroundColor = ConsoleColor.Red);
                            Console.Write("Found ", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.Write(datas[selID].stor.stats["CleanFolders"] + " ", Console.ForegroundColor = ConsoleColor.Cyan);
                            Console.WriteLine("Empty Folders!", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.Write(datas[selID].stor.stats["FolderError"] + " ", Console.ForegroundColor = ConsoleColor.Cyan);
                            Console.WriteLine("Denied Folders OR errors", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.Write(datas[selID].stor.stats["FileError"] + " ", Console.ForegroundColor = ConsoleColor.Cyan);
                            Console.WriteLine("Denied Files OR errors", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.WriteLine("=====", Console.ForegroundColor = ConsoleColor.Red);
                            Console.Write("Total file Size ", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.Write(( datas[selID].stor.stats["FileSize"] / 1048576 ).ToString(), Console.ForegroundColor = ConsoleColor.DarkGreen);
                            Console.WriteLine(" MB!", Console.ForegroundColor = ConsoleColor.DarkRed);
                            Console.WriteLine("=====", Console.ForegroundColor = ConsoleColor.Red);
                            int fails = 0;
                            foreach (String key in datas[selID].stor.datein.Keys.ToArray())
                            {
                                Int64 size = 0;
                                foreach (String val in datas[selID].stor.datein[key].Values.ToArray())
                                {
                                    try
                                    {
                                        size += new FileInfo(val).Length;
                                    }
                                    catch (Exception)
                                    {
                                        fails++;
                                    }
                                }
                                Console.Write("Found ", Console.ForegroundColor = ConsoleColor.DarkRed);
                                Console.Write(datas[selID].stor.datein[key].Values.ToArray().Length + " ", Console.ForegroundColor = ConsoleColor.Cyan);
                                Console.Write(key + " ", Console.ForegroundColor = ConsoleColor.DarkYellow);
                                Console.Write("Files with a Size of ", Console.ForegroundColor = ConsoleColor.DarkRed);
                                Console.Write(( size / 1048576 ) + " ", Console.ForegroundColor = ConsoleColor.DarkGreen);
                                Console.WriteLine("MB!", Console.ForegroundColor = ConsoleColor.DarkRed);
                            }
                            Console.WriteLine("=====", Console.ForegroundColor = ConsoleColor.Red);
                            Console.WriteLine(datas[selID].stor.start.ToString(), Console.ForegroundColor = ConsoleColor.Cyan);
                            Console.WriteLine(datas[selID].stor.end.ToString(), Console.ForegroundColor = ConsoleColor.Cyan);
                            Console.WriteLine(datas[selID].stor.end.Subtract(datas[selID].stor.start).ToString(), Console.ForegroundColor = ConsoleColor.DarkYellow);
                            Console.WriteLine("=====", Console.ForegroundColor = ConsoleColor.Red);
                            Console.WriteLine("-} Not Acountet Files: " + fails);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }
            }
            else if (arg.StartsWith("terminate"))
            {
                if (arg.Length > 10)
                {
                    arg = arg[10..];
                    if (int.TryParse(arg, out int selID))
                    {
                        if (names[selID].Status != "Processing")
                        {

                            long memBefore = GC.GetTotalMemory(true);
                            scans.Remove(selID, out _);
                            names.Remove(selID, out _);
                            datas.Remove(selID, out _);
                            long memAfter = GC.GetTotalMemory(true);
                            Console.WriteLine("=> id " + selID + " was terminated and " + ( memBefore - memAfter ) + " bytes of Memory was safed");
                        }
                        else
                        {
                            Console.WriteLine("This Scann is still Processing, cannot terminate aktive Scanns!");
                        }
                    }
                }
            }
            else if (arg.StartsWith("exit"))
            {
                foreach (int key in datas.Keys)
                {
                    datas.Remove(key, out _);
                }
                foreach (int key in names.Keys)
                {
                    names.Remove(key, out _);
                }
                foreach (int key in scans.Keys)
                {
                    scans.Remove(key, out _);
                }
                exit = true;
                Environment.Exit(0);
            }
            else if (arg.StartsWith("help"))
            {
                String[] help =
                {
                    "== help ==",
                    "exit => exits the Programm",
                    "add [FolderPath] [Name] => add a folder to the scann queue",
                    "addfast [FolderPath] [Name] => execute a faster version of the scanner on the folder path BUT it will not monitor data like file size,...",
                    "list => display the list of running or saved/loaded scanns",
                    "info [ScannID] => shows a very short summery of the scann",
                    "type [ScannID] list => shows a list of all found file types in this scann",
                    "type [ScannID] [FileType] => Displays a list of all files with path from that file type",
                    "type [ScannID] [FileType] [FileID] => shows dietailed info about that file",
                    "get [ScannID] => display a more detailed and better looking summery of the scann (Recomanded over info)",
                    "terminate [ScannID] => delete the scann and its data to free up memory",
                    "search [ScannID] [type|name|minSize|maxSize] [argument] => searches for a given type with a given critera ina scannID",
                    "search [ScannID] name [argument] => searches for a file name without file extension in the scann id",
                    "search [ScannID] type [argument] => searches for a file extension in the scann id for example '.exe'",
                    "search [ScannID] minSize [argument] => searches for files that are larger then [argument] in bytes in scannID",
                    "search [ScannÍD] maxSize [argument] => searches for files that are smaler then [argument] in bytes in scannID",
                    "search [ScannÍD] path [argument] => searches the file path for the [argument] in scannID",
                    "recount [ScannID] => recounts FileCount, FileErrors and FileSize for the given scannID ONLY recounts files that are already scanned dosnt scann new ones!",
                    "save [ScannID] minimal => Saves only the esentials of a scann to a file in the program folder",
                    "save [ScannID] normal => Saves a good amount of informattion to a file in the program direktori (like the 'get' command)",
                    "save [ScannID] advanced => Saves nomral + every file path categorised in file extensions and nummberd + all avaidable file types",
                    "save [ScannID] all => Saves absoulute anything the 'type [ScannID] [FileType] [FileID]' command run for every scanned file + advanced + normal",
                    "== help ==",
                    "[FolderPath] => the path to a folder from the windows explorer",
                    "[ScannID] => the id of a scan see in command {list} and will display if a scann finishes",
                    "[FileType] => file extension for example .exe, see command {type [ScannID] list}",
                    "[FileID] => id of a file in a file type category see command {type [ScannID] [FileType]} infront of the '=>'",
                    "[Name] => the name of the scann, use it as a reminder that you know for what the scann is (NO SPACES ALLOWED)",
                    "[argument] => a argument thats required by the previous construktor {search [ScannID] [name|type] [argument]} = string, {search [ScannID] [minSize,maxSize] [argument]} = number",
                    "== help =="
                };
                foreach (String s in help)
                {
                    Console.WriteLine(s);
                }
            }
            else if (arg.StartsWith("search"))
            {
                String[] args = arg.Split(' ');
                if (args.Length > 3)
                {
                    if (!int.TryParse(args[1], out int scannID))
                        return;
                    if (args[2] == "type")
                    {
                        ThreadPool.QueueUserWorkItem(cal => Search.SearchName(args[3], scannID));
                    }
                    else if (args[2].StartsWith("name"))
                    {
                        ThreadPool.QueueUserWorkItem(cal => Search.SearchName(args[3], scannID));
                    }
                    else if (args[2] == "minSize")
                    {
                        if (Int64.TryParse(args[3], out Int64 minSize))
                        {
                            ThreadPool.QueueUserWorkItem(cal => Search.SearchMinSize(minSize, scannID));
                        }
                    }
                    else if (args[2] == "maxSize")
                    {
                        if (Int64.TryParse(args[3], out Int64 maxSize))
                        {
                            ThreadPool.QueueUserWorkItem(cal => Search.SearchMaxSize(maxSize, scannID));
                        }
                    }
                    else if (args[2] == "path")
                    {
                        ThreadPool.QueueUserWorkItem(cal => Search.SearchPath(args[3], scannID));
                    }
                }
            }
            else if (arg.StartsWith("copy"))
            {
                if (arg.Length > 4)
                {
                    arg = arg[5..];
                    Console.WriteLine(arg);
                    if (int.TryParse(arg, out int scannID))
                    {
                        if (datas.ContainsKey(scannID))
                        {
                            lastID++;
                            datas.TryAdd(lastID, new DataStruct());
                            names.TryAdd(lastID, new Names());
                            datas[lastID].InitStruct();
                            foreach (String key in datas[scannID].stor.datein.Keys)
                            {
                                foreach (String val in datas[scannID].stor.datein[key].Values)
                                {
                                    datas[lastID].AddSingle(new FileInfo(val).Extension, val);
                                }
                            }
                            foreach (String key in datas[scannID].stor.stats.Keys)
                            {
                                datas[lastID].AddStat(key, datas[lastID].stor.stats[key]);
                            }
                            datas[lastID].stor.start = datas[scannID].stor.start;
                            datas[lastID].stor.end = datas[scannID].stor.end;
                            names[lastID].Name = names[scannID].Name + " - Copy";
                            names[lastID].StartPath = names[scannID].StartPath;
                            names[lastID].Type = names[scannID].Type;
                            Console.WriteLine("Copy of id: " + scannID + " made to id: " + lastID);
                        }
                    }
                }
            }
            else if (arg.StartsWith("recount"))
            {
                if (int.TryParse(arg[8..], out int scannID))
                {
                    if (datas.ContainsKey(scannID))
                    {
                        ThreadPool.QueueUserWorkItem(cal => Search.ReCount(scannID));
                        Console.WriteLine("Recounting files in scannID: " + scannID);
                    }
                }
            }
            else if (arg.StartsWith("exit"))
            {
                foreach (int key in datas.Keys)
                {
                    datas.Remove(key, out _);
                }
                foreach (int key in names.Keys)
                {
                    names.Remove(key, out _);
                }
                foreach (int key in scans.Keys)
                {
                    scans.Remove(key, out _);
                }
                Environment.Exit(0);
            }
            else if (arg.StartsWith("save"))
            {
                String[] args = arg.Split(" ");
                if (args.Length < 3)
                    return;
                if (int.TryParse(args[1], out int scannID))
                {
                    if (args[2].Equals("minimal", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Minimal Save for ID: " + scannID + " was Started");
                        ThreadPool.QueueUserWorkItem(cal => save.Save_minimal(scannID));
                    }
                    else if (args[2].Equals("normal", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Normal Save for ID: " + scannID + " was Started");
                        ThreadPool.QueueUserWorkItem(cal => save.Save_normal(scannID));
                    }
                    else if (args[2].Equals("advanced", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Advanced Save for ID: " + scannID + " was Started");
                        ThreadPool.QueueUserWorkItem(cal => save.Save_advanced(scannID));
                    }
                    else if (args[2].Equals("all", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("All Save for ID: " + scannID + " was Started");
                        ThreadPool.QueueUserWorkItem(cal => save.Save_all(scannID));
                    }
                }
            }
        }
    }
}
