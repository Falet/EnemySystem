using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BehaviourRotation : MonoBehaviour, IBehaviourEnemyPatrol
{
    [SerializeField] private Rotation rotation;
    [SerializeField] private List<Vector3> targetPoints;
    [SerializeField] private float duration = 1f;
    //[SerializeField] private EnemyAnimationController animationController;

    private int countPointsForRotation;
    private Tweener _tween;
    private Coroutine hold;
    private WaitForSeconds waiterForCoroutine;
    private Action onPartComplete;
    private Action onAllComplete;
    private Vector3 currentRotation;//Для того чтобы ненужные к изменению оси ротации оставались неизменными или использовать другой формат данных для поворота
    private TweenCallback _tweenCallback;
                                    
    private void Awake()
    {
        waiterForCoroutine = new WaitForSeconds(duration);
        for (var j = targetPoints.Count-2; j > 0 ; j--)
        {
            targetPoints.Add(targetPoints[j]);
        }
    }

    private void OnEnable()
    {
        _tweenCallback = OnCompletedRotation;
    }

    public void StartBehaviour(bool startOver)
    {
        if (startOver)
        {
            countPointsForRotation = 0;
        }
        GoToNextPoint();
    }

    public void StartBehaviour()
    {
        GoToNextPoint();
    }

    public void StopBehaviour()
    {
        if (hold != null)
        {
            StopCoroutine(hold);
        }

        _tween?.Kill();

        //animationController.StopAnimationWalking();
        
        if (countPointsForRotation >= targetPoints.Count)
        {
            countPointsForRotation = 0;
        }
    }

    public void OnPartComplete(Action callBack)
    {
        onPartComplete = callBack;
    }

    public void OnAllComplete(Action callBack)
    {
        onAllComplete = callBack;
    }

    private void GoToNextPoint()
    {
        if (countPointsForRotation < targetPoints.Count)
        {
            Rotate();
        }
        else
        {
            countPointsForRotation = 0;
            onAllComplete?.Invoke();
        }
    }

    private void OnCompletedRotation()
    {
        //animationController.StopAnimationWalking();
        hold = StartCoroutine(Hold());
    }
    
    private IEnumerator Hold()
    {
        countPointsForRotation++;
        yield return waiterForCoroutine;
        GoToNextPoint();
        onPartComplete?.Invoke();
    }

    private void Rotate()
    {
        //animationController.PlayAnimationWalking();
        _tween = rotation.SetRotate(targetPoints[countPointsForRotation],duration);
        _tween.OnComplete(_tweenCallback);
    }
    
    private void OnDisable()
    {
        _tweenCallback = null;
    }
}
