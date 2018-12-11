using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour {

    Player player;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw;
    public float pitch;

    public void OnInitialize()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        pitch = player.inhabited.transform.GetChild(3).transform.rotation.x;
        yaw = player.inhabited.transform.GetChild(3).transform.rotation.y;
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        if (pitch <= -89.0f)
        {
            pitch = -89.0f;
        }
        if (pitch >= 89.0f)
        {
            pitch = 89.0f;
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKeyDown("f2"))
        {
            Debug.Log("pitch: " + pitch + ", yaw: " + yaw);
            Debug.Log(player.inhabited.transform.GetChild(3) + 
                ", pitch: " + player.inhabited.transform.GetChild(3).transform.rotation.x + 
                ", yaw: " + player.inhabited.transform.GetChild(3).transform.rotation.y);
        }
    }
}
