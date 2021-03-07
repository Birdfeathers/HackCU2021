using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LogConverter
{
    private StreamWriter data; // converted data
    private StreamReader log;
    void Write()
    {
        log = new StreamReader("log.txt");
        File.Create("data.txt").Dispose();
        data = File.AppendText("data.txt");

        int total = 1;
        float totalSpeed = 0.01f;
        float totalFull = 10f;
        float totalSmellRange = 3f;
        int time = 0;


        data.Write($"Time 0; Pop 1; AveSpeed .01; AveFull 10; AveDist 3\n");
        string line;
        line = log.ReadLine();
        while( line.Length > 0)
        {

            string[] strings = line.Split(new string[]{ "; " },  StringSplitOptions.None);
            string[] times = strings[4].Split(new string[]{ " " },  StringSplitOptions.None);
            time = int.Parse(times[1]);

            string[] speeds = strings[1].Split(new string[]{ " " },  StringSplitOptions.None);
            string[] fulls = strings[2].Split(new string[]{ " " },  StringSplitOptions.None);
            string[] smells = strings[3].Split(new string[]{ " " },  StringSplitOptions.None);

            float speed = float.Parse(speeds[1]);
            float full = float.Parse(fulls[1]);
            float smell = float.Parse(smells[1]);

            if(strings[0] == "Add")
            {
                total++;
                totalSpeed+= speed;
                totalFull+= full;
                totalSmellRange += smell;

            }
            else
            {
                total--;
                totalSpeed-= speed;
                totalFull -= full;
                totalSmellRange-= smell;
            }


            float aveSpeed = totalSpeed / total;
            float aveFull = totalFull / total;
            float aveSmell = totalSmellRange / total;

            data.Write($"Time {time}; Pop {total}; AveSpeed {aveSpeed}; AveFull {aveFull}; AveDist {aveSmell}\n");


            line = log.ReadLine();

        }



    }
}
