using System;
using System.Collections;
using UnityEngine;

public class BehaviourMovementAtAlarm : MonoBehaviour, IBehaviourEnemyAttack
{
    [SerializeField] private Movement movement;
    [SerializeField] private EnemyAnimationController enemyAnimationController;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private Transform centerCircle;
    [SerializeField] private Transform[] targetsForCircle;
    [SerializeField] private float speedMovement = 3.5f;
    [SerializeField] private AlarmAttack alarmAttack;
    [SerializeField] private Transform objectForMove;
    [SerializeField] private Transform characterPosition; //TODO Rework. ScriptableObject говно,
                                                        //например [SerializeField] private Transform positionCharacter,
                                                        //а может и нормально - именно vector3.
                                                        //Нужно переделать сам ScriptableObject, чтобы он мог лишь
                                                        //выдать инфу о местоположении персонажа
                                              
    private Action<IBehaviourEnemyAttack> _movementCompleted;
    private Action _completeMove;
    private Vector3 _defaultPosition;
    private Vector3 _defaultEulerRotation;
    private WaitForSeconds _alarmWait;
    private Coroutine _waitDisableAlarm;
    
    private void Awake()
    {
        _completeMove = CompletedMovement;
        _defaultPosition = objectForMove.position;
        _defaultEulerRotation = objectForMove.eulerAngles;
        _alarmWait = new WaitForSeconds(alarmAttack.AlarmTimeInSecond);
    }

    public void StartBehaviour()
    {
        movement.CompleteMovementToPoint += _completeMove;
        StartMovement();
    }

    public void StopBehaviour()
    {
        movement.CompleteMovementToPoint -= _completeMove;
        
        movement.Kill();

        if (_waitDisableAlarm != null)
        {
            StopCoroutine(_waitDisableAlarm);
        }

        objectForMove.position = _defaultPosition;
        objectForMove.eulerAngles = _defaultEulerRotation;
    }

    public void OnComplete(Action<IBehaviourEnemyAttack> callBack)
    {
        _movementCompleted = callBack;
    }

    private void CompletedMovement()
    {
        _waitDisableAlarm = StartCoroutine(WaitDisableAlarm());
    }

    private IEnumerator WaitDisableAlarm()
    {
        if (Vector3.Distance(objectForMove.position, _defaultPosition) > 0.1f)
        {
            movement.SetPath(targetsForCircle, alarmAttack.AlarmTimeInSecond);
            yield return _alarmWait;
            movement.SetDestination(_defaultPosition,speedMovement);
        }
        else
        {
            _movementCompleted?.Invoke(this);
        }
    }

    private void StartMovement()
    {
        var positionForFlyEnemy = characterPosition.position;
        positionForFlyEnemy.y += 0.1f;
        fieldOfView.transform.position = positionForFlyEnemy;
        positionForFlyEnemy.y = transform.position.y;
        centerCircle.position = positionForFlyEnemy;
        movement.SetDestination(positionForFlyEnemy,speedMovement);
    }
}
