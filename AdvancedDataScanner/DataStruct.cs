using System;
using System.Collections.Generic;

namespace AdvancedDataScanner
{
    //Main Data Structure
    public struct Data
    {
        public Dictionary<String, Dictionary<int, String>> datein;

        public Dictionary<String, Int64> stats;

        public DateTime start;
        public DateTime end;
    }

    //tools for the scanner to store data
    public class DataStruct
    {
        public Data stor = new Data();

        //add specifik string to the given datatype
        public void AddSingle (String fileType, String path)
        {
            if (!stor.datein.ContainsKey(fileType))
                stor.datein.Add(fileType, new Dictionary<int, string>());

            stor.datein[fileType].Add(stor.datein[fileType].Count + 1, path);
        }

        //increse the given stat by the given number
        public void AddStat (String stat, Int64 val)
        {
            stor.stats[stat] += val;
        }

        public void ResetStat (String stat)
        {
            if (stor.stats.ContainsKey(stat))
            {
                stor.stats[stat] = 0;
            }
        }
        //initalize data structure to be ready
        public void InitStruct ()
        {
            stor.datein = new Dictionary<String, Dictionary<int, String>>();
            stor.stats = new Dictionary<String, Int64>
            {
                { "FolderCount", 0 },
                { "FileCount", 0 },
                { "FileError", 0 },
                { "FolderError", 0 },
                { "FileSize", 0 },
                { "CleanFolders", 0 }
            };
        }
    }

    public class Names
    {
        public String Name;
        public String Type;
        public String StartPath;
        public String Status;
    }
}
