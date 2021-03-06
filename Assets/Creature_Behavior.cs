using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Behavior : MonoBehaviour
{
    public float smellRadius;
    public float speed;
    public PlantManager plantManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        SniffFood();
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
    void Eat()
    {
        return;
    }

    void SniffFood()
    {
        List<Vector2> plants = Smell();
        if(plants.Count == 0) return;
        Vector2 closest = plants[0];
        foreach(Vector2 plant in plants)
        {
            if(plant.magnitude < closest.magnitude)
            {
                closest = plant;
            }
        }
        if(closest.magnitude > 0)
        {
            transform.position += speed * (Vector3) closest.normalized;
        }
        else{
            Eat();
        }

    }
}
