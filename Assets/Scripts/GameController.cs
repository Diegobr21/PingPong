using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject ballGO;
    public GameObject playerGO;
    public GameObject rivalGO;
    public bool bolafuera;
    public float coordenadasBola;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindObjectOfType<Player>().gameObject;
        ballGO = GameObject.FindObjectOfType<Ball>().gameObject;
        rivalGO = GameObject.Find("Rival");
    }

    // Update is called once per frame
    void Update()
    {
        DetectarBola();
        ResetPosition();
    }

    void ResetPosition(){

        if(bolafuera){
            
            float coordBola = DetectarBola();
            //Resetear rival, bola, y jugador
            // Llamada a Marcar Puntos
            MarcarPunto();
        }
        
        
        
    }

    public float DetectarBola(){
        //De que lado cayó la bola? Checar con eje Z
        //Checar también si se detuvo en la mesa con la red
        

        return coordenadasBola;

    }

    void MarcarPunto(){
        //UI

    }
}
