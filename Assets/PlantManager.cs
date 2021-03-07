using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public List<GameObject> plants;
    public GameObject plantPrefab;
    public int MaxPlants;


    private float eatingRadius;

    // Start is called before the first frame update
    void Start()
    {
        eatingRadius = .5f;
        for(int i = 0; i < 25; i++)
        {
            NewPlantAt(Random.insideUnitCircle* 6);
        }
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
        if(plants.Count  > MaxPlants) return;
        foreach(GameObject plant in plants)
        {
            if (Vector2.Distance(plant.transform.position, location) < 1) { return; }
        }
        plants.Add(Instantiate(plantPrefab, location, Quaternion.identity, transform));
    }
}
