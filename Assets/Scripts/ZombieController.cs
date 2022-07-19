using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public GameObject Zombie;
    public float speed = 0.5f;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        Zombie = GetComponent<GameObject>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        gm.GameOver();
    }
}
