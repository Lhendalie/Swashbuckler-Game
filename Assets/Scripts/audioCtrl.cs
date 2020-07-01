using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioCtrl : MonoBehaviour
{
    public GameObject rainPrefab;

    private RainScript2D rainScript;

    private bool rainChanged = true;

    // Start is called before the first frame update
    void Start()
    {
        rainScript = rainPrefab.GetComponent<RainScript2D>();
        rainScript.RainIntensity = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rainChanged)
        {
            StartCoroutine(ChangeRain());
        }
    }

    private IEnumerator ChangeRain()
    {
        rainChanged = false;

        int randomNumber = Random.Range(15, 30);

        yield return new WaitForSeconds(randomNumber);

        if (rainScript.RainIntensity == 0.3f)
        {
            rainScript.RainIntensity = 0.9f;
        }
        else
        {
            rainScript.RainIntensity = 0.3f;
        }

        rainChanged = true;
    }
}
