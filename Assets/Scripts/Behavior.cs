using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Behavior
{
    Func<List<Vector2>> sense;
    Action<List<Vector2>> motor;
    Action decide;
}
