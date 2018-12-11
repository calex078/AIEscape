using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScreen : MonoBehaviour {
    
    Player player;

    [SerializeField]
    GameObject noText;
    [SerializeField]
    GameObject defaultText;
    [SerializeField]
    GameObject fullText;
    bool firstTextView = false;
    bool fullTextDisplayed = false;

    [SerializeField]
    AudioClip beep;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void InhabitedOnStart()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        defaultText.SetActive(true);
        noText.SetActive(false);
        //Debug.Log("Startup Default Text " + firstTextView);
        //Debug.Log(player.inhabited);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && firstTextView == false && this.gameObject.layer == 8)
        {
            defaultText.SetActive(true);
            noText.SetActive(false);
            //Debug.Log("Default Text " + firstTextView);
        }
        else if(collider.gameObject.tag == "Player")
        {
            noText.SetActive(true);
            fullText.SetActive(false);
            defaultText.SetActive(false);
            //Debug.Log("No Default Text " + firstTextView);
        }
    }

    public void ObjectActivated()
    {
        if (player.inhabited == this.gameObject)
        {
            if (fullTextDisplayed == true)
            {
                noText.SetActive(true);
                fullText.SetActive(false);
                defaultText.SetActive(false);
                fullTextDisplayed = false;
                //Debug.Log("No Text");
            }
            else
            {
                fullText.SetActive(true);
                noText.SetActive(false);
                defaultText.SetActive(false);
                fullTextDisplayed = true;
                firstTextView = true;
                //Debug.Log("Full Text");
            }
        }
        else
        {
            this.GetComponent<AudioSource>().PlayOneShot(beep);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            noText.SetActive(true);
            fullText.SetActive(false);
            defaultText.SetActive(false);
            fullTextDisplayed = false;
            //Debug.Log("Text Reset");
        }
    }
}
