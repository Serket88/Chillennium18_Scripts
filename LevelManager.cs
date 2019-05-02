using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public RectTransform[] clocks;
    public RectTransform[] goScreens;
    public Text[] finalTime;
    public Text[] finalKills;
    public Text blueLife;
    public Text redLife;

    public GameObject pauseMenu;
    public GameObject displayContainer;
    public GameObject ceiling;

    public GameObject player;
    public GameObject leftControl;
    public GameObject rightControl;

    private float timer = 0.0f;
    private bool timeStarted = false;
    public bool isPaused = false;
    bool pauseLock = false;
    private string displayTime = "";


   // public List<AudioClip> hitclipslist;


    float EndTimer = 0.0f;

    private void Awake()
    {
        StaticDatabase.RedPlatform = 50f;
        StaticDatabase.BluePlatform = 50f;
        StaticDatabase.RedPopulation = 0;
        StaticDatabase.BluePopulation = 0;
        StaticDatabase.totalKilled = 0;
}

    // Use this for initialization
    void Start () {
        timeStarted = true;
        Time.timeScale = 1;
     
	}
    /*
    public void PlaySound()
    {
        AudioSource source = new AudioSource();
        AudioClip sel = hitclipslist[Random.Range(0, 11)];
        source.clip = sel;
        source.Play();
    } */
	
	// Update is called once per frame
	void Update () {
        if(timeStarted)
        {
            UpdateTime();
        }

        //Debug.Log("Blue life:  " + StaticDatabase.BluePlatform);
        //Debug.Log("Red life:  " + StaticDatabase.RedPlatform);

        blueLife.GetComponentInChildren<Text>().text = "" + StaticDatabase.BluePlatform;
        redLife.GetComponentInChildren<Text>().text = "" + StaticDatabase.RedPlatform;

        //IF EITHER PLATFORM DETROYED
        if (StaticDatabase.BluePlatform <= 0 || StaticDatabase.RedPlatform <= 0)
        {
            Time.timeScale = 0.0f;

            //  Disable all clocks
            foreach (RectTransform timeDisplay in clocks)
            {
                timeDisplay.gameObject.SetActive(false);
            }

            //  Enable all game over screens
            foreach(RectTransform finalDisplay in goScreens)
            {
                finalDisplay.gameObject.SetActive(true);
            }

            foreach(Text timeScore in finalTime)
            {
                timeScore.text = "" + displayTime;
            }

            foreach(Text killScore in finalKills)
            {
                killScore.text = "" + StaticDatabase.totalKilled;
            }

            leftControl.GetComponent<SpawnProjectile>().enabled = false;
            leftControl.transform.GetChild(2).gameObject.SetActive(false);
            leftControl.transform.GetChild(1).gameObject.SetActive(false);
            rightControl.GetComponent<SpawnProjectile>().enabled = false;
            rightControl.transform.GetChild(2).gameObject.SetActive(false);
            rightControl.transform.GetChild(1).gameObject.SetActive(false);

            leftControl.GetComponent<SteamVR_LaserPointer>().enabled = true;
            leftControl.GetComponent<VRUIInput>().enabled = true;
            rightControl.GetComponent<SteamVR_LaserPointer>().enabled = true;
            rightControl.GetComponent<VRUIInput>().enabled = true;

            //Debug.Log("You lost the game");
        }
	}

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateTime()
    {
        timer += Time.deltaTime;

        int d = (int)(timer * 100.0f);
        int minutes = d / (60 * 100);
        int seconds = (d % (60 * 100)) / 100;

        //LoseGame(seconds);

        int hundredths = d % 100;
        string newTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        displayTime = newTime;

        UpdateClocks(newTime);
    }

    void LoseGame(int seconds)
    {
        if (seconds >= 2)
        {
            StaticDatabase.BluePlatform = 0;
        }
    }

    void UpdateClocks(string newTime)
    {
        foreach (RectTransform timeDisplay in clocks) {
            timeDisplay.GetComponentInChildren<Text>().text = "" + newTime;
        }
    }

    public void PauseGame(object sender, ClickedEventArgs e)
    {
        if (pauseLock)
        {
            if (!isPaused)
            {
                //Debug.Log("We're fucking pausing");
                pauseMenu.GetComponent<Canvas>().enabled = true;
                pauseMenu.transform.parent = player.transform;
                Time.timeScale = 0.0f;
                isPaused = true;

                leftControl.GetComponent<SpawnProjectile>().enabled = false;
                leftControl.transform.GetChild(2).gameObject.SetActive(false);
                leftControl.transform.GetChild(1).gameObject.SetActive(false);
                rightControl.GetComponent<SpawnProjectile>().enabled = false;
                rightControl.transform.GetChild(2).gameObject.SetActive(false);
                rightControl.transform.GetChild(1).gameObject.SetActive(false);

                leftControl.GetComponent<SteamVR_LaserPointer>().enabled = true;
                leftControl.GetComponent<VRUIInput>().enabled = true;
                rightControl.GetComponent<SteamVR_LaserPointer>().enabled = true;
                rightControl.GetComponent<VRUIInput>().enabled = true;
            }
            else
            {
                //Debug.Log("pause unpause");
                Time.timeScale = 1;
                pauseMenu.transform.parent = player.transform.GetChild(2).GetChild(0).transform;
                pauseMenu.GetComponent<Canvas>().enabled = false;
                isPaused = false;
                //Debug.Log("pausegame set isPaused to " + isPaused);

                leftControl.GetComponent<SpawnProjectile>().enabled = true;
                rightControl.GetComponent<SpawnProjectile>().enabled = true;
                leftControl.GetComponent<SteamVR_LaserPointer>().enabled = false;
                leftControl.GetComponent<VRUIInput>().enabled = false;
                rightControl.GetComponent<SteamVR_LaserPointer>().enabled = false;
                rightControl.GetComponent<VRUIInput>().enabled = false;
            }
            pauseLock = false;
        }
    }

    public void UnPause()
    {
        //Debug.Log("unPause called");
        Time.timeScale = 1;
        pauseMenu.transform.parent = player.transform.GetChild(2).GetChild(0).transform;
        pauseMenu.GetComponent<Canvas>().enabled = false;
        isPaused = false;
        //Debug.Log("isPaused was set to " + isPaused);

        leftControl.GetComponent<SpawnProjectile>().enabled = true;
        rightControl.GetComponent<SpawnProjectile>().enabled = true;
        leftControl.GetComponent<SteamVR_LaserPointer>().enabled = false;
        leftControl.GetComponent<VRUIInput>().enabled = false;
        rightControl.GetComponent<SteamVR_LaserPointer>().enabled = false;
        rightControl.GetComponent<VRUIInput>().enabled = false;
    }

    public void LockPause(object sender, ClickedEventArgs e)
    {
        pauseLock = true;
    }
}
