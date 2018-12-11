using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject keyboardIndicator;
    [SerializeField]
    GameObject VRIndicator;
    [SerializeField]
    string startScene;
    [SerializeField]
    string vrTestScene;

    private void Start()
    {
        if (PlayerPrefs.GetInt("VR") == 1)
        {
            SelectVR();
        }
        else
        {
            SelectKeyboard();
        }
    }

    public void SelectKeyboard()
    {
        VRIndicator.SetActive(false);
        keyboardIndicator.SetActive(true);
        PlayerPrefs.SetInt("VR", 0);
    }

    public void SelectVR()
    {
        keyboardIndicator.SetActive(false);
        VRIndicator.SetActive(true);
        PlayerPrefs.SetInt("VR", 1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void VRTest()
    {
        SceneManager.LoadScene(vrTestScene);
    }
}
