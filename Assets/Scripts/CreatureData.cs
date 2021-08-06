using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureData
{
    public int birthtime;
    public int deathtime;
    public int generation;
    public float speed;
    public float smellRadius;
    public float thriftiness;
    public float angleChange;
    public float lifetime;
    public int strat;
    /* Arrangement of Creature Data:
    1. birthtime
    2. generation
    3. speed
    4. smellRadius
    5. thriftiness
    6. angleChange
    7. deathtime
    */

    public CreatureData(int B, int G, float Sp, float Sm, float T, float A, int Str)
    {
        birthtime = B;
        generation = G;
        speed = Sp;
        smellRadius = Sm;
        thriftiness = T;
        angleChange = A;
        deathtime = -1;
        lifetime = -1;
        strat = Str;
    }
}
