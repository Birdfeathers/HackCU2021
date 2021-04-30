using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public int growthTime; //average time before growing in frames
    public int timeTillGrowth;
    public int id;
    public int generation;

    private PlantManager plantManager;

    void Awake()
    {
        plantManager = transform.parent.GetComponent<PlantManager>();
        timeTillGrowth = growthTime + Random.Range(-10, 11); //mutate the growth range a little for organicness
    }


    void FixedUpdate()
    {
        if (timeTillGrowth <= 0)
        {
            Grow();
            timeTillGrowth = growthTime + Random.Range(-10, 11); //mutate the growth range a little for organicness
        }
        timeTillGrowth--;
    }

    private void Grow()
    {
        //new plant between .8 and 1.5 units away
        Vector2 randomVector = Random.insideUnitCircle;
        Vector2 location = (0.7f * randomVector) + (.8f * randomVector.normalized) + (Vector2)transform.position;
        _ = plantManager.NewPlantAt(location, gameObject);
    }
}
