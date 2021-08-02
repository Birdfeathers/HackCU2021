using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonBehavior : MonoBehaviour
{
    public GameObject buildScreen;
    public GameObject pauseScreen;

    public  void Open()
    {
        buildScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

}
