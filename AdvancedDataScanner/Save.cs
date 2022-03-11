using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedDataScanner
{
    public class Save
    {
        public void Save_minimal (int scannID)
        {
            String fileName = "./save_minimal_" + DateTime.Now.ToString().Replace(" ", "_").Replace(".", "-").Replace(":", "-") + ".txt";
            File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                //minimal
                writer.WriteLine("--Scann output Minimal--");
                writer.WriteLine("");
                writer.WriteLine("--General Scann Time--");
                writer.WriteLine("Scann Start Date: " + Program.names[scannID].start);
                writer.WriteLine("Scann End Date: " + Program.names[scannID].end);
                writer.WriteLine("-} Scann Time: " + Program.names[scannID].end.Subtract(Program.names[scannID].start));
                writer.WriteLine("");
                writer.WriteLine("--General Scann Information--");
                writer.WriteLine("Scann " + Program.names[scannID].Type);
                writer.WriteLine("Scann " + Program.names[scannID].StartPath);
                writer.WriteLine("Scann " + Program.names[scannID].Name);
                writer.WriteLine("Scann ID: " + scannID);
                writer.WriteLine("");
                writer.WriteLine("--General Scann Results--");
                writer.WriteLine("File Count: " + Program.datas[scannID].stor.stats["FileCount"]);
                writer.WriteLine("Folder Count: " + Program.datas[scannID].stor.stats["FolderCount"]);
                writer.WriteLine("Total File Size: " + Program.datas[scannID].stor.stats["FileSize"] + " bytes, " + Program.datas[scannID].stor.stats["FileSize"] / 1048576 + " megabytes");
                writer.WriteLine("Empty Folders: " + Program.datas[scannID].stor.stats["CleanFolders"]);
                writer.WriteLine("File Errors: " + Program.datas[scannID].stor.stats["FileError"]);
                writer.WriteLine("Folder Errors: " + Program.datas[scannID].stor.stats["FolderError"]);
            }
            Console.WriteLine("Save minimal for id: " + scannID + " finished, saved at file path: " + new FileInfo(fileName).FullName);
        }

        public void Save_normal (int scannID)
        {
            String fileName = "./save_normal_" + DateTime.Now.ToString().Replace(" ", "_").Replace(".", "-").Replace(":", "-") + ".txt";
            File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                //minimal
                writer.WriteLine("--Scann output Normal--");
                writer.WriteLine("");
                writer.WriteLine("--General Scann Information--");
                writer.WriteLine("Scann " + Program.names[scannID].Type);
                writer.WriteLine("Scann " + Program.names[scannID].StartPath);
                writer.WriteLine("Scann " + Program.names[scannID].Name);
                writer.WriteLine("Scann ID: " + scannID);
                writer.WriteLine("");

                //normal
                writer.WriteLine("--Advanced scann Info--");
                writer.WriteLine("=====");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["FileCount"] + " Files!");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["FolderCount"] + " Folders!");
                writer.WriteLine("=====");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["CleanFolders"] + " Empty Folders!");
                writer.WriteLine(Program.datas[scannID].stor.stats["FolderError"] + " Denied Folders OR errors");
                writer.WriteLine(Program.datas[scannID].stor.stats["FileError"] + " Denied Files OR errors");
                writer.WriteLine("=====");
                writer.WriteLine("Total file Size " + ( Program.datas[scannID].stor.stats["FileSize"] / 1048576 ).ToString() + " MB!");
                writer.WriteLine("=====");
                int fails = 0;
                foreach (String key in Program.datas[scannID].stor.datein.Keys.ToArray())
                {
                    Int64 size = 0;
                    foreach (String val in Program.datas[scannID].stor.datein[key].Values.ToArray())
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
                    writer.WriteLine("Found " + Program.datas[scannID].stor.datein[key].Values.ToArray().Length + " " + key + " Files with a Size of " + ( size / 1048576 ) + " MB!");
                }
                writer.WriteLine("=====");
                writer.WriteLine("Scann Start:" + Program.names[scannID].start.ToString());
                writer.WriteLine("Scann End: " + Program.names[scannID].end.ToString());
                writer.WriteLine("Total Scann Time: " + Program.names[scannID].end.Subtract(Program.names[scannID].start).ToString());
                writer.WriteLine("=====");
                writer.WriteLine("-} Not Acountet Files: " + fails);
            }
            Console.WriteLine("Save normal for id: " + scannID + " finished, saved at file path: " + new FileInfo(fileName).FullName);
        }

        public void Save_advanced (int scannID)
        {
            String fileName = "./save_advanced_" + DateTime.Now.ToString().Replace(" ", "_").Replace(".", "-").Replace(":", "-") + ".txt";
            File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                //minimal
                writer.WriteLine("--Scann output Advanced--");
                writer.WriteLine("");
                writer.WriteLine("--General Scann Information--");
                writer.WriteLine("Scann " + Program.names[scannID].Type);
                writer.WriteLine("Scann " + Program.names[scannID].StartPath);
                writer.WriteLine("Scann " + Program.names[scannID].Name);
                writer.WriteLine("Scann ID: " + scannID);
                writer.WriteLine("");

                //normal
                writer.WriteLine("--Advanced scann Info--");
                writer.WriteLine("=====");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["FileCount"] + " Files!");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["FolderCount"] + " Folders!");
                writer.WriteLine("=====");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["CleanFolders"] + " Empty Folders!");
                writer.WriteLine(Program.datas[scannID].stor.stats["FolderError"] + " Denied Folders OR errors");
                writer.WriteLine(Program.datas[scannID].stor.stats["FileError"] + " Denied Files OR errors");
                writer.WriteLine("=====");
                writer.WriteLine("Total file Size " + ( Program.datas[scannID].stor.stats["FileSize"] / 1048576 ).ToString() + " MB!");
                writer.WriteLine("=====");
                int fails = 0;
                foreach (String key in Program.datas[scannID].stor.datein.Keys.ToArray())
                {
                    Int64 size = 0;
                    foreach (String val in Program.datas[scannID].stor.datein[key].Values.ToArray())
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
                    writer.WriteLine("Found " + Program.datas[scannID].stor.datein[key].Values.ToArray().Length + " " + key + " Files with a Size of " + ( size / 1048576 ) + " MB!");
                }
                writer.WriteLine("=====");
                writer.WriteLine("Scann Start:" + Program.names[scannID].start.ToString());
                writer.WriteLine("Scann End: " + Program.names[scannID].end.ToString());
                writer.WriteLine("Total Scann Time: " + Program.names[scannID].end.Subtract(Program.names[scannID].start).ToString());
                writer.WriteLine("=====");
                writer.WriteLine("-} Not Acountet Files: " + fails);
                writer.WriteLine("");
                writer.WriteLine("--Adva Scann Results--");
                writer.WriteLine("-File Types-");
                foreach (String s in Program.datas[scannID].stor.datein.Keys)
                {
                    writer.WriteLine(s);
                }
                writer.WriteLine("");
                writer.WriteLine("-Files-");
                foreach (String key in Program.datas[scannID].stor.datein.Keys)
                {
                    writer.WriteLine("");
                    writer.WriteLine("-" + key + " Files-");
                    foreach (KeyValuePair<int, String> val in Program.datas[scannID].stor.datein[key])
                    {
                        writer.WriteLine(key + " " + val.Key + "=> " + val.Value);
                    }
                }
            }
            Console.WriteLine("Save advanced for id: " + scannID + " finished, saved at file path: " + new FileInfo(fileName).FullName);
        }

        public void Save_all (int scannID)
        {
            String fileName = "./save_all_" + DateTime.Now.ToString().Replace(" ", "_").Replace(".", "-").Replace(":", "-") + ".txt";
            File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                //minimal
                writer.WriteLine("--Scann output All--");
                writer.WriteLine("");
                writer.WriteLine("--General Scann Information--");
                writer.WriteLine("Scann " + Program.names[scannID].Type);
                writer.WriteLine("Scann " + Program.names[scannID].StartPath);
                writer.WriteLine("Scann " + Program.names[scannID].Name);
                writer.WriteLine("Scann ID: " + scannID);
                writer.WriteLine("");

                //normal
                writer.WriteLine("--Advanced scann Info--");
                writer.WriteLine("=====");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["FileCount"] + " Files!");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["FolderCount"] + " Folders!");
                writer.WriteLine("=====");
                writer.WriteLine("Found " + Program.datas[scannID].stor.stats["CleanFolders"] + " Empty Folders!");
                writer.WriteLine(Program.datas[scannID].stor.stats["FolderError"] + " Denied Folders OR errors");
                writer.WriteLine(Program.datas[scannID].stor.stats["FileError"] + " Denied Files OR errors");
                writer.WriteLine("=====");
                writer.WriteLine("Total file Size " + ( Program.datas[scannID].stor.stats["FileSize"] / 1048576 ).ToString() + " MB!");
                writer.WriteLine("=====");
                int fails = 0;
                foreach (String key in Program.datas[scannID].stor.datein.Keys.ToArray())
                {
                    Int64 size = 0;
                    foreach (String val in Program.datas[scannID].stor.datein[key].Values.ToArray())
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
                    writer.WriteLine("Found " + Program.datas[scannID].stor.datein[key].Values.ToArray().Length + " " + key + " Files with a Size of " + ( size / 1048576 ) + " MB!");
                }
                writer.WriteLine("=====");
                writer.WriteLine("Scann Start:" + Program.names[scannID].start.ToString());
                writer.WriteLine("Scann End: " + Program.names[scannID].end.ToString());
                writer.WriteLine("Total Scann Time: " + Program.names[scannID].end.Subtract(Program.names[scannID].start).ToString());
                writer.WriteLine("=====");
                writer.WriteLine("-} Not Acountet Files: " + fails);
                writer.WriteLine("");
                writer.WriteLine("--Adva Scann Results--");
                writer.WriteLine("-File Types-");
                foreach (String s in Program.datas[scannID].stor.datein.Keys)
                {
                    writer.WriteLine(s);
                }
                writer.WriteLine("");
                writer.WriteLine("-Files-");
                foreach (String key in Program.datas[scannID].stor.datein.Keys)
                {
                    writer.WriteLine("");
                    writer.WriteLine("-" + key + " Files-");
                    foreach (KeyValuePair<int, String> val in Program.datas[scannID].stor.datein[key])
                    {
                        writer.WriteLine(key + " " + val.Key + "=> " + val.Value);
                    }
                }
                writer.WriteLine("");
                writer.WriteLine("--Foreach File Advanced Info--");
                foreach (String key in Program.datas[scannID].stor.datein.Keys)
                {
                    writer.WriteLine("");
                    writer.WriteLine("-Foreach " + key + " File-");
                    foreach (KeyValuePair<int, String> val in Program.datas[scannID].stor.datein[key])
                    {
                        writer.WriteLine("");
                        writer.WriteLine("-File " + key + " " + val.Key + "-");
                        try
                        {
                            FileInfo fi = new FileInfo(val.Value);
                            writer.WriteLine(" => File Name: " + fi.Name);
                            writer.WriteLine(" => File Extension: " + fi.Extension);
                            writer.WriteLine(" => File Size: " + fi.Length + " bytes");
                            writer.WriteLine(" => File Path: " + fi.FullName);
                            writer.WriteLine(" => File Creation Time: " + fi.CreationTime);
                            writer.WriteLine(" => File Last Accessed: " + fi.LastAccessTime);
                            writer.WriteLine(" => File Last Written: " + fi.LastWriteTime);
                            writer.WriteLine(" => File Atributes: " + fi.Attributes);
                            writer.WriteLine(" => File Stil Exists: " + fi.Exists);
                            writer.WriteLine(" => File Read Only: " + fi.IsReadOnly);
                        }
                        catch (Exception e)
                        {
                            writer.WriteLine("File ist nor Accesable! Missing Permissions or the File was Deletet. ExeptionType: " + e.InnerException);
                        }
                    }
                }
            }
            Console.WriteLine("Save All for id: " + scannID + " finished, saved at file path: " + new FileInfo(fileName).FullName);
        }

        public void StorToFile (int scannID, string fileName)
        {
            if (!Directory.Exists("./stor"))
            {
                Directory.CreateDirectory("./stor");
            }
            string FilePathData = $"./stor/{fileName}_data.json";
            string FilePathStat = $"./stor/{fileName}_stat.json";
            string FilePathNames = $"./stor/{fileName}_name.json";
            try
            {
                File.WriteAllText(FilePathData, "");
                File.WriteAllText(FilePathStat, "");
                File.WriteAllText(FilePathNames, "");
            }
            catch (Exception)
            {
                Console.WriteLine("Filename isnt valid or the file is dupplicate");
                return;
            }
            using (StreamWriter writer = new StreamWriter(FilePathData))
            {
                writer.Write(JsonConvert.SerializeObject(Program.datas[scannID].stor.datein, Formatting.Indented));
            }
            using (StreamWriter writer = new StreamWriter(FilePathStat))
            {
                writer.Write(JsonConvert.SerializeObject(Program.datas[scannID].stor.stats, Formatting.Indented));
            }
            using (StreamWriter writer = new StreamWriter(FilePathNames))
            {
                writer.Write(JsonConvert.SerializeObject(Program.names[scannID], Formatting.Indented));
            }
            Console.WriteLine($"Finished saveing to JSON for id {scannID}.");
        }

        public void LoadFromFiles (int scannID, string fileName)
        {
            if (Directory.Exists("./stor"))
            {
                Console.WriteLine("1");
                if (File.Exists($"./stor/{fileName}_data.json") && File.Exists($"./stor/{fileName}_stat.json") && File.Exists($"./stor/{fileName}_name.json"))
                {
                    try
                    {
                        Console.WriteLine($"{Environment.CurrentDirectory}/stor/{fileName}_stat.json");
                        Program.datas[scannID].stor.datein = JsonConvert.DeserializeObject<ConcurrentDictionary<String, ConcurrentDictionary<int, String>>>(File.ReadAllText($"./stor/{fileName}_data.json"));
                        Program.datas[scannID].stor.stats = JsonConvert.DeserializeObject<ConcurrentDictionary<String, Int64>>(File.ReadAllText($"./stor/{fileName}_stat.json"));
                        Program.names[scannID] = JsonConvert.DeserializeObject<Names>(File.ReadAllText($"./stor/{fileName}_name.json"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"The JSON file wasnt in the right format a error acured reading stoped and scann was deletet! {e.Message}");
                        if (Program.datas.ContainsKey(scannID))
                            Program.datas.Remove(scannID, out _);
                        if (Program.names.ContainsKey(scannID))
                            Program.names.Remove(scannID, out _);
                    }
                }
            }
            Console.WriteLine($"Finished Loading JSON to scannID: {scannID}!");
        }
    }
}
