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

    public List<float> totals;
    public List<float> growthTimes;

    private float eatingRadius;
    private int currentID;
    public Dictionary<int, PlantData> data = new Dictionary<int, PlantData>();

    int time;

    // Start is called before the first frame update
    void Start()
    {
        currentID = 0;
        time = 0;
        eatingRadius = .5f;
        for (int i = 0; i < 25; i++)
        {
            if (NewPlantAt(Random.insideUnitCircle * 6))
            {
                PlantBehavior plant = plants[i].GetComponent<PlantBehavior>();
                plant.timeTillGrowth = plant.growthTime - Random.Range(0, plant.growthTime); //random growth left for organicness

                plant.generation = 0;
                plant.id = currentID;
                float growthRate = 1f / plant.growthTime;
                PlantData pd = new PlantData(plant.generation, growthRate, time);
                data.Add(currentID, pd);
                currentID++;
            }
            else { i--; }
        }
    }

    void FixedUpdate()
    {
        time++;
        UpdateVariables();
    }

    void UpdateVariables()
    {
        float totalgrowthTime = 0;
        for(int i = 0; i < plants.Count; i++)
        {
            PlantBehavior plant =  plants[i].GetComponent<PlantBehavior>();
            totalgrowthTime += plant.growthTime;
        }
        float aveGrowthTime = totalgrowthTime/ plants.Count;
        totals.Add(plants.Count);
        growthTimes.Add(1 / aveGrowthTime);
    }

    // public bool DeleteClosest(Vector2 location)
    // {
    //     for (int i = 0; i < plants.Count; i++)
    //     {
    //         Vector2 plantLocation = plants[i].transform.position;
    //         if (Vector2.Distance(plantLocation, location) <= eatingRadius)
    //         {
    //             GameObject plant = plants[i];
    //
    //             PlantBehavior P = plant.GetComponent<PlantBehavior>();
    //             data[P.id].deathtime = time;
    //             data[P.id].lifetime = time - data[P.id].birthtime;
    //
    //             plants.RemoveAt(i);
    //             Destroy(plant);
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    public bool DeletePlant(GameObject plant, Vector2 location)
    {
        Vector2 plantLocation = plant.transform.position;
        if (Vector2.Distance(plantLocation, location) <= eatingRadius)
        {
            PlantBehavior P = plant.GetComponent<PlantBehavior>();
            data[P.id].deathtime = time;
            data[P.id].lifetime = time - data[P.id].birthtime;

            plants.Remove(plant);
            Destroy(plant);
            return true;
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
        clone.growthTime += Random.Range(-Mathf.RoundToInt(plantfactor * clone.growthTime), Mathf.RoundToInt(plantfactor * clone.growthTime));

        clone.generation = clone.generation + 1;
        clone.id = currentID;
        float growthRate = 1f / clone.growthTime;
        PlantData pd = new PlantData(clone.generation, growthRate, time);
        data.Add(currentID, pd);
        currentID++;

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
