using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipArriveAnim : MonoBehaviour
{
    private Transform shipPos;
    private Camera cam;
    private Vector2 direction = Vector2.right;

    private AudioSource bgMusic;
    private AudioSource intro;

    private bool animationOver;

    private float camSpeed = 1.7f;

    void Start()
    {
        shipPos = GameObject.Find("Ship").GetComponent<Transform>();
        bgMusic = GameObject.Find("BGAudio").GetComponent<AudioSource>();
        intro = GameObject.Find("Intro").GetComponent<AudioSource>();

        bgMusic.Stop();
        intro.Play();
        transform.position = new Vector3(shipPos.position.x, transform.position.y, transform.position.z);
        cam = GetComponent<Camera>();
        cam.orthographicSize = 7.3f;

        StartCoroutine(playAnim(12.624f));
    }

    void Update()
    {
        if (cam.orthographicSize > 5)
        {
            cam.orthographicSize -= Time.deltaTime / 5;
        }

        if (!animationOver)
        {
            CameraMove();
        }
    }

    void CameraMove()
    {
        transform.Translate(direction * camSpeed * Time.deltaTime);
    }

    private IEnumerator playAnim(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        animationOver = true;
        bgMusic.Play();
    }
}
