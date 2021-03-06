using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Behavior : MonoBehaviour
{
    public float smellRadius;
    public PlantManager plantManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    List<Vector2> Smell()
    {
        List<Vector2> smelledPlants = new List<Vector2>();
        foreach(GameObject plant in plantManager.plants)
        {
            Vector2 plantLocation = plant.transform.position;
            if(Vector2.Distance(plantLocation, transform.position) <= smellRadius)
            {
                smelledPlants.Add(plantLocation - (Vector2) transform.position);
            }
        }
        return smelledPlants;
    }
}
