using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelAnim : MonoBehaviour
{
    public GameObject animPanel;
    public GameObject audioMusic;

    public GameObject path;
    public GameObject path2;

    public Button nextLvlBtn;
    public Button nextLvlBtn2;

    public Button lampBtn;
    public Button wheelbtn;
    public Button levelbtn;
    public Button Menu;
    public Button Play;
    public Button animPanelCloseBtn;

    private bool routineFinished;
    private bool routineFinished2;

    private void Start()
    {
        if (!routineFinished)
        {
            if (GameObject.FindGameObjectWithTag("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level1Completed)
            {
                StartCoroutine(StartAnimation(0));
            }
        }
        if (!routineFinished2)
        {
            if (GameObject.FindGameObjectWithTag("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level2Completed)
            {
                StartCoroutine(StartAnimation2(0));
            }
        }
        if (GameObject.FindGameObjectWithTag("ChosenPlayer").GetComponent<DontDestroyOnLoad>().level3Completed)
        {
            path.SetActive(true);
            path2.SetActive(true);
            nextLvlBtn.interactable = true;
            nextLvlBtn2.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAnimation(float seconds)
    {
        Time.timeScale = 1f;

        routineFinished = true;

        animPanelCloseBtn.enabled = false;
        lampBtn.enabled = false;
        wheelbtn.enabled = false;
        levelbtn.enabled = false;
        Menu.enabled = false;
        Play.enabled = false;

        yield return new WaitForSeconds(seconds);

        animPanel.SetActive(true);
        path.SetActive(true);
        audioMusic.GetComponent<AudioSource>().Stop();
        path.GetComponent<Animator>().SetBool("IsActive", true);
        path.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(6);

        nextLvlBtn.interactable = true;

        yield return new WaitForSeconds(1);

        audioMusic.GetComponent<AudioSource>().Play();
        path.GetComponent<AudioSource>().volume = 0.1f;

        nextLvlBtn.interactable = true;
        animPanelCloseBtn.enabled = true;
        //nextLvlBtn.GetComponent<AudioSource>().Play();
        lampBtn.enabled = true;
        wheelbtn.enabled = true;
        levelbtn.enabled = true;
        Menu.enabled = true;
        Play.enabled = true;
    }

    IEnumerator StartAnimation2(float seconds)
    {
        Time.timeScale = 1f;

        routineFinished2 = true;

        animPanelCloseBtn.enabled = false;
        lampBtn.enabled = false;
        wheelbtn.enabled = false;
        levelbtn.enabled = false;
        Menu.enabled = false;
        Play.enabled = false;
        nextLvlBtn.interactable = true;
        path.SetActive(true);

        yield return new WaitForSeconds(seconds);

        animPanel.SetActive(true);
        path2.SetActive(true);
        audioMusic.GetComponent<AudioSource>().Stop();
        path2.GetComponent<Animator>().SetBool("IsActive", true);
        path2.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(6);

        nextLvlBtn2.interactable = true;

        yield return new WaitForSeconds(1);

        audioMusic.GetComponent<AudioSource>().Play();
        path2.GetComponent<AudioSource>().volume = 0.1f;

        animPanelCloseBtn.enabled = true;
        //nextLvlBtn2.GetComponent<AudioSource>().Play();
        lampBtn.enabled = true;
        wheelbtn.enabled = true;
        levelbtn.enabled = true;
        Menu.enabled = true;
        Play.enabled = true;
    }
}
