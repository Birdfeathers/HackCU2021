using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreatureManager : MonoBehaviour
{
    public GameObject creaturePrefab;
    public List<GameObject> creatures;
    public int time;
    public float colorMutability; //how much it should mutate each channel from previous, as a multiplier
    public float mutability;

    private StreamWriter log;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        File.Create("log.txt").Dispose();
        log = File.AppendText("log.txt");
        CreatureBehavior creature = creatures[0].GetComponent<CreatureBehavior>();
        log.Write($"Add; Speed {creature.speed}; Full {creature.full}; smell {creature.smellRadius}; time {time}\n");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Enter"))
        {
            GenerateReport();
        }

    }
    void FixedUpdate()
    {
        time++;
    }

    void GenerateReport()
    {
        float totalSpeed = 0, totalFull = 0, totalSmellRange= 0;
        for(int i = 0; i < creatures.Count; i++)
        {
            CreatureBehavior creature =  creatures[i].GetComponent<CreatureBehavior>();
            totalSpeed += creature.speed;
            totalFull += creature.full;
            totalSmellRange += creature.smellRadius;
        }
        float aveSpeed = totalSpeed / creatures.Count;
        float aveFull = totalFull /creatures.Count;
        float aveSmell= totalSmellRange / creatures.Count;
        print($"Average Speed: {aveSpeed} \n");
        print($"Average Full: {aveFull} \n");
        print($"Average Smell Radius: {aveSmell} \n");
    }
    public void DeleteSelf(GameObject creature)
    {
        CreatureBehavior B = creature.GetComponent<CreatureBehavior>();
        log.Write($"Remove; Speed {B.speed}; Full {B.full}; smell {B.smellRadius}; time {time}\n" );
        log.Flush();
        creatures.Remove(creature);
        Destroy(creature);
        return;
    }
    float TransformNumber(float number)
    {
        number += Random.Range(-mutability *number, mutability * number);
        return number;
    }
    float TransformNumber(float number, float factor)
    {
        number += Random.Range(-factor * number, factor * number);
        return number;
    }
    void Mutate(CreatureBehavior creature)
    {
        creature.speed = TransformNumber(creature.speed);
        creature.full = TransformNumber(creature.full);
        creature.speed = TransformNumber(creature.speed);
        creature.smellRadius = TransformNumber(creature.smellRadius);
        Color oldColor = creature.gameObject.GetComponent<SpriteRenderer>().color;
        Color newColor = new Color
            (
            TransformNumber(oldColor.r, colorMutability),
            TransformNumber(oldColor.g, colorMutability),
            TransformNumber(oldColor.b, colorMutability)
            );
        creature.gameObject.GetComponent<SpriteRenderer>().color = newColor;

    }

    public CreatureBehavior Clone(GameObject creature)
    {
        CreatureBehavior clone = Instantiate(creature, transform).GetComponent<CreatureBehavior>();
        clone.transform.position += (Vector3) Random.insideUnitCircle.normalized;
        Mutate(clone);
        creatures.Add(clone.gameObject);
        log.Write($"Add; Speed {clone.speed}; Full {clone.full}; smell {clone.smellRadius}; time {time}\n" );
        log.Flush();
        return clone;
    }

}
