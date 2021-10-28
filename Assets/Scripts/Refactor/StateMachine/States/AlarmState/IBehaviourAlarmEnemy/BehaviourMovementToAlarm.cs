using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BehaviourMovementToAlarm : MonoBehaviour, IBehaviourAlarmEnemy
{
    [SerializeField] private MovementWithNavmesh movementWithNavmesh;
    [SerializeField] private float speedMovement = 3.5f;
    [SerializeField] private Transform centerTargetPointsForAlarm;
    [SerializeField] private List<Transform> targetPointsForAlarm;
    [SerializeField] private float waitSecondBetweenPoints = 0.5f;
    [SerializeField] private Transform characterPosition;
    //[SerializeField] private EnemyAnimationController enemyAnimationController;
    //[SerializeField] private GameObject exclamationMark;//TODO Вместо включения выключения объект сделать, запуск поведения визуала

    private Action _completeMove;
    private Action _allBehaviourCompleted;
    private WaitForSeconds _duration;
    private Coroutine _moveToPoints;
    private int _countAlarmPointsForMove;
    
    private void Awake()
    {
        _completeMove = OnCompleteMoveToAlarmPoint;
        _duration = new WaitForSeconds(waitSecondBetweenPoints);
        targetPointsForAlarm = centerTargetPointsForAlarm.gameObject.GetComponentsInChildren<Transform>().ToList();
        targetPointsForAlarm.Remove(centerTargetPointsForAlarm);
    }

    public void StartBehaviour()
    {
        //exclamationMark.SetActive(true);
        movementWithNavmesh.CompleteMovementToPoint += _completeMove;
        _countAlarmPointsForMove = 0;
        
        StartMoveToAlarmPoint();
    }

    public void StopBehaviour()
    {
        movementWithNavmesh.CompleteMovementToPoint -= _completeMove;
        
        if (_moveToPoints != null)
        {
            StopCoroutine(_moveToPoints);
        }
        
        movementWithNavmesh.Kill();
        
        //enemyAnimationController.StopAnimationRunning();

        //exclamationMark.SetActive(false);
    }

    public void OnComplete(Action callBack)
    {
        _allBehaviourCompleted = callBack;
    }

    private void StartMoveToAlarmPoint()
    {
        centerTargetPointsForAlarm.position = characterPosition.position;
        SetNewPoint();
    }

    private IEnumerator MoveToPoints()
    {
        yield return _duration;
        SetNewPoint();
    }

    private void SetNewPoint()
    {
        if (_countAlarmPointsForMove < targetPointsForAlarm.Count)
        {
            //enemyAnimationController.PlayAnimationRunning();
            movementWithNavmesh.SetDestination(targetPointsForAlarm[_countAlarmPointsForMove].position,speedMovement);
            _countAlarmPointsForMove++;
        }
        else
        {
            _countAlarmPointsForMove = 0;
            AllAlarmPointIsComplete();
        }
    }

    private void OnCompleteMoveToAlarmPoint()
    {
        //enemyAnimationController.StopAnimationRunning();
        _moveToPoints = StartCoroutine(MoveToPoints());
    }

    private void AllAlarmPointIsComplete()
    {
        _allBehaviourCompleted?.Invoke();
    }
}
