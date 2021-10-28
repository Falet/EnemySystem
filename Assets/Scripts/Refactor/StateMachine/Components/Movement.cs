using System;
using DG.Tweening;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Action CompleteMovementToPoint;

    private Tweener _tweener;
    private TweenCallback _tweenCallback;

    private void Awake()
    {
        _tweenCallback = CompletedMovement;
    }

    public void SetDestination(Vector3 targetPosition, float speed)
    {
        var distance = Vector3.Distance(transform.position,targetPosition);
        _tweener?.Kill();
        _tweener = transform.DOMove(targetPosition, distance / speed);
        transform.DOLookAt(targetPosition, 0.1f, AxisConstraint.Y);
        _tweener.OnComplete(_tweenCallback);
    }

    public void SetPath(Transform[] targets, float duration)
    {
        var targetsVector3 = new Vector3[targets.Length];//TODO remove Allocated memory
        for (var i = 0; i < targets.Length; i++)
        {
            targetsVector3[i] = targets[i].position;
        }
        _tweener = transform.DOPath(targetsVector3,duration,PathType.CatmullRom).SetLookAt(0.005f);
    }

    public void Kill()
    {
        _tweener?.Kill();
    }

    private void CompletedMovement()
    {
        CompleteMovementToPoint?.Invoke();
    }
}