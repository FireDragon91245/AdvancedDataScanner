using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdvancedDataScanner
{
    public class Search
    {
        public void SearchName(String searchName, int scannID)
        {
            Console.WriteLine("Search after Name: " + searchName + " scannID: " + scannID);
            foreach (String key in Program.datas[scannID].stor.datein.Keys)
            {
                foreach (int value in Program.datas[scannID].stor.datein[key].Keys)
                {
                    if (!new FileInfo(Program.datas[scannID].stor.datein[key][value]).Name[key.Length..].Contains(searchName))
                    {
                        Program.datas[scannID].stor.datein[key].Remove(value);
                    }
                }
                if (Program.datas[scannID].stor.datein[key].Count < 1) Program.datas[scannID].stor.datein.Remove(key);
            }
            ReCount(scannID);
            Console.WriteLine("Serch in scann: " + scannID + " finished");
        }

        public void SearchPath(String searchPath, int scannID)
        {
            Console.WriteLine("Search after Path: " + searchPath + " scann: " + scannID);
            foreach (string key in Program.datas[scannID].stor.datein.Keys)
            {
                foreach (int value in Program.datas[scannID].stor.datein[key].Keys)
                {
                    if (!Program.datas[scannID].stor.datein[key][value].Contains(searchPath))
                    {
                        Program.datas[scannID].stor.datein[key].Remove(value);
                    }
                }
                if (Program.datas[scannID].stor.datein[key].Count < 1) Program.datas[scannID].stor.datein.Remove(key);
            }
            ReCount(scannID);
            Console.WriteLine("Serch in scann: " + scannID + " finished");
        }

        public void SearchExtension(String searchExtension, int scannID)
        {
            Console.WriteLine("Search after File Extension: " + searchExtension + " scann: " + scannID);
            foreach (string key in Program.datas[scannID].stor.datein.Keys)
            {
                foreach (int value in Program.datas[scannID].stor.datein[key].Keys)
                {
                    if (!new FileInfo(Program.datas[scannID].stor.datein[key][value]).Extension.Equals(searchExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        Program.datas[scannID].stor.datein[key].Remove(value);
                    }
                }
                if (Program.datas[scannID].stor.datein[key].Count < 1) Program.datas[scannID].stor.datein.Remove(key);
            }
            ReCount(scannID);
            Console.WriteLine("Serch in scann: " + scannID + " finished");
        }

        public void SearchMinSize(Int64 searchMinSize, int scannID)
        {
            Console.WriteLine("Search after File Bigger then: " + searchMinSize + " bytes scann: " + scannID);
            foreach (string key in Program.datas[scannID].stor.datein.Keys)
            {
                foreach (int value in Program.datas[scannID].stor.datein[key].Keys)
                {
                    if (!(new FileInfo(Program.datas[scannID].stor.datein[key][value]).Length >= searchMinSize))
                    {
                        Program.datas[scannID].stor.datein[key].Remove(value);
                    }
                }
                if (Program.datas[scannID].stor.datein[key].Count < 1) Program.datas[scannID].stor.datein.Remove(key);
            }
            ReCount(scannID);
            Console.WriteLine("Serch in scann: " + scannID + " finished");
        }

        public void SearchMaxSize(Int64 searchMaxSize, int scannID)
        {
            Console.WriteLine("Search after File Smaler then: " + searchMaxSize + " bytes scann: " + scannID);
            foreach (string key in Program.datas[scannID].stor.datein.Keys)
            {
                foreach (int value in Program.datas[scannID].stor.datein[key].Keys)
                {
                    if (!(new FileInfo(Program.datas[scannID].stor.datein[key][value]).Length <= searchMaxSize))
                    {
                        Program.datas[scannID].stor.datein[key].Remove(value);
                    }
                }
                if (Program.datas[scannID].stor.datein[key].Count < 1) Program.datas[scannID].stor.datein.Remove(key);
            }
            ReCount(scannID);
            Console.WriteLine("Serch in scann: " + scannID + " finished");
        }

        public void ReCount(int scannID)
        {
            Program.datas[scannID].ResetStat("FileCount");
            Program.datas[scannID].ResetStat("FileError");
            Program.datas[scannID].ResetStat("FileSize");
            foreach(String key in Program.datas[scannID].stor.datein.Keys)
            {
                foreach(int key2 in Program.datas[scannID].stor.datein[key].Keys)
                {
                    try
                    {
                        Program.datas[scannID].AddStat("FileCount", 1);
                        Program.datas[scannID].AddStat("FileSize", (int)new FileInfo(Program.datas[scannID].stor.datein[key][key2]).Length);
                    }
                    catch (Exception)
                    {
                        Program.datas[scannID].AddStat("FileError", 1);
                    }
                }
            }
        }
    }
}
