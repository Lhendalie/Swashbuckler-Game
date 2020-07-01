using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChampSelectControl : MonoBehaviour
{
    public GameObject championSelectPanel;
    public GameObject levelSelectPanel;
    public GameObject instructionPanel;
    //public GameObject levelWarningPanel;
    //public GameObject champWarningPanel;

    public Button championSelectBtn;
    public Button levelSelectBtn;
    public Button instructionBtn;
    public Button binocularsBtn;

    public GameObject captainBeardCh;
    public GameObject atlasCh;
    public GameObject katrinaCh;

   public void OpenChamptionSelect()
    {
        championSelectPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        instructionPanel.SetActive(false);

        championSelectBtn.enabled = false;
        levelSelectBtn.enabled = false;
        instructionBtn.enabled = false;
        binocularsBtn.enabled = false;
    }
   public void OpenLevelSelect()
    {
        championSelectPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
        instructionPanel.SetActive(false);

        championSelectBtn.enabled = false;
        levelSelectBtn.enabled = false;
        instructionBtn.enabled = false;
        binocularsBtn.enabled = false;
    }
    public void OpenInstructionPanel()
    {
        championSelectPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        instructionPanel.SetActive(true);

        championSelectBtn.enabled = false;
        levelSelectBtn.enabled = false;
        instructionBtn.enabled = false;
        binocularsBtn.enabled = false;
    }

    //public void OpenLevelWarningPanel()
    //{
    //    levelWarningPanel.SetActive(true);
    //}

    //public void OpenChampWarningPanel()
    //{
    //    champWarningPanel.SetActive(true);
    //}

    public void ClosePanel()
    {
        championSelectPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        instructionPanel.SetActive(false);
        //champWarningPanel.SetActive(false);
        //levelWarningPanel.SetActive(false);


        levelSelectBtn.enabled = true;
        instructionBtn.enabled = true;
        binocularsBtn.enabled = true;
        championSelectBtn.enabled = true;
    }

    public void ShowNextCharacter()
    {
        if (captainBeardCh.activeInHierarchy)
        {
            captainBeardCh.SetActive(false);
            atlasCh.SetActive(true);
        }
        else if (atlasCh.activeInHierarchy)
        {
            atlasCh.SetActive(false);
            katrinaCh.SetActive(true);
        }
        else
        {
            katrinaCh.SetActive(false);
            captainBeardCh.SetActive(true);
        }
    }

    public void ShowPreviousCharacter()
    {
        if (captainBeardCh.activeInHierarchy)
        {
            captainBeardCh.SetActive(false);
            katrinaCh.SetActive(true);
        }
        else if (atlasCh.activeInHierarchy)
        {
            atlasCh.SetActive(false);
            captainBeardCh.SetActive(true);
        }
        else
        {
            katrinaCh.SetActive(false);
            atlasCh.SetActive(true);
        }
    }
}
