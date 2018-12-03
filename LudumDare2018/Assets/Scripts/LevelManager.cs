using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public enum State {Intro,Animation, Playing, End}
    State state;
    [Header("Level Number")]
    public int currentLevel;
    public bool isLastLevel;

    [Header("Player")]
    public GameObject Character;
    public GameObject CharacterStartPosition;

    [Header("Canvas")]
    public Image fadeImg;

    float startTime;

    [Header("StartAnimation")]
    public float IntroDuration=4;
    public float fadeDuration=0.25f;
    Vector3 initialPlayerPos;


    [Header("EndAnimation")]
    public ParticleSystem particles;
    public GameObject endOfLevelGoal;
    public float endAnimTime;

    [Header("Menu")]
    public MenuManager menu;
    public GameObject PauseCanvas;

    [Header("AlumnPrefab")]
    public GameObject alumno;

    private void Reset()
    {
        gameObject.name = "Level Manager";
    }

    public void spawnAlumno(Vector3 position, Quaternion rotation)
    {
        Instantiate(alumno, position, rotation);
    }

    public Vector3 getInitialPos()
    {
        return CharacterStartPosition.transform.position;
    }

    public PlayerManager getCharacter()
    {
        return Character.GetComponent<PlayerManager>();
    }
    public int getLevelNumber()
    {
        return currentLevel;
    }

    void Awake()
    {
        state = State.Intro;
        Character.GetComponent<CircleCollider2D>().enabled = false;
        initialPlayerPos = Character.transform.position = transform.position;
        fadeImg.gameObject.SetActive(true);
        menu.currentLevel = currentLevel;
        PauseCanvas.SetActive(false);
        startTime = 0;
    }

    void startGameplay()
    {
        state = State.Playing;
        Character.GetComponent<CircleCollider2D>().enabled = true;
        Character.GetComponent<PlayerManager>().started = true;
        Debug.Log("start");
        
    }

    public void endLevel()
    {
        Character.GetComponent<PlayerManager>().started = false;
        Character.GetComponent<PlayerManager>().setVelocity(new Vector2(0, 0));
        particles.Play();
        startTime = 0;
        initialPlayerPos = Character.transform.position;
        fadeImg.color = new Color(0, 0, 0, 0);
        state = State.End;
    }


    public void Update()
    {
        switch (state)
        {
            case State.Intro:
                //activar la animacion del player
                startTime += Time.deltaTime;
                float timeProp = startTime / IntroDuration;
                Character.transform.position = Vector3.Lerp(initialPlayerPos,CharacterStartPosition.transform.position,timeProp);
                if (fadeImg.gameObject.activeInHierarchy) {
                    fadeImg.color = new Color(0, 0, 0, Mathf.Clamp(1 - (startTime / fadeDuration),0,1));
                    if (startTime >= fadeDuration)
                        fadeImg.gameObject.SetActive(false);
                }
                if (startTime > IntroDuration)
                {
                    startGameplay();
                }

                break;
            case State.Playing:
                if (Input.GetKeyDown("escape"))
                {
                    PauseCanvas.SetActive(!PauseCanvas.activeInHierarchy);
                }

                break;
            case State.End:
                startTime += Time.deltaTime;
                Character.transform.position = Vector3.Lerp(initialPlayerPos, endOfLevelGoal.transform.position, startTime / endAnimTime);
                if (!fadeImg.gameObject.activeInHierarchy)
                {
                    if (startTime > endAnimTime - fadeDuration*2)
                    {
                        fadeImg.gameObject.SetActive(true);
                    }
                }
                else
                {

                    fadeImg.color = new Color(0, 0, 0, Mathf.Clamp(1-(endAnimTime - startTime) / fadeDuration*2 , 0, 1));
                }
                if (startTime > endAnimTime)
                {
                    Timemanager.Instance.roomsCleared++;
                    Timemanager.Instance.totalNumberOfAlum += Character.GetComponent<PlayerManager>().numOfAlumn;
                    Timemanager.Instance.setCurrentTime(Character.GetComponent<PlayerManager>().getTimeLeft());
                    if (!isLastLevel) 
                        SceneManager.LoadScene("Level" + (currentLevel + 1).ToString());
                    else
                        SceneManager.LoadScene("Victory");
                }
                break;
            
        }
        

    }

    

}
