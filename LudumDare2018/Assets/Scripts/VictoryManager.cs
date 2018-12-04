﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour {

    public Text rooms;
    public Text Students;
    public Text Time;
    public Text Score;

	// Use this for initialization
	void Start () {
        int roomsNum = Timemanager.Instance.roomsCleared+1;
        int studentsRescued = Timemanager.Instance.totalNumberOfAlum;
        float timeleft = Timemanager.Instance.getCurrentTime();

        int score =(int) (roomsNum * 100 + studentsRescued * 40 + timeleft * 2);


        rooms.text=roomsNum.ToString();
        Students.text = studentsRescued.ToString();
        Time.text = timeleft.ToString();
        Score.text = score.ToString();

	}

}
