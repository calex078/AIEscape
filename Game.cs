using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Game : MonoBehaviour {

    private static Game singleton;
    
    GameObject player;
    public int vr = 0;
    
    public GameObject startObject;
    public GameObject voidObject;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        player = GameObject.FindWithTag("Player");
        //vr = PlayerPrefs.GetInt("VR", 0);

        /*if (vr == 1)
        {
            StartCoroutine(LoadDevice("VR"));
            player.GetComponent<MouseCam>().enabled = false;
            player.transform.GetChild(0).gameObject.SetActive(false);
            player.transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log("VR ON");
        }
        else
        {
            Debug.Log("VR OFF");
        }*/

        Cursor.lockState = CursorLockMode.Locked;
        
        player.transform.position = startObject.transform.position;
        startObject.gameObject.layer = 8;
        startObject.transform.GetChild(0).gameObject.layer = 8;
        startObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadDevice(string OpenVR)
    {
        if (string.Compare(XRSettings.loadedDeviceName, OpenVR, true) != 0)
        {
            XRSettings.LoadDeviceByName(OpenVR);
            yield return null;
            XRSettings.enabled = true;

            Debug.Log("VR Loaded");
        }
    }
}
