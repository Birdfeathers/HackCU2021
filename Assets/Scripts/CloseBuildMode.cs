using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBuildMode : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject buildScreen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(pauseScreen.activeSelf) pauseScreen.SetActive(false);
    }

    public void Close()
    {
        if(Time.timeScale == 0) pauseScreen.SetActive(true);
        buildScreen.SetActive(false);
    }
}
