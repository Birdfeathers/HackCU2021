using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehavior : MonoBehaviour
{
    public float smellRadius;
    public float food;
    public float speed;
    public PlantManager plantManager;
    public CreatureManager creatureManager;

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
        plantManager.DeleteClosest(transform.position);
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
        if(closest.magnitude > 0.001)
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
    }
    class Behaviour
    {
        Func< List<Vector2>> sense;
        Action<List <Vector2>> motor;
        Action decide;
    }

}
