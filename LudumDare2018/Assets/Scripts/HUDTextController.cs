using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTextController : MonoBehaviour {

    Text alumnos;
    Text level;

    GameObject levelManager;
	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager");
        levelManager.GetComponent<LevelManager>().getLevelNumber();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
