using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderSpecial : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    private LevelLoaderSpecial script;
    private DontDestroyOnLoad script2;

    public string chosenLevel = "Level_1";

    public void Start()
    {
        script = GameObject.Find("SceneControl").GetComponent<LevelLoaderSpecial>();
        script2 = GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>();
        script.chosenLevel = "Level_1";
        script2.chosenLevel = "Level_1";

        Debug.Log(script.chosenLevel + "and" + script2.chosenLevel);
    }

    public void LoadLevel()
    {
        Debug.Log("Selected" + chosenLevel);
        StartCoroutine(LoadAsynchronously(chosenLevel));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
