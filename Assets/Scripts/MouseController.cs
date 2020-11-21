using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using System.IO.Ports;

public class MouseController : MonoBehaviour
{
    public float mouseSpeed = 500;
    public Transform cam;

    float mouseX;
    float mouseY;

    float yReal = 0.0f;//Para controlar la rotacion de la camara

    string[] analogData = null;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//Desaparece el cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        analogData = PlayerMovement.valores;
        if (analogData != null)
        {
            //2 y 3
            mouseX = int.Parse(analogData[2]);
            mouseY = int.Parse(analogData[3]);

            yReal -= (mouseY - 500) / 500;
            yReal = Mathf.Clamp(yReal, -90f, 90f);

            if ((-(mouseY - 500) / 500) > 0.1 || (-(mouseY - 500) / 500) < -0.1)
            {
                cam.localRotation = Quaternion.Euler(-yReal, 0f, 0f);
            }

            if ((-(mouseX - 500) / 500) > 0.1 || (-(mouseX - 500) / 500) < -0.1)
            {
                transform.Rotate(Vector3.up * -(mouseX - 500) / 300);
            }
        }
        else
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

            yReal -= mouseY;
            yReal = Mathf.Clamp(yReal, -90f, 90f);

            cam.localRotation = Quaternion.Euler(yReal, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
