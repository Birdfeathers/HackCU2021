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
    public float full;
    public float angle; //angle in radians that the creature goes when unsure where to go
    public float angleChange;



    //System.Random rnd = new System.Random();

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
        if(food < 0) Die();
        food -= .005f;
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
        //print(food);
        return;
    }

    bool SniffFood()
    {
        List<Vector2> plants = Smell();
        if(plants.Count == 0) return false;
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
            float newAngle = Mathf.Acos(closest.normalized.x);
            if (closest.y < 0) { newAngle = -newAngle; } //need y to uniquely determine angle.
            angle = newAngle;
        }
        else{
            Eat();
        }
        return true;

    }
    void Reproduce()
    {
        food = food / 2;
        creatureManager.Clone(gameObject);



    }
    void Die()
    {
        creatureManager.DeleteSelf(gameObject);
    }
    void Prioritize1()
    {
        if(food >= full)
        {
            Reproduce();
        }
        else
        {
            if(!SniffFood())
            {
                transform.position += speed * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
                angle += Random.Range(-angleChange, angleChange);
            }
        }
    }


}
