using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnSceneExit : MonoBehaviour {

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroyOnSceneExit");
        if (objs.Length > 1) Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
