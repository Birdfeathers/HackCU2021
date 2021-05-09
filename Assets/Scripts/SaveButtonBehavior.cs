using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonBehavior : MonoBehaviour
{

    public GameObject panel;


    public void Press()
    {
        panel.SetActive(true);

    }
}
