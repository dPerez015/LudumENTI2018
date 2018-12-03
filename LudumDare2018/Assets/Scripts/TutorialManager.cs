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
        for (int i = 0; i < TutorialStages.Length; i++)
        {
            TutorialStages[i].setActive(i == currentID);   
        }
        if (currentID == TutorialStages.Length-1)
        {
            lastStage = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("return"))
            animationsToPlay++;

        if (!TutorialStages[currentID].isPlaying() && animationsToPlay>0)
        {
            activateStage();
        }
        if(lastStage && !TutorialStages[currentID].isPlaying())
        {
            SceneManager.LoadScene("SampleScene");
        }
	}

}
