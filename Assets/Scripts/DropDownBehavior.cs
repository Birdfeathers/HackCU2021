using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownBehavior : MonoBehaviour
{
    public GameObject plantButtons;
    public GameObject animalButtons;
    public GameObject othersButton;
    public GameObject plantPop;
    public GameObject animalPop;
    public GameObject newAnimalButtons;
    public GameObject newPlantButtons;
    public GameObject plantDropdown;
    public GameObject animalDropdown;

    public Graph graph;
    public ButtonManagerBehavior bmb;
    //int value;
    /*
    1. Time
    2. Animal correlations
    3. Plant Correlations
    */
    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = GetComponent<Dropdown>().value;

    }

    public void SetVariables()
    {
        index = GetComponent<Dropdown>().value;
        graph.DestroyLines();
        bmb.OffButtons();
        if(index == 0)
        {
            othersButton.SetActive(true);
            newAnimalButtons.SetActive(false);
            newPlantButtons.SetActive(false);
            plantPop.SetActive(true);
            animalPop.SetActive(true);
            plantDropdown.SetActive(false);
            animalDropdown.SetActive(false);
        }
        else if(index == 1)
        {
            animalButtons.SetActive(true);
            plantButtons.SetActive(false);
            othersButton.SetActive(false);
            newAnimalButtons.SetActive(true);
            animalPop.SetActive(false);
            othersButton.GetComponent<MoreButtonBehavior>().current = 0;
            plantDropdown.SetActive(false);
            animalDropdown.SetActive(true);
        }
        else
        {
            animalButtons.SetActive(false);
            plantButtons.SetActive(true);
            othersButton.SetActive(false);
            newPlantButtons.SetActive(true);
            plantPop.SetActive(false);
            othersButton.GetComponent<MoreButtonBehavior>().current = 1;
            plantDropdown.SetActive(true);
            animalDropdown.SetActive(false);

        }
    }
}
