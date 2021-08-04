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
    public float angle; //angle in radians that the creature turns when unsure where to go
    public float angleChange;
    public int generation;
    public int id;

    void Start()
    {
        gameObject.tag = "creature";

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "creature") {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(),  GetComponent<BoxCollider2D>());
       }
    }

    void FixedUpdate()
    {
        if(food < 0) Die();
        food -= .5f * speed;
        Prioritize1();
    }

    List<Vector2> Smell()
    {
        List<Vector2> smelledPlants = new List<Vector2>();
        foreach(GameObject plant in plantManager.plants)
        {
            Vector2 plantLocation = plant.transform.position;
            if (Vector2.Distance(plantLocation, transform.position) <= smellRadius)
            {
                smelledPlants.Add(plantLocation - (Vector2) transform.position);
            }
        }
        return smelledPlants;
    }
    void Eat()
    {
        if (plantManager.DeleteClosest(transform.position)) { food++; }
        return;
    }

    bool SniffFood()
    {
        List<Vector2> plants = Smell();
        if(plants.Count == 0) return false;
        Vector2 closest = plants[0];
        foreach(Vector2 plant in plants)
        {
            if (plant.magnitude < closest.magnitude)
            {
                closest = plant;
            }
        }
        if (closest.magnitude > 0.1)
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
        if(creatureManager.creatures.Count >= creatureManager.maxCreatures) {return;}
        food = food / 2;
        creatureManager.Clone(gameObject);
    }

    void Die()
    {
        creatureManager.DeleteCreature(gameObject);
    }

    void Prioritize1()
    {
        if (food >= full) { Reproduce(); }
        else
        {
            if (!SniffFood())
            {
                transform.position += speed * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
                angle += Random.Range(-angleChange/ (speed *100), angleChange / (speed * 100));
            }
        }
    }
}
