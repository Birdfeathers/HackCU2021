using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    bool addFirst;
    bool addSecond;
    public GameObject barrier;
    public GameObject dotPrefab;

    GameObject dot1;
    GameObject dot2;

    Vector3 pos1;
    Vector3 pos2;

    void Start()
    {
        addFirst = false;
        addSecond = false;
    }

    void CreateBarrier()
    {
        Vector2 relPos = pos1 - pos2;
        Vector2 mid = (pos1 + pos2) / 2;

        float dist = Vector3.Distance(pos1, pos2);


        GameObject barr = Instantiate(barrier, mid, Quaternion.identity);
        // correct angle
        Vector2 normalizedDiff = relPos.normalized;
        float dir = Mathf.Acos(normalizedDiff.x) * Mathf.Sign(normalizedDiff.y);
        barr.transform.localEulerAngles = new Vector3(0,0,dir*180/Mathf.PI);
        // correct size
        barr.transform.localScale = new Vector3(dist, 0.5f, 1);
        barr.GetComponent<BoxCollider2D>().size = new Vector2(dist, 0.5f);

        Destroy(dot1);
        Destroy(dot2);

    }

    // Update is called once per frame
    void Update()
    {
        if(addFirst || addSecond)
        {
            if(Input.GetButtonDown("LeftClick"))
            {
                Vector3 mousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                GameObject dot = Instantiate(dotPrefab, mousePos, Quaternion.identity, transform);
                if(addSecond)
                {
                    addSecond = false;
                    pos2 = mousePos;
                    dot1 = dot;
                    CreateBarrier();

                }
                else
                {
                    addFirst = false;
                    addSecond = true;
                    pos1 = mousePos;
                    dot2 = dot;
                }

            }



        }
    }

    public void Add()
    {
        addFirst = true;
    }
}
