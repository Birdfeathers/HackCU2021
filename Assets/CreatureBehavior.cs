using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreatureBehavior : MonoBehaviour
{
    public float smellRadius;
    public float food;
    public float speed;
    public PlantManager plantManager;
    public CreatureManager creatureManager;
    public float full;
    System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {

        full = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        food -= .01f;
        Prioritize1();
        //SniffFood();
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
        if(plantManager.DeleteClosest(transform.position))
        {
            food++;
        }
        print(food);
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
        if(closest.magnitude > 0.1)
        {
            transform.position += speed * (Vector3) closest.normalized;
        }
        else{
            Eat();
        }

    }
    void Reproduce()
    {
        food = food / 2;
        creatureManager.Clone(gameObject);

    }
    void Prioritize1()
    {
        if(food >= full)
        {
            Reproduce();
        }
        else
        {
            SniffFood();
        }
    }

}
