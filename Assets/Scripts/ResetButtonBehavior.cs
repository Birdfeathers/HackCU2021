using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonBehavior : MonoBehaviour
{
    public SpeedSlider slider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetSpeed()
    {
        Time.timeScale = 1;
        slider.speedbar.value = 1;
    }
}
