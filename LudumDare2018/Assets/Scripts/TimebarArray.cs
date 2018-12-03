using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimebarArray : MonoBehaviour {

    public GameObject[] bars;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setHealthPercent(float t)
    {
        float id = bars.Length-Mathf.Ceil(t);
        for (int i = 0; i < id; i++)
            bars[i].SetActive(false);
        if (id < bars.Length)
            bars[(int)id].GetComponent<Timebar>().setHealthPercent(t - Mathf.Ceil(t - 1));
        else
        {
            bars[bars.Length - 1].SetActive(false);
        }

    }
    public void showHealthLossPercent(float t)
    {
        float id = bars.Length - Mathf.Ceil(t);
        
        if (id < bars.Length)
            bars[(int)id].GetComponent<Timebar>().showHealthLossPercent(t - Mathf.Ceil(t - 1));

    }

    public int getNumberOfBars()
    {
        return bars.Length;
    }
}
