using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
    public Text rooms;
    public Text Students;
    public Text Score;

	// Use this for initialization
	void Start () {
        int roomsNum = Timemanager.Instance.roomsCleared+1;
        int studentsRescued = Timemanager.Instance.totalNumberOfAlum;

        int score =(int) (roomsNum * 100 + studentsRescued * 40 );


        rooms.text=roomsNum.ToString();
        Students.text = studentsRescued.ToString();
        Score.text = score.ToString();

	}
}
