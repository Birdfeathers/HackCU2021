using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public List<GameObject> plants;
    public GameObject plantPrefab;
    public int MaxPlants;
    public float breathingRoom;
    public float plantfactor = .5f;


    private float eatingRadius;

    // Start is called before the first frame update
    void Start()
    {
        eatingRadius = .5f;
        for(int i = 0; i < 25; i++)
        {
            if (NewPlantAt(Random.insideUnitCircle * 6 ))
            {
                PlantBehavior plant = plants[i].GetComponent<PlantBehavior>();
                plant.timeTillGrowth = plant.growthTime - Random.Range(0, plant.growthTime);//random growth left for organicness
            }
            else { i--; }
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

    public bool NewPlantAt(Vector2 location, GameObject p)
    {
        if (plants.Count > MaxPlants) { return false; }
        foreach(GameObject plant in plants)
        {
            if (Vector2.Distance(plant.transform.position, location) < breathingRoom) { return false; }
        }
        //PlantBehavior clone = Instantiate(plantPrefab, location, Quaternion.identity, transform).GetComponent<Behavior>();
        //plants.Add(Instantiate(plantPrefab, location, Quaternion.identity, transform));

        PlantBehavior clone = Instantiate(p, transform).GetComponent<PlantBehavior>();
        clone.transform.position = location;
        clone.growthTime  += Random.Range(- Mathf.RoundToInt(plantfactor* clone.growthTime) , Mathf.RoundToInt(plantfactor * clone.growthTime));
        plants.Add(clone.gameObject);
        return true;
    }
    public bool NewPlantAt(Vector2 location)
    {
        if (plants.Count > MaxPlants) { return false; }
        foreach(GameObject plant in plants)
        {
            if (Vector2.Distance(plant.transform.position, location) < breathingRoom) { return false; }
        }
        plants.Add(Instantiate(plantPrefab, location, Quaternion.identity, transform));
        return true;
    }
}
