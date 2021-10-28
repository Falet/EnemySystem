using System;
using UnityEngine;

public class AlarmAttack : MonoBehaviour
{
    public event Action Alarmed;

    [SerializeField] private float alarmTimeInSecond = 5f;
    
    public float AlarmTimeInSecond => alarmTimeInSecond;

    public void StartAlarm()
    {
        Alarmed?.Invoke();
    }
    
    public void EndAlarm()
    {
        //Alarmed?.Invoke();
    }

}
