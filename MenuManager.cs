using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject instructionMenu;

    Scene main;

    private bool mainActive = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void InstructionSwap()
    {
        if (mainActive)
        {
            // disable main, activate instructions
            mainMenu.SetActive(false);
            instructionMenu.SetActive(true);
            mainActive = false;
        } else
        {
            // disable instructions, activate main
            instructionMenu.SetActive(false);
            mainMenu.SetActive(true);
            mainActive = true;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }
}
