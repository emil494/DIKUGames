using System;
using System.IO;
using System.Collections.Generic;

namespace Breakout;

public class FileReader {
    public List<string> map {get; private set;}
    public Dictionary<string, string> meta {get; private set;}
    public Dictionary<string, string> legend {get; private set;}

    public FileReader() {
        map = new List<string>{};
        meta = new Dictionary<string, string>();
        legend = new Dictionary<string, string>();
    }

    public void Read (string name) {
        //Empty excisting data in fields, to prepare for new data
        map.Clear();
        meta.Clear();
        legend.Clear();

        //Convert level ASCII txt file in to a string array
        string[] AllLines = File.ReadAllLines(Path.Combine("Assets", "Levels", name));

        //Convert the map into a list of strings for each line in map, and add it to the map field.
        int mapStartIndex = Array.IndexOf(AllLines, "Map:")+1;
        int mapEndIndex = Array.IndexOf(AllLines, "Map/")-1;
        for (int i = mapStartIndex; i <= mapEndIndex; i++) {
            map.Add(AllLines[i]);
        }
        
        //Convert the meta data into a dictionary and add it to the meta field.
        int metaStartIndex = Array.IndexOf(AllLines, "Meta:")+1;
        int metaEndIndex = Array.IndexOf(AllLines, "Meta/")-1;
        for (int i = metaStartIndex; i <= metaEndIndex; i++) {
            string[] splitter = AllLines[i].Split(':');
            meta.Add(splitter[0], splitter[1]);
        }

        //Convert the legend data into a dictionary and add it to the legend field
        int legendStartIndex = Array.IndexOf(AllLines, "Legend:")+1;
        int legendEndIndex = Array.IndexOf(AllLines, "Legend/")-1;
        for (int i = legendStartIndex; i <= legendEndIndex; i++) {
            string[] splitter = AllLines[i].Split(") ", StringSplitOptions.None);
            legend.Add(splitter[0], splitter[1]);
        }
    }
}