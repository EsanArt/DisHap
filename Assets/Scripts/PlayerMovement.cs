using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using System.IO.Ports;
using System;

public class PlayerMovement : MonoBehaviour
{
    //Cargando el lector del ARDUINO
    public static SerialPort serialPort = new SerialPort("COM3", 9600);

    public CharacterController controller;
    public float speed = 12f;

    float x;
    float z;

    Vector3 move;//Para la posicion

    //Gravedad
    Vector3 velocity;
    public float gravity = -15f;

    //Detectar si se esta tocando el suelo
    public Transform groundCheck;//Pies
    float radius = 0.4f;//Se detecta mediante un radio
    public LayerMask mask;//El layer del suelo
    bool isGrounded = false;//Si estamos tocando el suelo o no

    public float jumpForce = 1f;

    public static string[] valores;

    // Update is called once per frame
    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 10;
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, radius, mask);
        //print(serialPort);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }

        if (serialPort.IsOpen)
        {

            if (GameController.scorelights)
            {
                serialPort.Write("ON");
                GameController.scorelights = false;

            }
            string s;
            s = serialPort.ReadTo("\n");
            valores = s.Split(' ');
            x = int.Parse(valores[0]);
            z = int.Parse(valores[1]);
            
            if (((x - 510) / 500f) > 1 || ((x - 510) / 500f) < 1)
            {
                move = transform.right * -(x-510)/500f + transform.forward * -(z-528)/500f;
                controller.Move(move * speed * Time.deltaTime);
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);

            }
        
            
        }
        else
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        
            move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);


        
            // velocity.y += gravity * Time.deltaTime;
            // controller.Move(velocity * Time.deltaTime);
        }

        //Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
