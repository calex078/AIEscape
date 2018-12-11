using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBeep : MonoBehaviour {

    [SerializeField]
    AudioClip beep;

    public void ObjectActivated()
    {
        this.GetComponent<AudioSource>().PlayOneShot(beep);
    }

    public void InhabitedOnStart()
    {
        
    }
}
