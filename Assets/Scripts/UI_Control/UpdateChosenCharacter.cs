using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateChosenCharacter : MonoBehaviour
{
    public GameObject CaptainBeard;
    public GameObject Atlas;
    public GameObject Katrina;
    public GameObject ChampionSelectPanel;

    public GameObject ChosenPlayer;

    public void Start()
    {
        ChosenPlayer = GameObject.FindGameObjectWithTag("ChosenPlayer");
        ChosenPlayer.GetComponent<DontDestroyOnLoad>().characterName = "CaptainBeard";
    }
    public void Update()
    {
        if (ChampionSelectPanel.activeInHierarchy)
        {
            if (Katrina.activeInHierarchy)
            {
                ChosenPlayer.GetComponent<DontDestroyOnLoad>().characterName = "Katrina";
            }
            else if (Atlas.activeInHierarchy)
            {
                ChosenPlayer.GetComponent<DontDestroyOnLoad>().characterName = "Atlas";
            }
            else
            {
                ChosenPlayer.GetComponent<DontDestroyOnLoad>().characterName = "CaptainBeard";
            }
        }
    }
}
