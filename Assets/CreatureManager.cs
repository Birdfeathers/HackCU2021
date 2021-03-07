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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public CreatureBehavior Clone(GameObject creature)
    {
        CreatureBehavior clone = Instantiate(creature, transform).GetComponent<CreatureBehavior>();
        clone.transform.position += (Vector3) Random.insideUnitCircle.normalized*clone.speed*100;
        creatures.Add(clone.gameObject);
        return clone;
    }

}
