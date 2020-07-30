using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 initialPos;
    Vector3 playerPointPos;

    public AudioSource tableAudioSource;
        
    public Rigidbody rb;
    public Vector3 lastPositionBola;

    public int counter;
    public bool playerServing;
    public bool matchEnd;

    public GameController gameControllerScript;
    public GameObject TableGO;
    public GameObject PlayerGO;
    public Player playerScript;
    public GameObject RivalGO;
    public Rival rivalScript;
    public GameObject audioFXGO;
    public AudioFX audioFXscript;

    public int ptsP;
    public int ptsR;
    public string hitter;

    public bool surface; //true = wall / false = player

    // Start is called before the first frame update
    void Start()
    {

        initialPos = transform.position;
        
        gameControllerScript = TableGO.GetComponent<GameController>();

        rivalScript = RivalGO.GetComponent<Rival>();

        playerScript = PlayerGO.GetComponent<Player>();

        playerPointPos = new Vector3(-3.32f, 2.8f, -6.66f);

        audioFXGO = GameObject.Find("AudioFX");

        audioFXscript = audioFXGO.GetComponent<AudioFX>();
        
        rb = GetComponent<Rigidbody>();

        DisableRagdoll();

        //gameObject.SetActive(false);


        
    
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void EnableRagdoll() {
         rb.isKinematic = false;
         rb.detectCollisions = true;
     }
    public void DisableRagdoll() {
         rb.isKinematic = true;
         rb.detectCollisions = false;
     }
    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Wall") || other.transform.CompareTag("Net")){
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            lastPositionBola = transform.position;

            if(gameControllerScript.matchStarted){
                WhoScored(lastPositionBola);
            }
              
        }
    }

    private void OnCollisionExit(Collision other) {
        if(playerServing){
            playerServing = false;
            playerScript.Serve();
        }
    }
    
    public void WhoScored(Vector3 lastPB){
        // false - rival scored
        // true - player scored

        //ptP = (-3.40+)
        audioFXscript.Point();
        int sideBall = 0;
        if(lastPB.z > -3.45){
           ptsP += 1;
        }else if (lastPB.z < -4){
            ptsR += 1;
            sideBall = 1;
        }else{
            //No point 
        }

        StartCoroutine(ReturnPosition(sideBall));
    }

    IEnumerator ReturnPosition(int side){

        if(side == 0){
            
            playerServing = true;
            playerScript.Serve();
            transform.position = playerPointPos;
            
            
            //transform.position = initialPos;

        }else if(side == 1){
            
            rivalScript.ResetPosition();
            transform.position = initialPos;
        }

        yield return new WaitForSeconds(1);
        //transform.position = initialPos;

        
    }


}
