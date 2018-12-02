using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour {

    public float minScale=1;
    public float maxScale=1;

    Transform punta;

    public void setScale(float p)
    {
        transform.localScale=new Vector3(Mathf.Lerp(minScale,maxScale,p),1,1);
        punta.localScale = new Vector3(1/Mathf.Lerp(minScale, maxScale, p),1, 1);
    }

    public void SetActive(bool a)
    {
         gameObject.SetActive(a);
    }

	// Use this for initialization
	void Start () {
        punta = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
