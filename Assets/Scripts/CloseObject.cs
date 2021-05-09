using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseObject : MonoBehaviour
{
    public GameObject o;
    public void Close()
    {
        o.SetActive(false);
    }
}
