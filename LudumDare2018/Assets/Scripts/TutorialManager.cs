using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public TutorialStage[] TutorialStages;

    int animationsToPlay;
    int currentID;

    bool lastStage;
    // Use this for initialization
    void Start () {
        animationsToPlay = 1;
        currentID=-1;
        activateStage();
        lastStage = false;
	}

    void activateStage(){
        animationsToPlay--;
        currentID++;
        //currentID = Mathf.Clamp(currentID, 0, 1);
        for (int i = 0; i < TutorialStages.Length; i++)
        {
            TutorialStages[i].setActive(i == currentID);   
        }
        if (currentID >= TutorialStages.Length-1)
        {
            lastStage = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("return"))
        {
            if(!lastStage)
                animationsToPlay+=1;
        }
            if (!TutorialStages[currentID].isPlaying() && animationsToPlay > 0)
            {
            if(!lastStage)
                activateStage();
            }
        
        if(lastStage && !TutorialStages[currentID].isPlaying())
        {
            Timemanager.Instance.setMaxTime(1000);
            Timemanager.Instance.setCurrentTime(1000);
            SceneManager.LoadScene("Level0");
        }
	}

}
