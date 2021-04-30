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
    public PlantManager pm;
    public Dropdown ddx;
    public Dropdown dda;
    public Dropdown ddp;


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
        if(ddx.value == 0)
        {
            for(int i = 0; i < bmb.buttons.Count; i++)
            {
                if(bmb.buttons[i].on)
                {
                DrawLines(FindPairs(bmb.buttons[i]), bmb.buttons[i].color);

                }
            }
        }
        else if(ddx.value == 1)
        {
            for(int i = 0; i < bmb.buttons.Count; i++)
            {
                if(bmb.buttons[i].on)
                {
                    int x = dda.value;
                    int y = bmb.buttons[i].number;
                    bool livingIncluded = true;
                    if(x > 5 || y > 5) livingIncluded = false;
                    DrawLines(ListSpacing(CreateListFromData(x,y, livingIncluded)),bmb.buttons[i].color);

                }
            }
        }
        else
        {
            for(int i = 0; i < bmb.buttons.Count; i++)
            {
                if(bmb.buttons[i].on)
                {
                    int x = ddp.value;
                    int y = bmb.buttons[i].number;
                    bool livingIncluded = true;
                    if(x > 2 || y > 2) livingIncluded = false;
                    DrawLines(ListSpacing(CreateListFromPlantData(x,y, livingIncluded)),bmb.buttons[i].color);

                }
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

    private int LookupIndex(List<Vector2> list, float value)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].x == value)
            {
                return i;
            }
        }
        return -1;
    }

    private float returnVariable(int code, int index)
    {
        switch(code)
        {
            case 0:
                return cm.data[index].generation;
            case 1:
                return cm.data[index].speed;
            case 2:
                return cm.data[index].smellRadius;
            case 3:
                return cm.data[index].full;
            case 4:
                return cm.data[index].angleChange;
            case 5:
                return cm.data[index].birthtime;
            case 6:
                return cm.data[index].deathtime;
            case 7:
                return cm.data[index].lifetime;

        }
        return -1;
    }
    private float returnPlantVariable(int code, int index)
    {
        switch(code)
        {
            case 0:
                return pm.data[index].generation;
            case 1:
                return pm.data[index].growthRate;
            case 2:
                return pm.data[index].birthtime;
            case 3:
                return pm.data[index].deathtime;
            case 4:
                return pm.data[index].lifetime;

        }
        return -1;
    }

    private float AdjustAverage(float currentAverage, float total, float number)
    {
        return ((currentAverage * (total - 1)) + number) / total;
    }

    private List<Vector2> ListSpacing(List<Vector2> list)
    {
        List<Vector2> newList = new List<Vector2>();
        int spacing;
        if(list.Count <= MAXPOINTS)
        {
            return list;
        }
        else
        {
            spacing = list.Count / MAXPOINTS;
        }

        for(int i = 0; i < list.Count; i+= spacing)
        {
            Vector2 v = new Vector2(list[i].x, list[i].y);
            newList.Add(v);
        }
        return newList;
    }

    private List<Vector2> CreateListFromData(int x, int y, bool livingIncluded)
    {
        /* Arrangement of Creature Data:
        6. birthtime
        1. generation
        2. speed
        3. smellRadius
        4. full
        5. angleChange
        7. deathtime
        8. lifetime
        */
        List<Vector2> newList= new List<Vector2>();
        List<Vector2> totalAtX = new List<Vector2>();
        foreach(KeyValuePair<int , CreatureData> creature in cm.data)
        {
            if(livingIncluded || creature.Value.deathtime != -1)
            {
                float xVal = returnVariable(x, creature.Key);
                float yVal = returnVariable(y, creature.Key);
                int index = LookupIndex(newList, xVal);
                if(index == -1)
                {
                    Vector2 v = new Vector2(xVal, yVal);
                    newList.Add(v);
                    Vector2 total = new Vector2(xVal, 1);
                    totalAtX.Add(total);
                }
                else
                {
                    float Ty =  totalAtX[index].y + 1;
                    float Ny = AdjustAverage(newList[index].y, totalAtX[index].y, yVal);
                    Vector2 v = new Vector2(xVal,Ny);
                    Vector2 total = new Vector2(xVal, Ty);
                    newList[index] = v;
                    totalAtX[index] = total;

                }
            }

        }
        newList = SortByX(newList);
        // for(int i = 0; i < newList.Count; i++)
        // {
        //     print($"x: {newList[i].x}; y: {newList[i].y}");
        // }
        return newList;
    }

        private List<Vector2> CreateListFromPlantData(int x, int y, bool livingIncluded)
        {
            List <Vector2> newList= new List<Vector2>();
            List<Vector2> totalAtX = new List<Vector2>();
            foreach(KeyValuePair<int , PlantData> plant in pm.data)
            {
                if(livingIncluded || plant.Value.deathtime != -1)
                {
                    float xVal = returnPlantVariable(x, plant.Key);
                    float yVal = returnPlantVariable(y, plant.Key);
                    int index = LookupIndex(newList, xVal);
                    if(index == -1)
                    {
                        Vector2 v = new Vector2(xVal, yVal);
                        newList.Add(v);
                        Vector2 total = new Vector2(xVal, 1);
                        totalAtX.Add(total);
                    }
                    else
                    {
                        float Ty =  totalAtX[index].y + 1;
                        float Ny = AdjustAverage(newList[index].y, totalAtX[index].y, yVal);
                        Vector2 v = new Vector2(xVal,Ny);
                        Vector2 total = new Vector2(xVal, Ty);
                        newList[index] = v;
                        totalAtX[index] = total;

                    }
                }

            }
        newList = SortByX(newList);
        // for(int i = 0; i < newList.Count; i++)
        // {
        //     print($"x: {newList[i].x}; y: {newList[i].y}");
        // }
        return newList;
    }

    private List<Vector2> SortByX(List<Vector2> list)
    {
        List<Vector2> newList = new List<Vector2>();
        newList.Add(list[0]);
        for(int i = 1; i < list.Count; i++)
        {
            int place = 0;
            while(place < newList.Count && list[i].x > newList[place].x) place++;
            newList.Insert(place, list[i]);

        }
        // for(int i = 0; i < newList.Count; i++)
        // {
        //     print($" Sorted x: {newList[i].x}; y: {newList[i].y}");
        // }
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
        else if(button.type == "wander")
        {
            return(CreateVectorList(cm.times, cm.wanders));
        }
        else if(button.type == "plantPop")
        {
            return(CreateVectorList(cm.times, pm.totals));
        }
        else if(button.type == "plantGrowthTime")
        {
            return(CreateVectorList(cm.times, pm.growthTimes));
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

    public void DestroyLines()
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
