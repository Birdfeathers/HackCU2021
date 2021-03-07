using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public int growthTime; //average time before growing in frames

    private PlantManager plantManager;
    private int timeTillGrowth;

    // Start is called before the first frame update
    void Start()
    {
        plantManager = transform.parent.GetComponent<PlantManager>();
        timeTillGrowth = growthTime + Random.Range(-10, 11);//mutate the growth range a little for organicness
    }


    void FixedUpdate()
    {
        if (timeTillGrowth <= 0)
        {
            Grow();
            timeTillGrowth = growthTime + Random.Range(-10, 11);//mutate the growth range a little for organicness
        }
        timeTillGrowth--;
    }

    private void Grow()
    {
        plantManager.NewPlantAt(3*Random.insideUnitCircle + (Vector2) transform.position);
    }
}
