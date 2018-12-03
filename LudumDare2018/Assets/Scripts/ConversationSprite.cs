using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationSprite : MonoBehaviour {

    public GameObject initialPos;
    public GameObject finalPos;

    public float movementDuration;

    float StartTime;
    bool playing;

    int appearingOrDisappearing;

    int currentID;

    public bool animationPlaying()
    {
        return playing;
    }

    public void appear()
    {
        appearingOrDisappearing = 1;
        StartTime = 0;
        playing = true;
    }

    public void disappear()
    {
        appearingOrDisappearing = 0;
        playing = true;
        StartTime = 0;
    }

	// Update is called once per frame
	void Update () {
        if (playing)
        {
            StartTime += Time.deltaTime;
            if (StartTime<=movementDuration)
            {
                transform.position = Vector3.Lerp(initialPos.transform.position, finalPos.transform.position,(appearingOrDisappearing - (StartTime/movementDuration))*((appearingOrDisappearing*2)-1));
            }
            else
            {
                playing = false;
            }
        }
	}
}
