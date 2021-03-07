using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        paused = true;
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused) { Unpause(); }
            else { Pause(); }
            paused = !paused;
        }
    }

    private void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
