using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

    public GameObject levelManager;

    private bool isPaused;
    private bool hasFlipped = false;

    bool delayCheck;
    bool pauseCheck;

    public Transform redBase;
    public Transform blueBase;
    public Transform currentBase;

    SteamVR_TrackedController controller;

	// Use this for initialization
	void Awake ()
    {
        redBase = GameObject.Find("PlayerPointRed").transform;
        blueBase = GameObject.Find("PlayerPointBlue").transform;
        currentBase = blueBase;
        delayCheck = false;
        pauseCheck = false;
        controller = GetComponent<SteamVR_TrackedController>();

        GameObject.Find("[CameraRig]").transform.position = blueBase.position;
        GameObject.Find("[CameraRig]").transform.rotation = blueBase.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        PauseCheck(controller);
        DimensionShiftCheck(controller);
    }

    IEnumerator DelayLock()
    {
        delayCheck = true;
        yield return new WaitForSeconds(0.5f);
        delayCheck = false;
    }
    
    void DimensionShiftCheck(SteamVR_TrackedController controller)
    {
        
        if(controller != null && controller.gripped && !delayCheck && levelManager.GetComponent<LevelManager>().isPaused == false)
        {
            Debug.Log("Grip pressed");

            //If on red switch to blue
            if (currentBase == redBase)
            {
                GameObject.Find("[CameraRig]").transform.position = blueBase.position;
                GameObject.Find("[CameraRig]").transform.rotation = blueBase.rotation;
                currentBase = blueBase;
            }
            //If on blue switch to red
            else if(currentBase == blueBase)
            {
                GameObject.Find("[CameraRig]").transform.position = redBase.position;
                GameObject.Find("[CameraRig]").transform.rotation = redBase.rotation;
                currentBase = redBase;
            }


            if (currentBase == redBase)
            {
                levelManager.GetComponent<LevelManager>().displayContainer.transform.rotation = new Quaternion(-180, 0, 0, 0);
                levelManager.GetComponent<LevelManager>().ceiling.transform.position = new Vector3(3.1f, 5.5f, -2.43f);
               
            }
            else
            {
                levelManager.GetComponent<LevelManager>().displayContainer.transform.rotation = new Quaternion(0, 0, 0, 0);
                levelManager.GetComponent<LevelManager>().ceiling.transform.position = new Vector3(3.1f, 19.15f, -2.43f);
               
            }
            

            StartCoroutine("DelayLock");
        }
    } 
    

    void PauseCheck(SteamVR_TrackedController controller)
    {
        controller.MenuButtonClicked += levelManager.GetComponent<LevelManager>().LockPause;
        controller.MenuButtonUnclicked += levelManager.GetComponent<LevelManager>().PauseGame;
    }

   
}
