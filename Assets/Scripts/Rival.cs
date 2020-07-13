using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rival : MonoBehaviour
{
    public float speed = 3f;
    public float force = 9;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        //Change direction
        if(other.GetComponent<Ball>() != null){ 
                Vector3 dir = player.position - transform.position;
                print("Bola");
                //Random number for varying direction(?)
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 3, 0);
        }

    }
}
