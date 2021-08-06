using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LogConverter
{
    private StreamWriter timeData;
    private StreamWriter creatureData;
    private StreamWriter saveList;
    private StreamWriter plantData;
    private CreatureManager cm;
    private PlantManager pm;

    public LogConverter(string name, CreatureManager creatureManager, PlantManager plantManager)
    {
        string savedName = "SavedFiles.txt";
        string timeName = @"Saved\"  + name + "TimeData.csv";
        string creatureName = @"Saved\"  + name + "CreatureData.csv";
        string plantName = @"Saved\"  + name + "PlantData.csv";
        File.Create(timeName).Dispose();
        timeData = File.AppendText(timeName);
        cm = creatureManager;
        pm = plantManager;
        using (saveList = File.AppendText(savedName))
        {
            saveList.Write($"{name}\n");
        }
        File.Create(creatureName).Dispose();
        creatureData = File.AppendText(creatureName);
        File.Create(plantName).Dispose();
        plantData = File.AppendText(plantName);
    }

    public void WriteTime()
    {
        timeData.Write("Time,Population,Average Speed,Average Thriftiness, Average Smell Distance, Angle Change \n");
        for(int i = 0; i < cm.times.Count; i++)
        {
            timeData.Write($"{cm.times[i]},{cm.totals[i]},{cm.speeds[i]},{cm.thrifties[i]},{cm.smells[i]}, {cm.wanders[i]}\n");
        }


    }

    public void WriteCreature()
    {
        creatureData.Write("Creature ID,Birth Time,Death Time,Life Time,Generation,Speed,Smell Radius,Thriftiness,Angle Change\n");
        foreach(KeyValuePair<int , CreatureData> creature in cm.data)
        {
            int index = creature.Key;
            creatureData.Write($"{index},{cm.data[index].birthtime},{cm.data[index].deathtime},{cm.data[index].lifetime},{cm.data[index].generation},{cm.data[index].speed},{cm.data[index].smellRadius},{cm.data[index].thriftiness},{cm.data[index].angleChange}\n");
        }

    }
    public void WritePlant()
    {
        plantData.Write("Plant ID,Birth Time,Death Time,Life Time,Generation,Growth Rate\n");
        foreach(KeyValuePair<int , PlantData> plant in pm.data)
        {
            int index = plant.Key;
            plantData.Write($"{index},{pm.data[index].birthtime},{pm.data[index].deathtime},{pm.data[index].lifetime},{pm.data[index].generation},{pm.data[index].growthRate}\n");
        }

    }
    public void Close()
    {
        saveList.Close();
        timeData.Close();
        creatureData.Close();
        plantData.Close();

    }
}
