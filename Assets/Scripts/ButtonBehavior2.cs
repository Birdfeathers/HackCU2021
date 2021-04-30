using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior2 : MonoBehaviour
{
    public bool on;
    public List<Vector2> pairs;
    public string type;
    public int number;
    public Graph graph;
    public Color color;
    //public LogConverter con;
    // Start is called before the first frame update
    void Start()
    {
        on = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Press()
    {
        if(on)
        {
            on = false;
            graph.OpenGraph();
        }
        else
        {
            on = true;
            graph.OpenGraph();

        }
    }
    public void Smells()
    {

    }


}
