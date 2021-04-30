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
    public float full;
    public float angleChange;
    public float lifetime;

    /* Arrangement of Creature Data:
    1. birthtime
    2. generation
    3. speed
    4. smellRadius
    5. full
    6. angleChange
    7. deathtime
    */

    public CreatureData(int B, int G, float Sp, float Sm, float F, float A)
    {
        birthtime = B;
        generation = G;
        speed = Sp;
        smellRadius = Sm;
        full = F;
        angleChange = A;
        deathtime = -1;
        lifetime = -1;
    }
}
