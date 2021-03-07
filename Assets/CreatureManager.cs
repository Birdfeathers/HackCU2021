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

    public void Clone(GameObject creature)
    {
        creatures.Add(Instantiate(creature));
    }

}
