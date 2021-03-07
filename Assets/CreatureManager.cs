using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public GameObject creaturePrefab;
    public List<GameObject> creatures;
    // Start is called before the first frame update
    void Start()
    {
        //print("Status at Start----");
        //GenerateReport();
    }
    // Update is called once per frame
    void Update()
    {

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
        creatures.Remove(creature);
        Destroy(creature);
        return;
    }
    float TransformNumber(float number)
    {
        number += Random.Range(-.5f *number, .5f * number);
        return number;
    }
    void Mutate(CreatureBehavior creature)
    {
        creature.speed = TransformNumber(creature.speed);
        creature.full = TransformNumber(creature.full);
        creature.speed = TransformNumber(creature.speed);
        creature.smellRadius = TransformNumber(creature.smellRadius);

    }

    public CreatureBehavior Clone(GameObject creature)
    {
        CreatureBehavior clone = Instantiate(creature, transform).GetComponent<CreatureBehavior>();
        clone.transform.position += (Vector3) Random.insideUnitCircle.normalized*clone.speed*100;
        Mutate(clone);
        creatures.Add(clone.gameObject);
        return clone;
    }

}
