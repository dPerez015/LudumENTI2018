using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage : MonoBehaviour {

    public GameObject[] appearSprites;
    public GameObject[] disappearSprites;

    public void setActive(bool a)
    {
        if (a)
        {
            foreach(GameObject obj in appearSprites)
            {
                obj.GetComponent<ConversationSprite>().appear();
            }
            foreach(GameObject obj in disappearSprites)
            {
                obj.GetComponent<ConversationSprite>().disappear();
            }
        }
        gameObject.SetActive(a);
    }

    public bool isPlaying()
    {
        foreach (GameObject obj in appearSprites)
        {
            if (obj.GetComponent<ConversationSprite>()!=null)
                if(obj.GetComponent<ConversationSprite>().animationPlaying())
                    return true; 
        }
        foreach (GameObject obj in disappearSprites)
        {
            if (obj.GetComponent<ConversationSprite>() != null)
                if (obj.GetComponent<ConversationSprite>().animationPlaying())
                    return true;
        }
        return false;
    }



}
