using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public Transform balltransform;
    public bool bolafuera;
    public bool matchEnded;
    public bool matchStarted;
    public bool pausa;
    public float coordenadasBola;

    public int ptsRival = 0;
    public int ptsPlayer = 0;

    public Ball ballScript;

    AudioSource audioSource;
    public GameObject audioFXGO;
    public AudioFX audioFXscript;
    public GameObject playerGO;
    public Player playerScript;
    public GameObject rivalGO;
    public GameObject ballGO;
    
    public GameObject backgroundFinal;
    public Text txtMarcador;
    public Text txtResultado;


    public Vector3 posIniBola1 = new Vector3(-3.32f, 2.14f, -6.58f);
    public Vector3 posIniBola2 = new Vector3(-3.32f, 2.14f, -0.47f);
    public Vector3 posIniRival = new Vector3(-3.32f, 2.323f, -0.09f);
    public Vector3 posIniPlayer = new Vector3(-3.43f, 1.85f, -7.26f);
    

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindObjectOfType<Player>().gameObject;
        playerScript = playerGO.GetComponent<Player>();
        ballGO = GameObject.FindObjectOfType<Ball>().gameObject;
        ballScript = ballGO.GetComponent<Ball>();
        rivalGO = GameObject.Find("Rival");
        
        audioSource = GetComponent<AudioSource>();
        audioFXGO = GameObject.Find("AudioFX");
        audioFXscript = audioFXGO.GetComponent<AudioFX>();

        backgroundFinal = GameObject.Find("EndMatch");
        backgroundFinal.SetActive(false);

        Vector3 ballDir = balltransform.position;
        posIniPlayer = playerGO.transform.position;
        posIniRival = rivalGO.transform.position;

        //CuentaRegresiva();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(matchStarted && pausa == false){
            
            DetectarBola();
            PauseControl();
        }
        
        
      
    }

    public void CuentaRegresiva(){
        matchStarted = true;
    }


        
        

    public void DetectarBola(){
        //De que lado cayó la bola? Checar con eje Z
        //bolafuera = true;
        //Checar también si se detuvo en la mesa con la red
        ptsPlayer = ballScript.ptsP;
        ptsRival = ballScript.ptsR;
        if(ptsPlayer >= 6 || ptsRival >=6){
            MatchEnd();
            
        }
        if(!matchEnded){
            MarcarPunto();
        }
        

    }

    void MarcarPunto(){
        //UI
        
        txtMarcador.text = ptsPlayer.ToString() + " - " + ptsRival.ToString();

        
    }

    void MatchEnd(){
        //audioFXscript.BallDrop();

        //Stop Game
        //Time.timeScale = 0;
       if(ptsPlayer > ptsRival){
           txtResultado.text = "Ganaste";
       }else if(ptsPlayer < ptsRival){
           txtResultado.text = "Perdiste";
       }else if(ptsPlayer == ptsRival){
           txtResultado.text = "Empate";
       }
        backgroundFinal.SetActive(true);
        matchEnded = true;
        ballScript.DisableRagdoll();
        
        //PauseGame();

    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
        pausa = true;
        //playerScript.wall = true;
    }

    public void ResumeGame ()
    {
            Time.timeScale = 1;
            //playerScript.wall = false;
        
    }

    public void PauseControl(){
        if (Input.GetKeyDown(KeyCode.O)){
            if(Time.timeScale == 1)
             {
                 PauseGame();
             }
             else
             {
                 ResumeGame();
             }  
        }
    }

}
