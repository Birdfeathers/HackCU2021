using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    public Slider speedbar;
    public float speed;
    public bool okay;
    // Start is called before the first frame update
    void Start()
    {
        speedbar = GetComponent<Slider>();
        speedbar.value = 1;
        speed = 1;
        okay = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValueChanged()
    {
        speed = speedbar.value;
        Time.timeScale = speed;
    }

    public void DeactivateMove()
    {
        okay = false;
    }

    public void ActivateMove()
    {
        okay = true;
    }
}
