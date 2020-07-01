using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public bool potionPick;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!potionPick)
            {
                GetComponent<AudioSource>().Play();
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().playerHealth < 100)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().playerHealth += 10;
                }
                GetComponent<SpriteRenderer>().enabled = false;
                potionPick = true;
            }
        }
    }
}
