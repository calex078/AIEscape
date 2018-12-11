using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Core variables
    Game game;
    [SerializeField]
    MovementControl movementControl;
    [SerializeField]
    UseControl useControl;
    GameObject player;
    public GameObject inhabited;
    MouseCam mouseCam;

    //VR Managers
    [SerializeField]
    GameObject reticle;
    public bool VRMovement = false;

    void OnEnable()
    {
        //Activate VR if available
        player = GameObject.FindWithTag("Player");
        game = GameObject.FindWithTag("GameController").GetComponent<Game>();
        movementControl = GameObject.FindWithTag("MoveControl").GetComponent<MovementControl>();
        useControl = GameObject.FindWithTag("UseControl").GetComponent<UseControl>();
        mouseCam = GameObject.FindWithTag("Player").GetComponent<MouseCam>();
        if (game.vr == 1)
        {
            VRMovement = true;
            reticle.SetActive(false);
        }
        else
        {
            VRMovement = false;
        }
        inhabited = game.startObject;
        Debug.Log("Started at " + inhabited);
        inhabited.GetComponent<Activator>().OnStartLevel();
        Debug.Log("VRMovement " + VRMovement);
    }

    void Update()
    {
        if (inhabited != null && (VRMovement != true && Input.GetKeyDown("space")) || (VRMovement == true && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)))
        {
            if (movementControl.moveSelected != game.voidObject)
            {
                StartCoroutine(PlayerMove());
            }
            else
            {
                Debug.Log("Cannot move to void object");
            }
        }

        if (inhabited != null && useControl.useSelected != inhabited && (VRMovement != true && Input.GetKeyDown("tab")) || (VRMovement == true && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)))
        {
            if (useControl.useSelected != game.voidObject)
            {
                useControl.useSelected.GetComponent<Activator>().ActivateObject();
                //Debug.Log("Using " + useSelected);
            }
            else
            {
                Debug.Log("Cannot use void object");
            }
        }

        if (inhabited != null && (VRMovement != true && Input.GetKeyDown("e")) || (VRMovement == true && OVRInput.GetDown(OVRInput.Button.One)))
        {
            inhabited.GetComponent<Activator>().ActivateObject();
        }

        if (Input.GetKeyDown("f1"))
        {
            Debug.Log("======================");
            Debug.Log("VR = " + game.vr);
            Debug.Log("moveSelected = " + movementControl.moveSelected + ", useSelected = " + useControl.useSelected + ", inhabited = " + inhabited);
            Debug.Log("======================");
        }
    }

    IEnumerator PlayerMove()
    {
        Debug.Log("Move Start");

        movementControl.moveAllowed = false;
        movementControl.moveSelected.gameObject.layer = 8;
        movementControl.moveSelected.transform.GetChild(0).gameObject.layer = 8;
        movementControl.moveHalo.GetType().GetProperty("enabled").SetValue(movementControl.moveHalo, false, null);

        yield return new WaitForSeconds(1);

        player.transform.position = movementControl.moveSelected.transform.GetChild(3).transform.position;
        player.transform.rotation = movementControl.moveSelected.transform.GetChild(3).transform.rotation;
        mouseCam.pitch = movementControl.moveSelected.transform.GetChild(3).transform.rotation.x;
        mouseCam.yaw = movementControl.moveSelected.transform.GetChild(3).transform.rotation.y;
        movementControl.moveSelected.transform.GetChild(2).gameObject.SetActive(false);

        Debug.Log("Now at " + movementControl.moveSelected.transform.gameObject.name);
        if (movementControl.moveSelected == useControl.useSelected && useControl.useSelected != null)
        {
            useControl.useSelected.transform.GetChild(1).gameObject.SetActive(false);
        }
        movementControl.moveAllowed = true;
        //Debug.Log("Move Complete");
    }
}