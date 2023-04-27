using System;
using System.IO;
using System.Collections.Generic;

namespace Breakout;

public class FileReader {
    public List<string> map {get; private set;}
    public Dictionary<string, string> meta {get; private set;}
    public Dictionary<char, string> legend {get; private set;}

    public FileReader() {
        map = new List<string>{};
        meta = new Dictionary<string, string>();
        legend = new Dictionary<char, string>();
    }

    public bool Read (string name) {
        //Empty excisting data in fields, to prepare for new data
        map.Clear();
        meta.Clear();
        legend.Clear();

        //Convert level ASCII txt file in to a string array
        string[] AllLines = File.ReadAllLines(Path.Combine("Assets", "Levels", name));

        //Convert the map into a list of strings for each line in map, and add it to the map field
        int mapStartIndex = Array.IndexOf(AllLines, "Map:")+1;
        int mapEndIndex = Array.IndexOf(AllLines, "Map/")-1;
        //Check if the ASCII map has the correct width of 12 characters
        bool wrongLevelWidth = false;
        for (int i = mapStartIndex; i <= mapEndIndex; i++) {
            if (AllLines[i].Length != 12) {
                wrongLevelWidth = true;
            }
        }
        //Map error handling
        if (mapStartIndex == 0 || mapEndIndex == -2 || mapEndIndex - mapStartIndex != 24 || wrongLevelWidth) {
            Console.WriteLine("Invalid ASCII file, does not follow map standart");
            return false;
        }
        //Adding map lines to map field
        for (int i = mapStartIndex; i <= mapEndIndex; i++) {
            map.Add(AllLines[i]);
        }
        
        //Convert the meta data into a dictionary and add it to the meta field
        int metaStartIndex = Array.IndexOf(AllLines, "Meta:")+1;
        int metaEndIndex = Array.IndexOf(AllLines, "Meta/")-1;
        //Meta Error handling
        if (metaStartIndex == 0 || metaEndIndex == -2) {
            Console.WriteLine("Invalid ASCII file, does not follow meta standart");
            return false;
        }
        //Adding meta data to dictionary field
        for (int i = metaStartIndex; i <= metaEndIndex; i++) {
            string[] splitter = AllLines[i].Split(": ", StringSplitOptions.None);
            meta.Add(splitter[0], splitter[1]);
        }

        //Convert the legend data into a dictionary and add it to the legend field
        int legendStartIndex = Array.IndexOf(AllLines, "Legend:")+1;
        int legendEndIndex = Array.IndexOf(AllLines, "Legend/")-1;
        //Legend error handling
        if (legendStartIndex == 0 || legendEndIndex == -2) {
            Console.WriteLine("Invalid ASCII file, does not follow legend standart");
            return false;
        }
        //Adding legend data to dictionary field
        for (int i = legendStartIndex; i <= legendEndIndex; i++) {
            string[] splitter = AllLines[i].Split(") ", StringSplitOptions.None);
            legend.Add((splitter[0].ToCharArray())[0], splitter[1]);
        }

        return true;
    }
}