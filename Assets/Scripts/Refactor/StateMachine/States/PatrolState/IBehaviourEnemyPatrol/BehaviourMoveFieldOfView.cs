using System;
using System.Collections;
using UnityEngine;

//TODO Rework класс говна, убрать методы перемещения в отдельный класс
public class BehaviourMoveFieldOfView : MonoBehaviour, IBehaviourEnemyPatrol
{
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private float radius = 5f;
    private Action onPartComplete;
    private Action onAllComplete;
    private Vector3 _startLocalPositionFieldOfView;
    private Coroutine _coroutineInfinityMoveStart;
    private Coroutine _coroutineInfinityMoveEnd;
    private int _stepMovementForSinusoid;//TODo сделать счетчиком для корутин
    
    private void Awake()
    {
        _startLocalPositionFieldOfView = fieldOfView.transform.localPosition;
    }
    public void StartBehaviour(bool startOver)
    {
        _coroutineInfinityMoveStart = StartCoroutine(InfinityMoveStart(fieldOfView.transform));
    }

    public void StopBehaviour()
    {
        if (_coroutineInfinityMoveStart != null)
        {
            StopCoroutine(_coroutineInfinityMoveStart);
        }

        if (_coroutineInfinityMoveEnd != null)
        {
            StopCoroutine(_coroutineInfinityMoveEnd);
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
    
    private IEnumerator InfinityMoveStart(Transform transformObject)
    {
        var i = 0;
        while (true)
        {
            var rad = i / 180f * Mathf.PI;
            var x = _startLocalPositionFieldOfView.x + radius * Mathf.Cos(rad);
            var z = _startLocalPositionFieldOfView.z + radius * Mathf.Sin(rad);
            transformObject.localPosition = new Vector3(x - radius, _startLocalPositionFieldOfView.y, z);
            i += 2;
            yield return null;
            if (i >= 360) break;
        }
        
        onPartComplete?.Invoke();
        _coroutineInfinityMoveEnd = StartCoroutine(InfinityMoveEnd(transformObject));
    }
    private IEnumerator InfinityMoveEnd(Transform transformObject)
    {
        var i = 180;
        while (true)
        {
            var rad = i / 180f * Mathf.PI;
            var x = _startLocalPositionFieldOfView.x + radius * Mathf.Cos(rad);
            var z = _startLocalPositionFieldOfView.z + radius * Mathf.Sin(rad);
            transformObject.localPosition = new Vector3(x + radius, _startLocalPositionFieldOfView.y, z);
            i-=2;
            yield return null;
            if (i <= -180) break;
        }
        
        onPartComplete?.Invoke();
        onAllComplete?.Invoke();
        _coroutineInfinityMoveStart = StartCoroutine(InfinityMoveStart(transformObject));
    }
}