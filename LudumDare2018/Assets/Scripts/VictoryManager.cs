using System.Collections;
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

        float score = roomsNum * 100 + studentsRescued * 20 + timeleft * 5;


        rooms.text=roomsNum.ToString();
        Students.text = studentsRescued.ToString();
        Time.text = timeleft.ToString();
        Score.text = score.ToString();

	}

}
