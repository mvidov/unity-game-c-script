using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Player_Look : MonoBehaviour {

    public Transform playerBody;

    public float mouseSensitivity;

    float xAxisClamp = 0f;

    void Awake()
    {

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

    }

    void Update()
    {

        RotateCamera();

        if(scr_Pause_Menu.isPaused == true)
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

    }

    void RotateCamera()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity * Time.deltaTime;
        float rotAmountY = mouseY * mouseSensitivity * Time.deltaTime;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;

        xAxisClamp -= rotAmountY;

        if(xAxisClamp > 90)
        {

            xAxisClamp = 90;
            targetRotCam.x = 90;

        }

        else if(xAxisClamp < -90)
        {

            xAxisClamp = -90;
            targetRotCam.x = 270;

        }

        targetRotBody.y += rotAmountX;

        transform.rotation = Quaternion.Euler(targetRotCam);
        playerBody.rotation = Quaternion.Euler(targetRotBody);

    }

}
