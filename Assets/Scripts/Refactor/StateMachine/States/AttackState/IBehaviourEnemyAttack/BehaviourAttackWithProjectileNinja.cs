using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BehaviourAttackWithProjectileNinja : MonoBehaviour, IBehaviourEnemyAttack
{
    [SerializeField] private AttackByProjectile attackByProjectile;
    [SerializeField] private Transform fromProjectile;
    [SerializeField] private float speedFlyProjectile;
    [SerializeField] private float delayAfterFire = 0.5f;
    [SerializeField] private AnimationControllerEnemy animationController;
    [SerializeField] private Rotation rotation;
    [SerializeField] private float durationRotation;

    private Action<IBehaviourEnemyAttack> _completedAttack;
    private WaitForSeconds _waitAfterAttack;
    private Coroutine _delayBeforeFire;
    private Transform _playerPosition;
    private Tweener _rotateTweener;

    private void Awake()
    {
        _waitAfterAttack = new WaitForSeconds(delayAfterFire);
    }

    public void Init(Transform playerPosition)
    {
        _playerPosition = playerPosition;
    }
    
    public void StartBehaviour()
    {
        StartAttack();
    }

    public void StopBehaviour()
    {
        if (_delayBeforeFire != null)
        {
            StopCoroutine(_delayBeforeFire);
        }
        _rotateTweener.Kill();
    }

    public void OnComplete(Action<IBehaviourEnemyAttack> callBack)
    {
        _completedAttack = callBack;
    }

    private void StartAttack()
    {
        _rotateTweener = rotation.LookAt(_playerPosition.position, durationRotation);
        _rotateTweener.OnComplete(OnCompleteRotation);
    }

    private void OnCompleteRotation()
    {
        attackByProjectile.Fire(fromProjectile, _playerPosition, 
            Vector3.Distance(fromProjectile.position,_playerPosition.position)/speedFlyProjectile);
        animationController.Fire();
        _delayBeforeFire = StartCoroutine(DelayBeforeFire());
    }

    private IEnumerator DelayBeforeFire()
    {
        yield return _waitAfterAttack;
        CompleteAttack();
    }

    private void CompleteAttack()
    {
        _completedAttack?.Invoke(this);
    }
}