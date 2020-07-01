using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public Text playerScore;
    public bool coinPick;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!coinPick)
            {
                GetComponent<AudioSource>().Play();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().playerScore++;
                playerScore.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().playerScore.ToString();
                GetComponent<SpriteRenderer>().enabled = false;
                coinPick = true;
            }            
        }
    }
}
