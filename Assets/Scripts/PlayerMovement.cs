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

    Vector3 move;

    //Gravedad
    Vector3 velocity;
    public float gravity = -15f;

    public Transform groundCheck;
    float radius = 0.4f;
    public LayerMask mask;
    bool isGrounded = false;

    public float jumpForce = 1f;

    public static string[] valores;

    // Update is called once per frame
    void Start()
    {
        serialPort.Open();
        //serialPort.ReadTimeout = 100;
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
            string s;
            s = serialPort.ReadTo("\n");
            valores = s.Split(' ');
            x = int.Parse(valores[0]);
            z = int.Parse(valores[1]);
        
            move = transform.right * -(x-510)/1000f + transform.forward * (z-528)/1000f;
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        
            move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);


        
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }
    }
}
