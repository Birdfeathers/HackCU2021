using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    public Graph graph;
    public GameObject slider;

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
        //force graph to close when unpausing
        graph.CloseGraph();
        SpeedSlider values = slider.GetComponent<SpeedSlider>();
        Time.timeScale = values.speed;
        values.speedbar.value = values.speed;
        slider.SetActive(true);
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        slider.SetActive(false);
    }
}
