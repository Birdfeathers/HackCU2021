using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    public GameObject graphBackground;
    public GameObject rectangle;
    public float lineWeight;

    public List<Vector2> pairs;
    private List<GameObject> lines;
    private Vector2 resolution;
    private bool opened;


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
            CloseGraph();
            OpenGraph();
            resolution = NewResolution();
        }
    }

    public void OpenGraph()
    {
        if (opened) { return; }
        graphBackground.SetActive(true);
        if(pairs.Count > 1)
        {
            Vector2 positionA = pairs[0];
            for (int i = 1; i < pairs.Count; i++)
            {
                Vector2 positionB = pairs[i];
                CreateLine(positionA, positionB);
                positionA = positionB;
            }
        }
        opened = true;
    }

    public void CloseGraph()
    {
        if (!opened) { return; }
        graphBackground.SetActive(false);
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
        lines = new List<GameObject>();
        opened = false;
    }


    public void Reset()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void CreateLine(Vector2 positionA, Vector2 positionB)
    {
        Vector2 size = graphBackground.GetComponent<RectTransform>().rect.size;
        GameObject line = Instantiate(rectangle);
        line.transform.SetParent(graphBackground.transform, false);
        Vector2 normalizedDiff = (size * (positionB - positionA)).normalized;
        float dir = Mathf.Acos(normalizedDiff.x) * Mathf.Sign(normalizedDiff.y);
        float distance = Vector2.Distance(positionA*size, positionB*size);
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, lineWeight*Mathf.Min(size.x,size.y));
        rectTransform.anchoredPosition = positionA * size;
        rectTransform.localEulerAngles = new Vector3(0,0,dir*180/Mathf.PI);
        lines.Add(line);
    }

    private Vector2 NewResolution() => new Vector2(Screen.width, Screen.height);
}
