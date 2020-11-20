using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public PlayerAction player;
    public Transform ball;
    public Transform canasta;
    public bool canScore;

    static public bool scorelights = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.holdingBall == false)
        {
            if (ball.position.y > canasta.position.y && canScore == false)
            {
                
                canScore = true;
                Debug.Log("Puedes Canastar!");
                //scorelights = true;
            }
        }
    }
}
