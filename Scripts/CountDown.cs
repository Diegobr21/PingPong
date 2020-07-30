using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public GameObject TableGO;
    public GameController gameControllerScript;
    public Sprite[] numeros;
    public GameObject ContadorNum;
    public SpriteRenderer contadorNumSpriteRenderer;

    public GameObject BallGO;
    public Ball ballScript;
    // Start is called before the first frame update
    void Start()
    {
        Inicializar();
        Conteo();
        
    }
    void Inicializar(){
        TableGO = GameObject.Find("Table");
        gameControllerScript = TableGO.GetComponent<GameController>();
        ContadorNum = GameObject.Find("ContadorNum");
        contadorNumSpriteRenderer = ContadorNum.GetComponent<SpriteRenderer>();
        BallGO = GameObject.Find("Ball");
        ballScript = BallGO.GetComponent<Ball>();
    }

    void Conteo(){
        StartCoroutine(ConteoRegresivo());
        
    }

    IEnumerator ConteoRegresivo(){
       // gameControllerScript.pausa = true;

        yield return new WaitForSeconds (1);

        contadorNumSpriteRenderer.sprite = numeros[1];
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds (1);

        contadorNumSpriteRenderer.sprite = numeros[2];
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds (1);

        contadorNumSpriteRenderer.sprite = numeros[3];
        //gameControllerScript.pausa = false;
        ContadorNum.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds (.5f);
        gameControllerScript.matchStarted = true;
        ballScript.EnableRagdoll();


        ContadorNum.SetActive(false);
    }
}
