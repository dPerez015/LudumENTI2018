using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTextController : MonoBehaviour {

    public Text alumnos;
    public Text level;

    GameObject levelManager;
    PlayerManager player;
	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager");
        int currentLevel=levelManager.GetComponent<LevelManager>().getLevelNumber();
        level.text = (currentLevel+1).ToString() + "/15";
        player = levelManager.GetComponent<LevelManager>().getCharacter();
	}
	
	// Update is called once per frame
	void Update () {
        alumnos.text = player.numOfAlumn.ToString();
	}
}
