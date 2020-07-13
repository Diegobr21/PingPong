using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rival;

    public Animator animPlayer;

    public GameObject playerGO;

    public GameObject ballGO;
    public Collider ballCollider;
    public float speed = 3f;
    public float force = 10;

    public float h;
    public float v;

    bool hitting;
    public bool wall;
    

   void Start() {
       playerGO = GameObject.FindObjectOfType<Player>().gameObject;
       animPlayer = GetComponent<Animator>();
       ballGO = GameObject.FindObjectOfType<Ball>().gameObject;
       ballCollider = ballGO.GetComponent<SphereCollider>();

   }
    void Update()
    {
        if(wall){
            Movement(0);
        }else{
            Movement(1);
        }
        
        Shoot();       
        
        

    }

    void Movement(int notwall){

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        

        if(h!= 0){ //&& hitting
            playerGO.transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            //hitting = true;
            rival.Translate(new Vector3(-h, 0, 0) * speed * Time.deltaTime);
        }else{
            
            print("Wall");
        
        }

    }
    private void OnTriggerEnter(Collider other) {
        
        if(other.GetComponent<Ball>() != null){ // && hitting
                Vector3 dir = rival.position - transform.position;
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 3, 0);
        }

    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Wall")){
            //print("Wall");
            //wall = true;
        }
        
    }

    void Shoot(){
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            animPlayer.SetInteger("IZQUIERDA", 1);
            hitting = true;
        }else{
            animPlayer.SetInteger("IZQUIERDA", 0);
        }
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            animPlayer.SetInteger("DERECHA", 1);
            hitting = true;
        }else{
            animPlayer.SetInteger("DERECHA", 0);
        }
        

    }

    
}
