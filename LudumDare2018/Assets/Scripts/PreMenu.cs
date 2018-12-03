using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreMenu : MonoBehaviour {

    public GameObject slimesensei, fire1, fire2, fire3;
    public Image fadeBlanco;
    float fade = 0;
    float timeFade = 0;
    float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 2 && timer < 4)
        {
            fire1.SetActive(true);
        }
        if (timer > 4 && timer < 5)
        {
            fire2.SetActive(true);
        }
        if (timer > 5 && timer < 7)
        {
            fire3.SetActive(true);
        }
        if (timer > 7 && timer < 11)
        {

            slimesensei.transform.position += new Vector3(1, 1) * Time.deltaTime * 150;
        }
        if (timer > 9)
        {
            fadeBlanco.color = new Color(1,1,1,timeFade/1);
            timeFade += Time.deltaTime;
            if(timeFade >= 1)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

    }
}
