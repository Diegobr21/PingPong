using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Transform rival;
    public Transform[] Aimtargets;
    public Animator animPlayer;
    AudioSource audioSource;

    public GameObject playerGO;
    public Vector3 initialPlayerPosition;
    public Vector3 aimTargetSelected;
    public GameObject paletaDerecha;
    public GameObject paletaIzquierda;

    public GameObject ballGO;
    public Collider ballCollider;
    public Ball ballScript;
    public float speed = 3f;
    public float force = 10;

    public float h;
    public float v;

    //Touch controls
    public Joystick joystick;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;
    public float SWIPE_THRESHOLD = 20f;

    //----

    public bool hittingright;
    public bool hittingLeft;
    public bool wall = false;
    public GameController gameControllerScript;
    public GameObject TableGO;



    ShotManager shotManager;
    Shot currentShot;
    

   void Start() {
        playerGO = GameObject.FindObjectOfType<Player>().gameObject;
        initialPlayerPosition = transform.position;
        animPlayer = GetComponent<Animator>();
        ballGO = GameObject.FindObjectOfType<Ball>().gameObject;
        ballCollider = ballGO.GetComponent<SphereCollider>();
        ballScript = ballGO.GetComponent<Ball>();
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.normal;
        audioSource = GetComponent<AudioSource>();
        gameControllerScript = TableGO.GetComponent<GameController>();
        aimTargetSelected = Aimtargets[1].position; //default center
          
       

   }
    void Update()
    {
        
        if(gameControllerScript.matchStarted){
            Movement();
        }   
        
        ShootAnimation();   
        //StartCoroutine(ShootDelay()); 

    /*
        if (Input.touchCount == 2)
            {
                Touch touch = Input.GetTouch(1);
                currentShot = shotManager.normal;

                if (touch.phase == TouchPhase.Began)
                {
                    hittingLeft = true;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    hittingLeft = false;
                }
            }

        else if(Input.touchCount == 3){

            Touch touch = Input.GetTouch(2);
            currentShot = shotManager.topSpin;
            if (touch.phase == TouchPhase.Began)
                {
                    hittingLeft = true;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    hittingLeft = false;
                }

        }
        else if(Input.touchCount == 4){

            Touch touch = Input.GetTouch(3);
            currentShot = shotManager.flat;
            if (touch.phase == TouchPhase.Began)
                {
                    hittingLeft = true;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    hittingLeft = false;
                }

        }
*/
    
        switch(Input.touchCount){
            case 2: 
                currentShot = shotManager.normal;
                break;

            case 3: 
                currentShot = shotManager.topSpin;
                break;

            case 4: 
                currentShot = shotManager.flat;
                break;

            default: 
                currentShot = shotManager.normal;
                break;

        }
        

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    hittingLeft = true;
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
                hittingLeft = false;
            }
        }       

    }

    private void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                //OnSwipeUp();
                //Apuntar a CenterTarget 
                aimTargetSelected = Aimtargets[1].position;
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                //OnSwipeDown();
                //Apuntar a CenterTarget 
                aimTargetSelected = Aimtargets[1].position;
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                //OnSwipeRight();
                //Apuntar a RightTarget 
                aimTargetSelected = Aimtargets[2].position;
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                //OnSwipeLeft();
                //Apuntar a LeftTarget 
                aimTargetSelected = Aimtargets[0].position;
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
            //aimTargetSelected = Aimtargets[1].position;
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    public void Movement(){

        if(!wall){
            h = joystick.Horizontal;
            //v = joystick.Vertical;
        
        

            if(h!= 0 ){ 
                playerGO.transform.Translate(Vector2.right * h * speed * Time.deltaTime);
                //hitting = true;
                //rival.Translate(new Vector3(-h, 0, 0) * speed * Time.deltaTime);
            }
        
        }else{
            
            print("Wall");
        
        }

    }


    private void OnTriggerEnter(Collider other) {

        
        
        if(other.CompareTag("Ball") && hittingright ){ //&& (hittingright || hittingLeft)
                ballScript.hitter = "player";
                Vector3 dir = aimTargetSelected - transform.position;
                other.GetComponent<Rigidbody>().velocity = (dir.normalized) * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
                audioSource.Play();
               
        }
        else if(other.CompareTag("Ball") && hittingLeft){ 
                ballScript.hitter = "player";
                Vector3 dir = aimTargetSelected - transform.position;
                other.GetComponent<Rigidbody>().velocity = (dir.normalized) * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
                audioSource.Play();
               
        }

    }

    public void Serve(){
        ballScript.hitter = "player";
        if(ballScript.playerServing){
            //print("Ball should rise up and you could shoot if timed correctly");
            transform.position = initialPlayerPosition;
            hittingLeft = true;
        }else{
            hittingLeft = false;
        }

    }

    

    void ShootAnimation(){

    

        if(hittingLeft){
            animPlayer.SetInteger("IZQUIERDA", 1);
        }else{
            animPlayer.SetInteger("IZQUIERDA", 0);
        }
        
        if(hittingright){
            animPlayer.SetInteger("DERECHA", 1);
        }else{
            animPlayer.SetInteger("DERECHA", 0);
        }

        
    }

  

    /*
    IEnumerator ShootDelay(){
        
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            hittingLeft = true;
        
        }else if (Input.GetKeyUp(KeyCode.LeftControl)){
            yield return new WaitForSeconds(1);
            hittingLeft = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            hittingright = true;
            
        }else if(Input.GetKeyUp(KeyCode.LeftAlt)){
            yield return new WaitForSeconds(1);
            hittingright = false;
        }


        //if(v >= .5f && Input.touch)
        if(Input.GetKeyDown(KeyCode.T)){
            hittingright = true;
            currentShot = shotManager.topSpin;
            
        }else if(Input.GetKeyUp(KeyCode.T)){
            yield return new WaitForSeconds(1);
            hittingright = false;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            hittingLeft = true;
            currentShot = shotManager.topSpin;
            
        }else if(Input.GetKeyUp(KeyCode.R)){
            yield return new WaitForSeconds(1);
            hittingLeft = false;
        }


        //if(v <= -.5f && Input.touch)
        if(Input.GetKeyDown(KeyCode.G)){
            hittingright = true;
            currentShot = shotManager.flat;
            
        }else if(Input.GetKeyUp(KeyCode.G)){
            yield return new WaitForSeconds(1);
            hittingright = false;
        }

        if(Input.GetKeyDown(KeyCode.F)){
            hittingLeft = true;
            currentShot = shotManager.flat;
            
        }else if(Input.GetKeyUp(KeyCode.F)){
            yield return new WaitForSeconds(1);
            hittingLeft = false;
        }

        yield return null;
    }
        */

    }

    

