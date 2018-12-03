using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timemanager  {

    private static readonly Timemanager instance=new Timemanager();


    float maxTime=100;
    float currentTime=100;

    static Timemanager()
    {

    }

    private Timemanager()
    {

    }

    public static Timemanager Instance
    {
        get{ return instance; }
    }

    public void setMaxTime(float t)
    {
        maxTime = t;
    }
    public void setCurrentTime(float t)
    {
        currentTime = t;
    }

    public float getRateOverMaxTime(float tLeft)
    {
        return tLeft / maxTime;
    }
    public float getCurrentTime()
    {
        return currentTime;
    }
    public float getMaxTime()
    {
        return maxTime;
    }
}
