using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BehaviourAttack : MonoBehaviour, IBehaviourEnemyAttack
{
    //[SerializeField] private EnemyAnimationController animationController;
    [SerializeField] private Rotation rotation;
    [SerializeField] private float delayAfterFire = 0.5f;
    [SerializeField] private float durationRotation;
    //[SerializeField] private float stunTime;//Stun должен делать отдельный компонент
    [SerializeField] private Transform characterPosition;
    //[SerializeField] private ScriptableFloatEvent setTongueOnCooldown;

    private WaitForSeconds _waitAfterAttack;
    private Coroutine _delayAfterAttack;
    private Action<IBehaviourEnemyAttack> _completedAttack;
    private Tweener _tweenerRotation;
    private TweenCallback _tweenCallback;
    
    private void Awake()
    {
        _waitAfterAttack = new WaitForSeconds(delayAfterFire);
        _tweenCallback = CompleteRotation;
    }

    public void StartBehaviour()
    {
        StartAttack();
    }

    public void StopBehaviour()
    {
        if (_delayAfterAttack != null)
        {
            StopCoroutine(_delayAfterAttack);
        }
        _tweenerRotation?.Kill();
    }

    public void OnComplete(Action<IBehaviourEnemyAttack> callBack)
    {
        _completedAttack = callBack;
    }
    
    private void StartAttack()
    {
        _tweenerRotation = rotation.LookAt(characterPosition.position,durationRotation);
        _tweenerRotation.OnComplete(_tweenCallback);
    }
    
    private void CompleteRotation()
    {
        //animationController.PlayAnimationAttack();
        //setTongueOnCooldown.Invoke(stunTime);
        _delayAfterAttack = StartCoroutine(DelayAfterAttack());
    }
    
    private IEnumerator DelayAfterAttack()
    {
        yield return _waitAfterAttack;
        _completedAttack?.Invoke(this);
    }
}