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
    public float thriftiness;
    public float angle; //angle in radians that the creature turns when unsure where to go
    public float angleChange;
    public int generation;
    public int id;
    public List<GameObject> touching;
    public int strat;

    void Start()
    {
        gameObject.tag = "creature";

    }

    void OnCollisionEnter2D(Collision2D other)
    {
       //  if(collision.gameObject.tag == "creature") {
       //      Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(),  GetComponent<BoxCollider2D>());
       // }
       touching.Add(other.gameObject);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        touching.Remove(other.gameObject);
    }

    void Update()
    {
        for(int i = 0; i < touching.Count; i++)
        {
            if(touching[i] == null) touching.RemoveAt(i);
        }
    }

    void FixedUpdate()
    {
        if(food < 0) Die();
        food -= .5f * speed;
        GetComponent<Rigidbody2D>().mass = food;
        float size = Mathf.Sqrt(GetComponent<Rigidbody2D>().mass /5);
        transform.localScale = new Vector3(size, size, 1);
        CallStrat();
    }

    void CallStrat()
    {
        switch(strat)
        {
            case 1:
                Prioritize1();
                break;
            case 2:
                Prioritize2();
                break;
        }
    }

    Vector2 getRelLocation(GameObject thing)
    {
        Vector2 thingLocation = thing.transform.position;
         return (thingLocation - (Vector2) transform.position);
    }

    List<GameObject> Smell(List<GameObject> things)
    {
        List<GameObject> smelledThings = new List<GameObject>();
        foreach(GameObject thing in things)
        {
            Vector2 relLocation = getRelLocation(thing);
            if (relLocation.magnitude <= smellRadius)
            {
                smelledThings.Add(thing);
            }
        }
        return smelledThings;
    }
    bool Eat(string type, GameObject thing)
    {
        if(type == "plant")
        {
            if (plantManager.DeletePlant(thing, transform.position)) {
                food++;
                return true;
            }
        }
        return false;
    }

    GameObject returnClosest(List<GameObject> things)
    {
        GameObject closest = things[0];
        Vector2 shortest = getRelLocation(things[0]);
        foreach(GameObject thing in things)
        {
            Vector2 relLocation = getRelLocation(thing);
            if (relLocation.magnitude < shortest.magnitude)
            {
                closest = thing;
                shortest = relLocation;
            }
        }
        return closest;
    }

    float Vector2Angle(Vector2 vec)
    {
        float newAngle = Mathf.Acos(vec.normalized.x);
        if (vec.y < 0) { newAngle = -newAngle; } //need y to uniquely determine angle.
        return newAngle;
    }

    void MoveTowards(Vector2 relLoc)
    {
        transform.position += speed * (Vector3) relLoc.normalized;
        angle = Vector2Angle(relLoc);
    }

    void MoveAwayFrom(Vector2 location)
    {
        transform.position -= speed * (Vector3) location.normalized;
        angle = Vector2Angle(location);

    }

    void MoveAdjacentTo(Vector2 location)
    {
        Vector2 Adjacent = new Vector2(location.y, location.x * -1);
        transform.position += speed * (Vector3) Adjacent.normalized;
        angle = Vector2Angle(location);
    }



    bool SniffFood(string type)
    {
        List<GameObject> plants = Smell(plantManager.plants);
        if(plants.Count == 0) return false;
        GameObject closest = returnClosest(plants);
        Vector2 relLoc = getRelLocation(closest);
        if (relLoc.magnitude > 0.1)
        {
            MoveTowards(relLoc);
        }
        else{
            Eat(type, closest);
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

    void Wander()
    {
        transform.position += speed * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        angle += Random.Range(-angleChange/ (speed *100), angleChange / (speed * 100));
    }

    Vector2 getAveAngle()
    {
        Vector2 sum = new Vector2(0, 0);
        for(int i = 0; i < touching.Count; i++)
        {
            if(touching[i] == null) touching.RemoveAt(i);
            else sum += getRelLocation(touching[i]);
        }
        return sum.normalized;
    }

    void SpaceOut()
    {
        MoveAdjacentTo(getAveAngle());
    }

    void Prioritize1()
    {
        if (food >= thriftiness) { Reproduce(); }
        else
        {
            if (!SniffFood("plant"))
            {
                Wander();
            }
        }
    }

    void Prioritize2()
    {
        if (food >= thriftiness) { Reproduce(); }
        else
        {
            if(touching.Count != 0) SpaceOut();
            else if (!SniffFood("plant"))
            {
                Wander();
            }
        }
    }
}
