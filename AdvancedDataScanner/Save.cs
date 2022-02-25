using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace AdvancedDataScanner
{
    public class Save
    {
        public void  Save_minimal (int scannID)
        {
            String fileName = "./save_minimal_" + DateTime.Now.ToString().Replace(" ", "_").Replace(".", "-").Replace(":", "-") + ".txt";
            File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                //minimal
                writer.WriteLine("--Scann output Minimal--");
                writer.WriteLine("");
                writer.WriteLine("--General Scann Time--");
                writer.WriteLine("Scann Start Date: " + Program.datas[scannID].stor.start);
                writer.WriteLine("Scann End Date: " + Program.datas[scannID].stor.end);
                writer.WriteLine("-} Scann Time: " + Program.datas[scannID].stor.end.Subtract(Program.datas[scannID].stor.start));
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
                writer.WriteLine("Scann Start:" + Program.datas[scannID].stor.start.ToString());
                writer.WriteLine("Scann End: " + Program.datas[scannID].stor.end.ToString());
                writer.WriteLine("Total Scann Time: " + Program.datas[scannID].stor.end.Subtract(Program.datas[scannID].stor.start).ToString());
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
                writer.WriteLine("Scann Start:" + Program.datas[scannID].stor.start.ToString());
                writer.WriteLine("Scann End: " + Program.datas[scannID].stor.end.ToString());
                writer.WriteLine("Total Scann Time: " + Program.datas[scannID].stor.end.Subtract(Program.datas[scannID].stor.start).ToString());
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
                writer.WriteLine("Scann Start:" + Program.datas[scannID].stor.start.ToString());
                writer.WriteLine("Scann End: " + Program.datas[scannID].stor.end.ToString());
                writer.WriteLine("Total Scann Time: " + Program.datas[scannID].stor.end.Subtract(Program.datas[scannID].stor.start).ToString());
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
    }
}
