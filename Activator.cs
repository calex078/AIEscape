using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public MonoBehaviour activateTarget;

    public void ActivateObject()
    {
        Debug.Log("Activated");
        activateTarget.gameObject.SendMessage("ObjectActivated");
    }

    public void OnStartLevel()
    {
        activateTarget.gameObject.SendMessage("InhabitedOnStart");
    }
}
