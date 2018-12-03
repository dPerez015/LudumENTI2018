using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timebar : MonoBehaviour {
    public Image health;
    public Image healthLoss;

    float healthLossObjective;
    float healthLossCurrent;

    float startLifeLossTime;
    
    public void setHealthPercentInstant(float p)
    {
        health.fillAmount = p;
        healthLoss.fillAmount = p;
    }
    

    public void setHealthPercent(float p)
    {
        health.fillAmount = p;
        healthLossObjective = p;
        healthLossCurrent = healthLoss.fillAmount;
        startLifeLossTime = 0;
    }

    private void Start()
    {
        healthLossObjective = 1;
        healthLossCurrent = 1;
    }

    private void Update()
    {
        startLifeLossTime += Time.deltaTime;
        healthLoss.fillAmount=Mathf.Lerp(healthLossCurrent, healthLossObjective, Mathf.Clamp(startLifeLossTime, 0, 1));
    }

    public void showHealthLossPercent(float p)
    {
        health.fillAmount = p;
    }

}
