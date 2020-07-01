using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject controlsPanel;

    public void openOptionsPanel()
    {
        menuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void openMenuPanel()
    {
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void openControlsPanel()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
}
