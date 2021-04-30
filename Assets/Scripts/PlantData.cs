using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData
{
    public int generation;
    public float growthRate;
    public int birthtime;
    public int deathtime;
    public float lifetime;
    public PlantData(int Ge, float Gr, int B)
    {
        birthtime = B;
        generation = Ge;
        growthRate = Gr;
        deathtime = -1;
        lifetime = -1;
    }
}
