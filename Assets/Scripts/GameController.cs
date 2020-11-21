using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public PlayerAction Player;
    public Transform ball;
    public Transform Canasta;
    public bool canScore;//Cuando si y cuando no se puede canastar

    static public bool scorelights = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (Player.holdingBall == false)
        {
            if (ball.position.y > Canasta.position.y && canScore == false)
            {
                
                canScore = true;
                Debug.Log("Puedes Canastar!");
                //scorelights = true;
            }
        }
    }
}
