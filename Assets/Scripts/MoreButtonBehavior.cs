using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreButtonBehavior : MonoBehaviour
{
    public List<GameObject> p;
    public int current;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        SetInactive();
        p[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetInactive()
    {
        for(int i = 0; i < p.Count; i++)
        {
            p[i].SetActive(false);
        }
    }

    public void ChangePanel()
    {
        p[current].SetActive(false);
        if(current < p.Count - 1) current++;
        else current = 0;
        p[current].SetActive(true);
    }
}
