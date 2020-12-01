using System;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Xml.Serialization;

[Serializable]
public class LevelSave
{
    public const string filename = "TestSave.xml";
    public string SaveStamp;
    public Level[] levels;

   
    public static void SavePlayers(Level level)
    {
        var saves = FileManager.OpenSaves<LevelSave>(LevelSave.filename) as LevelSave;
        
        if(saves == null)
        {
            saves = new LevelSave();
            saves.levels = new Level[0];
        }
        saves.SaveStamp = DateTime.Now.ToShortTimeString();
        var savedLevel = saves.levels.FirstOrDefault(x => x.maxLevelSize.Equals(level.maxLevelSize)); 

        if(savedLevel != null)
        {
            //savedLevel.UpdateData(level);
        }
        else
        {
            var savedLevels = saves.levels.ToList();
            savedLevel = new Level();
            //savedLevel.UpdateData(level);
            //savedLevel.Add(savedLevel);
            //saves.levels = savedLevel.ToArray();
        }
        FileManager.SaveFile(saves, filename);

    }




}