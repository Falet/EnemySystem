using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    public Text Text;
    DateTime _lastTime; // marks the beginning the measurement began
    int _framesRendered; // an increasing count
    int _fps; // the FPS calculated from the last measurement
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }
    
    void Update()
    {
        _framesRendered++;

        if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
        {
            // one second has elapsed 

            _fps = _framesRendered;                     
            _framesRendered = 0;            
            _lastTime = DateTime.Now;
        }

        Text.text = _fps.ToString();
    }
}