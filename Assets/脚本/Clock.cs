using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hourTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;
    public bool iscontinue = true;

    private const float degree = 30f;
    void UpdateContinuous() {
        DateTime time = DateTime.Now;
        hourTransform.localRotation = 
        Quaternion.Euler(0f,time.Hour * degree,0f);
        minutesTransform.localRotation = 
        Quaternion.Euler(0f,time.Minute * degree,0f);
        secondsTransform.localRotation = 
        Quaternion.Euler(0f,time.Second * degree,0f);
    }

    void UpdateDiscrete() {
        DateTime time = DateTime.Now;
        hourTransform.localRotation = 
        Quaternion.Euler(0f,time.Hour * degree,0f);
        minutesTransform.localRotation = 
        Quaternion.Euler(0f,time.Minute * degree,0f);
        secondsTransform.localRotation = 
        Quaternion.Euler(0f,time.Second * degree,0f);
    }

    void Update()
    {
        if(iscontinue)
        {
            UpdateContinuous();
        }
        else
        {
            UpdateDiscrete();
        }
    }
}
