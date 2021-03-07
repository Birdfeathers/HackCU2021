using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public List<GameObject> plants;
    public GameObject plantPrefab;


    private float eatingRadius;

    // Start is called before the first frame update
    void Start()
    {
        eatingRadius = .5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool DeleteClosest(Vector2 location)
    {
        for(int i=0; i < plants.Count; i++)
        {
            Vector2 plantLocation = plants[i].transform.position;
            if (Vector2.Distance(plantLocation, location) <= eatingRadius)
            {
                GameObject plant = plants[i];
                plants.RemoveAt(i);
                Destroy(plant);
                return true;
            }
        }
        return false;
    }

    public void NewPlantAt(Vector2 location)
    {
        plants.Add(Instantiate(plantPrefab, location, Quaternion.identity, transform));
    }
}
