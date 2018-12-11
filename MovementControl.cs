using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MovementControl : MonoBehaviour
{

    Game game;
    [SerializeField]
    GameObject moveController;
    Player player;
    MouseCam mouseCam;
    public GameObject moveTarget;
    public GameObject moveSelected;
    //public GameObject inhabited;
    GameObject prevMoveSelected;
    public bool moveAllowed = true;
    int inhabitLayer = 1 << 10;
    int wallLayer = 1 << 13;
    public Component moveHalo;
    public bool moveGlowActive = false;

    void OnEnable()
    {
        game = GameObject.FindWithTag("GameController").GetComponent<Game>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        mouseCam = GameObject.FindWithTag("Player").GetComponent<MouseCam>();
        //inhabited = game.startObject;
        //inhabited.GetComponent<Activator>().OnStartLevel();
        moveSelected = game.voidObject;
        /*if (game.vr == 1)
        {
            moveController = player.transform.GetChild(1).GetChild(1).gameObject;
        }
        else
        {
            moveController = player.gameObject;
            mouseCam.OnInitialize();
        }*/
    }

    void Update()
    {
        RaycastHit moveHit;
        if (Physics.Raycast(moveController.transform.position, transform.TransformDirection(Vector3.forward), out moveHit, Mathf.Infinity, inhabitLayer))
        {
            if (Physics.Linecast(moveController.transform.position, moveHit.transform.position, wallLayer))
            {
                Debug.Log("Object obstructed");
            }
            else
            {
                /*Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
                Debug.Log("hit " + hit.transform.gameObject);*/
                moveTarget = moveHit.transform.gameObject;
                moveHalo = moveTarget.transform.gameObject.GetComponent("Halo");
                if (moveGlowActive == false)
                {
                    moveHalo.GetType().GetProperty("enabled").SetValue(moveHalo, true, null);
                    moveGlowActive = true;
                }

                if (moveTarget != null && moveAllowed == true && (player.VRMovement != true && Input.GetMouseButtonDown(0)) || (player.VRMovement == true && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)))
                {
                    if (moveTarget.gameObject.tag == "Inhabitable")
                    {
                        prevMoveSelected = moveSelected;
                        moveSelected = moveTarget;
                        if (prevMoveSelected != null)
                        {
                            prevMoveSelected.transform.GetChild(2).gameObject.SetActive(false);
                        }
                        moveSelected.transform.GetChild(2).gameObject.SetActive(true);
                        Debug.Log("moveSelected = " + moveSelected);
                    }
                }
            }
        }
        else if (moveGlowActive == true)
        {
            moveHalo.GetType().GetProperty("enabled").SetValue(moveHalo, false, null);
            moveGlowActive = false;
        }
    }
}
