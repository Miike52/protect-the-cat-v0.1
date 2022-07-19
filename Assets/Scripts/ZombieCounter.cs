using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieCounter : MonoBehaviour
{
    private GameManager gm;
    GameObject[] zombies;
    public Text zombieCountText;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zombies = GameObject.FindGameObjectsWithTag("Danger");
        zombieCountText.text = "ZOMBIE LEFT: " + zombies.Length.ToString();

        if (zombies.Length <= 0)
        {
            gm.YouWon();
        }
    }  
}
