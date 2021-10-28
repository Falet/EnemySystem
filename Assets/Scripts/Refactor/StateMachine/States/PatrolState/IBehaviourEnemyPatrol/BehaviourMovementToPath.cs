using System;
using UnityEngine;

public class BehaviourMovementToPath : MonoBehaviour, IBehaviourEnemyPatrol
{
    [SerializeField] private MovementWithNavmesh movementWithNavmesh;
    [SerializeField] private float speedMovement = 3.5f;
    [SerializeField] private Transform defaultPosition;
    [SerializeField] private EnemyAnimationController enemyAnimationController;
    
    private Action _completedBehaviour;
    private Action _completedMove;

    private void Awake()
    {
        _completedMove = OnMoveCompleted;
    }

    public void StartBehaviour(bool startOver)
    {
        movementWithNavmesh.CompleteMovementToPoint += _completedMove;
        MoveToDefault();
    }

    public void StopBehaviour()
    {
        movementWithNavmesh.CompleteMovementToPoint -= _completedMove;
        
        enemyAnimationController.StopAnimationWalking();
    }

    public void OnPartComplete(Action callBack)
    {
        _completedBehaviour = callBack;
    }

    public void OnAllComplete(Action callBack)
    {
        _completedBehaviour = callBack;
    }

    private void MoveToDefault()
    {
        enemyAnimationController.PlayAnimationWalking();
        movementWithNavmesh.SetDestination(defaultPosition.position,speedMovement);
    }

    private void OnMoveCompleted()
    {
        _completedBehaviour?.Invoke();
    }
}