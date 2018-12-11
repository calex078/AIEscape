using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInhabit : MonoBehaviour {

    Player player;
    MovementControl movementControl;
    UseControl useControl;
    Component halo;
    GameObject body;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        movementControl = GameObject.FindWithTag("MoveControl").GetComponent<MovementControl>();
        useControl = GameObject.FindWithTag("UseControl").GetComponent<UseControl>();
        halo = this.transform.gameObject.GetComponent("Halo");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && this.gameObject.layer != 8)
        {
            //Debug.Log("in");
            this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && this.gameObject.layer == 8)
        {
            StartCoroutine(InhabitWait());
            //Debug.Log("out");
        }
    }

    IEnumerator InhabitWait()
    {
        yield return new WaitForSeconds(0.5f);
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
        this.gameObject.layer = 10;
        this.transform.GetChild(0).gameObject.layer = 10;
        if(useControl.useSelected == player.inhabited)
        {
            useControl.useSelected.transform.GetChild(1).gameObject.SetActive(true);
        }
        player.inhabited = movementControl.moveTarget;
        movementControl.moveTarget = null;
        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
}
