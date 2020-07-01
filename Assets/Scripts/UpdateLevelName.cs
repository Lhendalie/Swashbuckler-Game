using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpdateLevelName : MonoBehaviour
{
    public GameObject button;
    private LevelLoaderSpecial script;
    private DontDestroyOnLoad script2;

    private void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        script = GameObject.Find("SceneControl").GetComponent<LevelLoaderSpecial>();
        script2 = GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>();
    }

    void TaskOnClick()
    {
        script.chosenLevel = button.name;
        script2.chosenLevel = button.name;
        Debug.Log("Selected" + button.name);
    }
}
