using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public GameObject audioSound;
    public GameObject slider;
    // Start is called before the first frame update
    void Update()
    {
        audioSound.GetComponent<AudioSource>().volume = slider.GetComponent<Slider>().value;
    }
}
