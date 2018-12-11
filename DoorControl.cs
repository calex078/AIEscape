using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {

    public GameObject door;
    [SerializeField]
    Animator m_Animator;
    [SerializeField]
    bool doorOpen;
    [SerializeField]
    AudioClip beep;

    private void Start()
    {
        if (doorOpen == true)
        {
            m_Animator.SetBool("Opened", true);
            door.layer = 0;
        }
    }

    public void ObjectActivated()
    {
        doorOpen = m_Animator.GetBool("Opened");
        if (doorOpen == true)
        {
            doorOpen = false;
            m_Animator.SetBool("Opened", false);
            door.layer = 13;
        }
        else
        {
            doorOpen = true;
            m_Animator.SetBool("Opened", true);
            door.layer = 0;
        }
        Debug.Log("Door opened = " + doorOpen);
        this.GetComponent<AudioSource>().PlayOneShot(beep);
    }
}
