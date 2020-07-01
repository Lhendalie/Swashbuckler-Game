using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public string characterName;
    public string chosenLevel;
    public bool level1Completed;
    public bool level2Completed;
    public bool level3Completed;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ChosenPlayer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
