using System;
using System.Collections.Concurrent;

namespace AdvancedDataScanner
{
    //Main Data Structure
    public struct Data
    {
        public ConcurrentDictionary<String, ConcurrentDictionary<int, String>> datein;

        public ConcurrentDictionary<String, Int64> stats;

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
                stor.datein.TryAdd(fileType, new ConcurrentDictionary<int, string>());

            stor.datein[fileType].TryAdd(stor.datein[fileType].Count + 1, path);
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
            stor.datein = new ConcurrentDictionary<String, ConcurrentDictionary<int, String>>();
            stor.stats = new ConcurrentDictionary<String, Int64>();
            stor.stats.TryAdd("FolderCount", 0);
            stor.stats.TryAdd("FileCount", 0);
            stor.stats.TryAdd("FileError", 0);
            stor.stats.TryAdd("FolderError", 0);
            stor.stats.TryAdd("FileSize", 0);
            stor.stats.TryAdd("CleanFolders", 0);
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
