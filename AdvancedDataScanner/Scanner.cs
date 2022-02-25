using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedDataScanner
{
    public class Scanner
    {
        //Folder To Start Scanning From
        private String startDictionary = "";
        private int thisScannID = 0;

        //initailize data for the scanner to work
        public void Init (string dictionary, int id)
        {
            thisScannID = id;
            startDictionary = dictionary;
            Console.WriteLine("Added scan to Que for startPath: " + startDictionary);
            Program.datas[thisScannID].stor.start = DateTime.Now;
        }

        //begin a scann
        public void RunScann ()
        {
            List<string> toScann = new List<string>
            {
                startDictionary
            };

            while (toScann.Count > 0)
            {
                foreach (String scannPath in toScann.ToArray())
                {
                    Program.datas[thisScannID].AddStat("FolderCount", 1);
                    toScann.Remove(scannPath);
                    try
                    {
                        if (new DirectoryInfo(scannPath).GetFileSystemInfos().Length == 0)
                            Program.datas[thisScannID].AddStat("CleanFolders", 1);
                        foreach (String toAddToToScann in Directory.GetDirectories(scannPath))
                        {
                            toScann.Add(toAddToToScann);
                        }
                    }
                    catch (Exception)
                    {
                        Program.datas[thisScannID].AddStat("FolderError", 1);
                    }
                    try
                    {
                        foreach (String filePath in Directory.GetFiles(scannPath))
                        {
                            Program.datas[thisScannID].AddSingle(Path.GetExtension(filePath), filePath);
                            Program.datas[thisScannID].AddStat("FileCount", 1);
                            Program.datas[thisScannID].AddStat("FileSize", (int) new FileInfo(filePath).Length);
                        }
                    }
                    catch (Exception)
                    {
                        Program.datas[thisScannID].AddStat("FileError", 1);
                    }
                }
            }

            Console.WriteLine("Scann with id " + thisScannID + " finsihed scanning");
            Program.names[thisScannID].Status = "Status: Finished";
            Program.datas[thisScannID].stor.end = DateTime.Now;
            Program.scans.Remove(thisScannID);
        }

        public void RunScannFast ()
        {
            List<string> toScann = new List<string>
            {
                startDictionary
            };

            while (toScann.Count > 0)
            {
                foreach (String scannPath in toScann.ToArray())
                {
                    Program.datas[thisScannID].AddStat("FolderCount", 1);
                    toScann.Remove(scannPath);
                    try
                    {
                        foreach (String toAddToToScann in Directory.GetDirectories(scannPath))
                        {
                            toScann.Add(toAddToToScann);
                        }
                    }
                    catch (Exception)
                    {
                        Program.datas[thisScannID].AddStat("FolderError", 1);
                    }
                    try
                    {
                        foreach (String filePath in Directory.GetFiles(scannPath))
                        {
                            Program.datas[thisScannID].AddStat("FileCount", 1);
                        }
                    }
                    catch (Exception)
                    {
                        Program.datas[thisScannID].AddStat("FileError", 1);
                    }
                }
            }

            Console.WriteLine("Scann with id " + thisScannID + " finsihed scanning");
            Program.names[thisScannID].Status = "Status: Finished";
            Program.datas[thisScannID].stor.end = DateTime.Now;
            Program.scans.Remove(thisScannID);
        }
    }
}
