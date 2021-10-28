using System;
using System.Collections;
using UnityEngine;

public class BehaviourAttackWithProjectile : MonoBehaviour, IBehaviourEnemyAttack
{
    [SerializeField] private AttackWithProjectile attackWithProjectile;
    [SerializeField] private float delayBeforeFire = 0.5f;
    [SerializeField] private float delayAfterFire = 0.5f;
    [SerializeField] private EnemyAnimationController animationController;
    [SerializeField] private CharacterDetection characterDetection;
    
    private Action<IBehaviourEnemyAttack> _completedAttack;
    private WaitForSeconds _waitBeforeAttack;
    private WaitForSeconds _waitAfterAttack;
    private Coroutine _delayBeforeFire;
    
    private void Awake()
    {
        _waitBeforeAttack = new WaitForSeconds(delayBeforeFire);
        _waitAfterAttack = new WaitForSeconds(delayAfterFire);
    }
    //TODO Переделать подписку на эвент, убрать аллок и сделать на колбэках
    private void OnEnable()
    {
        attackWithProjectile.CompleteAttack += CompleteAttack;
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
        attackWithProjectile.Kill();
        
        animationController.PlayAnimationWaitWatch();
    }

    public void OnComplete(Action<IBehaviourEnemyAttack> callBack)
    {
        _completedAttack = callBack;
    }

    private void StartAttack()
    {
        animationController.PlayAnimationAttack();
        _delayBeforeFire = StartCoroutine(DelayBeforeFire());
    }

    private IEnumerator DelayBeforeFire()
    {
        yield return _waitBeforeAttack;
        if (characterDetection.CharacterIsDetect)
        {
            animationController.PlayAnimationBang();
            attackWithProjectile.Attack();
        }
        else
        {
            animationController.PlayAnimationWaitWatch();
            yield return _waitAfterAttack;
            CompleteAttack();
        }
    }

    private void CompleteAttack()
    {
        _completedAttack?.Invoke(this);
    }

    private void OnDisable()
    {
        attackWithProjectile.CompleteAttack -= CompleteAttack;
    }
}
