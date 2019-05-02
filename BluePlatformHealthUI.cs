using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BluePlatformHealthUI : MonoBehaviour {


    public TextMeshPro ui;
    public TextMeshPro ui_2;
	// Use this for initialization
	void Start () {
        ui = GetComponent<TextMeshPro>();
        ui_2 = GameObject.Find("B_HealthUI").GetComponent<TextMeshPro>();
        
		
	}
	
	// Update is called once per frame
	void Update () {
        ui.text = StaticDatabase.RedPlatform.ToString();
        ui_2.text = StaticDatabase.BluePlatform.ToString();
		
	}
}
