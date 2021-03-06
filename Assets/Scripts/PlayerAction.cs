﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class PlayerAction : MonoBehaviour
{
    public GameObject ball;//Para acceder a todas las propiedades
    SphereCollider ballCollider;
    TrailRenderer ballTrail;
    public Transform cam;
    public float ballDistance = 2f;
    public float ballForceMin = 150f;
    public float ballForceMax = 100000f;
    public float ballForce;
    public float totalTimer = 3f;
    
    //float currentTime = 0.0f;


    public bool holdingBall = true;
    Rigidbody ballRB;

    
    bool isPickableBall = false;
    public CanvasController canvasScript;//Accediendo al script desde el inspector
    public LayerMask pickableLayer;
    RaycastHit hit;//"Rayos" que se lanzan para detectar una colision

    string[] datoBoton = null;
    int boton;
    int boton2;

    // Start is called before the first frame update
    void Start()
    {
        ballRB = ball.GetComponent<Rigidbody>();
        ballCollider = ball.GetComponent<SphereCollider>();
        ballTrail = ball.GetComponent<TrailRenderer>();
        ballTrail.enabled = false;
        ballRB.useGravity = false;

        canvasScript.OcultarCursor(true);
        canvasScript.ActivarSlider(false);

        ballCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        datoBoton = PlayerMovement.valores;
        if (datoBoton != null)
        {
            boton = int.Parse(datoBoton[4]);
            boton2 = int.Parse(datoBoton[5]);
        }

        if (holdingBall == true)
        { 
            if (Input.GetMouseButton(0) || boton == 1)
            {
                //currentTime = 0.0f;
                canvasScript.SetValueBar(0);
                canvasScript.ActivarSlider(true);

                holdingBall = false;
                ballCollider.enabled = true;
                ballRB.useGravity = true;
                ballRB.AddForce(cam.forward * 400);
                canvasScript.OcultarCursor(false);
                canvasScript.ActivarSlider(false);
                ballTrail.enabled = true;
            }
        }
        else//Cuando no se tiene la pelota recogida
        {
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, pickableLayer))
            {
                if (isPickableBall == false)
                {
                    isPickableBall = true;
                    canvasScript.ChangePickableBallColor(true);
                }
                if ((isPickableBall) && (Input.GetKeyDown(KeyCode.E) || boton2 == 1))
                {
                    holdingBall = true;
                    ballCollider.enabled = false;
                    ballRB.useGravity = false;
                    ballRB.velocity = Vector3.zero;
                    ballRB.angularVelocity = Vector3.zero;
                    ball.transform.localRotation = Quaternion.identity;

                    GameController.instance.canScore = false;

                    canvasScript.ChangePickableBallColor(true);
                    canvasScript.OcultarCursor(true);
                    ballTrail.enabled = false;
                }
            }
            else if (isPickableBall == true)//Cuando no se este tocando la pelota con el raycast
            {
                isPickableBall = false;
                canvasScript.ChangePickableBallColor(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerMovement.serialPort.Close();
            SceneManager.LoadScene(0);
        }

    }

    private void LateUpdate()
    {
        if (holdingBall == true)
        {
            ball.transform.position = cam.position + cam.forward * ballDistance;
        }
    }
}
