using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rival : MonoBehaviour
{
    private readonly Random _random = new Random();  
    public float speed = 2f;
    public float force = 3;
    public Animator animRival;

    AudioSource audioSource;
    public Transform ball;
    public GameObject BallGO;
    public Ball ballScript;
    public Transform player;
    public Transform[] BotAimtargets;
    Vector3 targetPosition;
    Vector3 initPosition;

    ShotManager shotManager;
    Shot currentShot;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        animRival = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        shotManager = GetComponent<ShotManager>();
        BallGO = GameObject.Find("Ball");
        ballScript = BallGO.GetComponent<Ball>();
        currentShot = shotManager.normal;
        initPosition = transform.position;
          
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){

        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {

        
        SelectRandomShot();
        
        
        //Change direction
        if(other.GetComponent<Ball>() != null){ 
            ballScript.hitter = "rival";
            animRival.SetTrigger("SHOT");
                //animRival.SetTrigger("EndSHOT");
            Vector3 dir = PickTarget() - transform.position;
            audioSource.Play();
                //animRival.SetInteger("SHOT", 0);
                //Random number for varying direction(?)
                
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
                //animRival.SetTrigger("EndSHOT");
                //animRival.SetTrigger("SHOT");
                
        }

    }

    public float RandomNumber(){
        return Random.Range(0.1f, 7.0f); 
    }

    Vector3 PickTarget(){
        int randValue = Random.Range(0, BotAimtargets.Length);
        return BotAimtargets[randValue].position;
    }

    public void ResetPosition(){
        transform.position = initPosition;
    }
    void SelectRandomShot(){
        float shot = RandomNumber();
        if(shot <= 4){
            currentShot = shotManager.normal;
            print("normal");
        }
        else if( shot >= 4 && shot <= 5.8){
            currentShot = shotManager.topSpin;
            print("top");
        }
        else if (shot > 5.8){
            currentShot = shotManager.flat;
            print("flat");
        }

    }


}
