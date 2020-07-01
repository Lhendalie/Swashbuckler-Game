using DigitalRuby.RainMaker;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject rain;
    public GameObject spawnLocation;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;

    public GameObject captainImg;
    public GameObject atlasImg;
    public GameObject katrinaImg;

    public bool playerSpawned;

    private string characterName;

    private void Start()
    {
        if(GameObject.Find("ChosenPlayer") != null)
        { 
            characterName = GameObject.Find("ChosenPlayer").GetComponent<DontDestroyOnLoad>().characterName;
        }

        if (characterName != null)
        {            
            if (characterName == "Atlas")
            {
                SpawnPlayer(player2);
                atlasImg.SetActive(true);
            }
            else if (characterName == "Katrina")
            {
                SpawnPlayer(player3);
                katrinaImg.SetActive(true);
            }
            else
            {
                SpawnPlayer(player1);
                captainImg.SetActive(true);
            }
        }
        else
        {
            SpawnPlayer(player1);
            captainImg.SetActive(true);
        }
    }

    private void SpawnPlayer(GameObject player)
    {
        GameObject.Instantiate(player, spawnLocation.transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().playerHealth = 100;
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (rain != null)
        {
            rain.GetComponent<RainScript2D>().Camera = camera;
        }
        playerSpawned = true;
    }
}
