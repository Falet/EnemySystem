using System;
using System.Collections;
using UnityEngine;

public class BehaviourAlarmAttack : MonoBehaviour, IBehaviourEnemyAttack
{
    [SerializeField] private AlarmAttack alarmAttack;
    [SerializeField] private Rotation rotation;
    [SerializeField] private Transform characterPosition;
    [SerializeField] private float durationRotation = 0.1f;
    //[SerializeField] private EnemyAnimationController animationController;

    private WaitForSeconds _waitForSecondsEndAlarm;
    private Coroutine _waitEndAlarm;
    private Action<IBehaviourEnemyAttack> _completeAlarm;

    private void Awake()
    {
        _waitForSecondsEndAlarm = new WaitForSeconds(alarmAttack.AlarmTimeInSecond);
    }

    public void StartBehaviour()
    {
        StartAlarm();
    }

    public void StopBehaviour()
    {
        alarmAttack.EndAlarm();
        if (_waitEndAlarm != null)
        {
            StopCoroutine(_waitEndAlarm);
        }
        //animationController.StopAnimationDetect();
    }

    public void OnComplete(Action<IBehaviourEnemyAttack> callBack)
    {
        _completeAlarm = callBack;
    }

    private void StartAlarm()
    {
        alarmAttack.StartAlarm();
        rotation.LookAt(characterPosition.position,durationRotation);
        //animationController.PlayAnimationDetect();
        _waitEndAlarm = StartCoroutine(WaitEndAlarm());
    }

    private IEnumerator WaitEndAlarm()
    {
        yield return _waitForSecondsEndAlarm;
        //animationController.StopAnimationDetect();
        alarmAttack.EndAlarm();
        _completeAlarm?.Invoke(this);
    }
}
