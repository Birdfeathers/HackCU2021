using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBehavior : MonoBehaviour
{
    private LogConverter lc;
    public CreatureManager cm;
    public PlantManager pm;
    public GameObject panel;
    public GameObject done;
    private void Write()
    {
        lc.WriteTime();
        lc.WriteCreature();
        lc.WritePlant();
        lc.Close();
    }
    private void Save(string name)
    {
        lc = new LogConverter(name, cm, pm);
        Write();

    }

    public void Type()
    {
        TMP_InputField input = GetComponent<TMP_InputField>();
        Save(input.text);
        done.SetActive(true);
        panel.SetActive(false);
    }

}
