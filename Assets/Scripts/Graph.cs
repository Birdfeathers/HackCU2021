using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    public GameObject graphBackground;
    public GameObject graph;
    public GameObject rectangle;
    public float lineWeight;

    public List<Vector2> defaultPairs;
    private List<GameObject> lines;
    private Vector2 resolution;
    private bool opened;

    public int MAXPOINTS = 500;
    public ButtonManagerBehavior bmb;
    public CreatureManager cm;


    void Start()
    {
        lines = new List<GameObject>();
        resolution = NewResolution();
        opened = false;
    }

    private void Update()
    {
        if (opened && resolution != NewResolution())
        {
            DestroyLines();
            DrawGraphs();
            resolution = NewResolution();
        }
    }

    private void DrawGraphs()
    {
        for(int i = 0; i < bmb.buttons.Count; i++)
        {
            if(bmb.buttons[i].on)
            {
            DrawLines(FindPairs(bmb.buttons[i]), bmb.buttons[i].color);

            }
        }
    }

    private List<Vector2> CreateVectorList(List<float> x, List<float> y)
    {
        List<Vector2> newList = new List<Vector2>();
        if(x.Count == y.Count)
        {
            // spacing is the number of divisions needed
            int spacing;
            if(x.Count <= MAXPOINTS)
            {
                spacing = 1;
            }
            else
            {
                spacing = x.Count / MAXPOINTS;
            }

            for(int i = 0; i < x.Count; i+= spacing)
            {
                Vector2 v = new Vector2(x[i], y[i]);
                newList.Add(v);
            }
        }
        else{
            print("error");
        }
        return newList;
    }

    private List<Vector2> FindPairs(ButtonBehavior2 button)
    {
        if(button.type == "smell")
        {
            return(CreateVectorList(cm.times, cm.smells));
        }
        else if(button.type == "full")
        {
            return(CreateVectorList(cm.times, cm.fulls));
        }
        else if(button.type == "speed")
        {
            return(CreateVectorList(cm.times, cm.speeds));
        }
        else if(button.type == "total")
        {
            return(CreateVectorList(cm.times, cm.totals));
        }
        return(CreateVectorList(cm.times, cm.totals));
    }

    public void OpenGraph()
    {
        //if (opened) { return; }
        graphBackground.SetActive(true);
        DestroyLines();
        DrawGraphs();
        opened = true;
    }

    private void DrawLines(List<Vector2> pairs, Color color)
    {
        if (pairs.Count > 1)
        {
            Vector2 max = pairs[0];
            for (int i = 1; i < pairs.Count; i++)
            {
                if (pairs[i].x > max.x) { max.x = pairs[i].x; }
                if (pairs[i].y > max.y) { max.y = pairs[i].y; }
            }
            Vector2 positionA = pairs[0] / max;
            for (int i = 1; i < pairs.Count; i++)
            {
                Vector2 positionB = pairs[i] / max;
                CreateLine(positionA, positionB, color);
                positionA = positionB;
            }
        }
    }

    public void CloseGraph()
    {
        if (!opened) { return; }
        graphBackground.SetActive(false);
        DestroyLines();
        opened = false;
    }

    private void DestroyLines()
    {
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
        lines = new List<GameObject>();
    }

    public void Reset()
    {
        SceneManager.LoadSceneAsync(0);
    }

    //used the code from Code Monkey https://www.youtube.com/watch?v=CmU5-v-v1Qo as a basis
    //takes in 2 positions and draws a line between them.
    private void CreateLine(Vector2 positionA, Vector2 positionB, Color color)
    {
        //set size based on the current size of the background
        Vector2 size = graph.GetComponent<RectTransform>().rect.size;
        //make a new line that belongs to the background
        GameObject line = Instantiate(rectangle);
        line.transform.SetParent(graph.transform, false);
        //move the line to positionA and make it rotate about that point
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchoredPosition = positionA * size;
        //make the line the length of the distance between positions a and b,
        //scaled to the size of the graph window.
        float distance = Vector2.Distance(positionA * size, positionB * size);
        rectTransform.sizeDelta = new Vector2(distance, lineWeight * Mathf.Min(size.x, size.y));
        //finally, get the angle of the line, and rotate it accordingly
        Vector2 normalizedDiff = (size * (positionB - positionA)).normalized;
        float dir = Mathf.Acos(normalizedDiff.x) * Mathf.Sign(normalizedDiff.y);
        rectTransform.localEulerAngles = new Vector3(0,0,dir*180/Mathf.PI);
        // Set the color
        Image image = line.GetComponent<Image>();
        image.color = color;
        lines.Add(line);
    }

    private Vector2 NewResolution() => new Vector2(Screen.width, Screen.height);
}
