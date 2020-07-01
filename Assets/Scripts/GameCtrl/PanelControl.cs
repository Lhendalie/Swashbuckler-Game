using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject LevelCompletePanel;
    public GameObject InstructionsPanel;

    public GameObject ClimbPanel;
    public GameObject SwimPanel;
    public GameObject JumpPanel;
    public GameObject shootPanel;
    public GameObject ChestPanel;
    public GameObject SabrePanel;
    public GameObject BombPanel;

    public Text GameOverScoreTxt;
    public Text GameWinScoreTxt;
    public Text scoreText;
    public Text moveInstructions;

    private void Start()
    {
        Time.timeScale = 1;
       
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            OpenPausePanel();
        }

        GameOverScoreTxt.text = scoreText.text;
        GameWinScoreTxt.text = scoreText.text;
    }

    public void OpenPausePanel()
    {
        if (!PausePanel.activeInHierarchy)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }

        PauseTime();
    }

    public void OpenGOPanel()
    {
        if (!GameOverPanel.activeInHierarchy)
        {
            GameOverPanel.SetActive(true);
        }
        else
        {
            GameOverPanel.SetActive(false);
        }

        PauseTime();
    }

    public void OpenLCPanel()
    {
        if (!LevelCompletePanel.activeInHierarchy)
        {
            LevelCompletePanel.SetActive(true);
            if (GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().chosenLevel == "Level_1")
            {
                GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level1Completed = true;
            }
            else if(GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().chosenLevel == "Level_2")
            {
                GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level2Completed = true;
                GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level1Completed = false;
            }
            else if(GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().chosenLevel == "Level_3")
            {
                GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level3Completed = true;
                GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level2Completed = false;
                GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level1Completed = false;
            }
        }
        else
        {
            LevelCompletePanel.SetActive(false);
        }

        PauseTime();
    }

    public void OpenInstructionsPanel(string instructionName)
    {
        InstructionsPanel.SetActive(true);

        if (instructionName == "Climb")
        {
            ClimbPanel.SetActive(true);
        }
        else if (instructionName == "Swim")
        {
            SwimPanel.SetActive(true);
        }
        else if (instructionName == "Shoot")
        {
            shootPanel.SetActive(transform);
        }
        else if (instructionName == "Chest")
        {
            ChestPanel.SetActive(true);
        }
        else if (instructionName == "Jump")
        {
            JumpPanel.SetActive(true);
        }
        else if (instructionName == "Sabre")
        {
            SabrePanel.SetActive(true);
        }
        else if (instructionName == "Bomb")
        {
            BombPanel.SetActive(true);
        }
    }

    public void HideInstructionPanel(string panelName)
    {
        GameObject.Find(panelName).SetActive(false);
        InstructionsPanel.SetActive(false);
    }

    void PauseTime()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
