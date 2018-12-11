using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseControl : MonoBehaviour
{

    Game game;
    [SerializeField]
    GameObject useController;
    Player player;
    public GameObject useTarget;
    public GameObject useSelected;

    GameObject prevUseSelected;
    public bool moveAllowed = true;
    int interactLayer = 1 << 12 | 1 << 10;
    int wallLayer = 1 << 13;
    public Component useHalo;
    public bool useGlowActive = false;

    void OnEnable()
    {
        game = GameObject.FindWithTag("GameController").GetComponent<Game>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        useSelected = game.voidObject;
        /*if (game.vr == 1)
        {
            useController = player.transform.GetChild(1).GetChild(0).gameObject;
        }
        else
        {
            useController = player.gameObject;
        }*/
    }

    void Update()
    {
        RaycastHit useHit;
        if (Physics.Raycast(useController.transform.position, transform.TransformDirection(Vector3.forward), out useHit, Mathf.Infinity, interactLayer))
        {
            if (Physics.Linecast(useController.transform.position, useHit.transform.position, wallLayer))
            {
                Debug.Log("Object obstructed");
            }
            else
            {
                /*Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
                Debug.Log("hit " + hit.transform.gameObject);*/
                useTarget = useHit.transform.gameObject;
                useHalo = useTarget.transform.gameObject.GetComponent("Halo");
                if (useGlowActive == false)
                {
                    useHalo.GetType().GetProperty("enabled").SetValue(useHalo, true, null);
                    useGlowActive = true;
                }

                if (useTarget != null && (player.VRMovement != true && Input.GetMouseButtonDown(1)) || (player.VRMovement == true && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)))
                {
                    prevUseSelected = useSelected;
                    useSelected = useTarget;
                    if (prevUseSelected != null)
                    {
                        prevUseSelected.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    useSelected.transform.GetChild(1).gameObject.SetActive(true);
                    Debug.Log("useSelected = " + useSelected);
                }
            }
        }
        else if (useGlowActive == true)
        {
            useHalo.GetType().GetProperty("enabled").SetValue(useHalo, false, null);
            useGlowActive = false;
        }
    }
}
