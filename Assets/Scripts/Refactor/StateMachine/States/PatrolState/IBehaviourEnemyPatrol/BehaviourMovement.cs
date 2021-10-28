using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourMovement : MonoBehaviour,IBehaviourEnemyPatrol
{
    [SerializeField] private MovementWithNavmesh movementWithNavmesh;
    [SerializeField] private float speedMovement = 3.5f;
    [SerializeField] private List<Transform> targetPoints;
    [SerializeField] private float duration = 1f;
    //[SerializeField] private EnemyAnimationController animationController;

    private int _countPointsForMove;
    private List<NavMeshPath> _calculatedPath;
    private WaitForSeconds _waiterForCoroutine;
    private Coroutine _hold;
    private Action _onPartComplete;
    private Action _onAllComplete;
    private Action _completeMove;
    
    private void Awake()
    {
        _completeMove = OnCompleteMovementToPoint;
        _waiterForCoroutine = new WaitForSeconds(duration);
        for (var j = targetPoints.Count-2; j > 0 ; j--)
        {
            targetPoints.Add(targetPoints[j]);
        }

        _calculatedPath = new List<NavMeshPath>();
        for (var g = 0; g < targetPoints.Count; g++)
        {
            _calculatedPath.Add(new NavMeshPath());
            movementWithNavmesh.CalculatePath(targetPoints[g].position,_calculatedPath[g]);
        }
    }

    public void StartBehaviour(bool startOver)
    {
        movementWithNavmesh.CompleteMovementToPoint += _completeMove;
        if (startOver)
        {
            _countPointsForMove = 0;
        }

        GoToNextPoint();
    }

    public void StopBehaviour()
    {
        movementWithNavmesh.CompleteMovementToPoint -= _completeMove;
        
        if (_hold != null)
        {
            StopCoroutine(_hold);
        }
        movementWithNavmesh.Kill();
        //animationController.StopAnimationWalking();
        
        if (_countPointsForMove >= targetPoints.Count)
        {
            _countPointsForMove = 0;
        }
    }

    public void OnPartComplete(Action callBack)
    {
        _onPartComplete = callBack;
    }

    public void OnAllComplete(Action callBack)
    {
        _onAllComplete = callBack;
    }

    private void GoToNextPoint()
    {
        if (_countPointsForMove < _calculatedPath.Count)
        {
            Move();
        }
        else
        {
            _countPointsForMove = 0;
            _onAllComplete?.Invoke();
        }
    }
    
    private void OnCompleteMovementToPoint()
    {
        //animationController.StopAnimationWalking();
        _hold = StartCoroutine(Hold());
    }

    private IEnumerator Hold()
    {
        _countPointsForMove++;
        yield return _waiterForCoroutine;
        GoToNextPoint();
        _onPartComplete?.Invoke();
    }

    private void Move()
    {
        //animationController.PlayAnimationWalking();
        if (movementWithNavmesh.SetPath(_calculatedPath[_countPointsForMove],speedMovement) == false)
        {
            movementWithNavmesh.SetDestination(targetPoints[_countPointsForMove].position,speedMovement);
        }
    }
}
