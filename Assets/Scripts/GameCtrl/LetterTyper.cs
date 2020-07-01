using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterTyper : MonoBehaviour
{
    private float letterPause = 0.03f;

    //public AudioClip typeSound1;
    //public AudioClip typeSound2;

    string message;
    Text textComp;
    bool coroutineReady;
    bool coroutineFinished;

    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        coroutineReady = true;
        coroutineFinished = false;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && coroutineReady)
        {
            StartCoroutine(TypeText());
        }

        if (coroutineFinished)
        {
            GetComponent<AudioSource>().Stop();
        }

    }

    IEnumerator TypeText()
    {
        coroutineReady = false;
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            //if (typeSound1 && typeSound2)
            //    SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        coroutineFinished = true;
    }
}
