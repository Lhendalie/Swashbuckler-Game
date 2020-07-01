using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsShow : MonoBehaviour
{
    public GameObject panelCtrl;

    public bool climbSHown;
    public bool swimshwon;
    public bool moveShown;
    public bool shootshown;
    public bool chestShown;
    public bool bombShown;
    public bool sabreShown;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(gameObject.name == "Climb" && !climbSHown)
            {
                panelCtrl.GetComponent<PanelControl>().OpenInstructionsPanel(gameObject.name);
                climbSHown = true;
            }
            else if (gameObject.name == "Swim" && !swimshwon)
            {
                panelCtrl.GetComponent<PanelControl>().OpenInstructionsPanel(gameObject.name);
                swimshwon = true;
            }
            else if (gameObject.name == "Jump" && !moveShown)
            {
                panelCtrl.GetComponent<PanelControl>().OpenInstructionsPanel(gameObject.name);
                moveShown = true;
            }
            else if (gameObject.name == "Shoot" && !shootshown)
            {
                panelCtrl.GetComponent<PanelControl>().OpenInstructionsPanel(gameObject.name);
                shootshown = true;
            }
            else if (gameObject.name == "Chest" && !chestShown)
            {
                panelCtrl.GetComponent<PanelControl>().OpenInstructionsPanel(gameObject.name);
                chestShown = true;
            }
            else if (gameObject.name == "Bomb" && !bombShown)
            {
                panelCtrl.GetComponent<PanelControl>().OpenInstructionsPanel(gameObject.name);
                bombShown = true;
            }
            else if (gameObject.name == "Sabre" && !sabreShown)
            {
                panelCtrl.GetComponent<PanelControl>().OpenInstructionsPanel(gameObject.name);
                sabreShown = true;
            }
        }
    }
}
