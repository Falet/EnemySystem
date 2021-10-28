using System;
using System.Collections;
using UnityEngine;

public class BehaviourRecurrentCheck : MonoBehaviour, IBehaviourEnemyPatrol
{
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private Animator animatorForLink;
    [SerializeField] private EnemyAnimationController animationController;
    [SerializeField] private float cooldownTimeBetweenCheck;
    
    private Action onPartComplete;
    private Action onAllComplete;
    private bool isStoppedBehaviour;
    private WaitForSeconds _waitForSeconds;
    private Coroutine _cooldownCheck;
    
    private void Awake()
    {
        SceneLinkedSMB<BehaviourRecurrentCheck>.Initialise(animatorForLink,this);
        _waitForSeconds = new WaitForSeconds(cooldownTimeBetweenCheck);
    }

    public void StartBehaviour(bool startOver)
    {
        fieldOfView.gameObject.SetActive(true);
        isStoppedBehaviour = false;
        StartCheck();
    }

    public void StopBehaviour()
    {
        fieldOfView.gameObject.SetActive(true);
        isStoppedBehaviour = true;
        if (_cooldownCheck != null)
        {
            StopCoroutine(_cooldownCheck);
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

    //TODO сделать интерфейс для связки с аниматором, если возможно
    public void OnCompletedAnimation()
    {
        if (isStoppedBehaviour == false)//TODO подумать как избавиться от булика
        {
            fieldOfView.gameObject.SetActive(false);
            _cooldownCheck = StartCoroutine(CooldownCheck());
        }
    }

    private IEnumerator CooldownCheck()
    {
        yield return _waitForSeconds;
        onPartComplete?.Invoke();
        onAllComplete?.Invoke();
    }

    private void StartCheck()
    {
        animationController.PlayAnimationWatch();
    }
}