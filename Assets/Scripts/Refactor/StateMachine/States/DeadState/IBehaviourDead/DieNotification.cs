using System;
using UnityEngine;
using UnityEngine.AI;

public class DieNotification : MonoBehaviour, IBehaviourDead
{
    public event Action<EnemyStateController> EnemyDead;

    [SerializeField] private EnemyStateController enemyStateController;//TODO переосмыслить, ибо поведение знает о
                                                                       //контроллере состояний, а он над уровнем
                                                                       //поведения и поведению нельзя его использовать
    [SerializeField] private AnimationControllerEnemy animationControllerEnemy;
    [SerializeField] private Collider colliderTrigger;
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    public void Init()
    {
        
    }

    public void StartBehaviour()
    {
        colliderTrigger.enabled = false;//TODO Не поведение должно отключать логику
        animationControllerEnemy.Death();
        navMeshAgent.enabled = false;
        EnemyDead?.Invoke(enemyStateController);
    }

    public void StopBehaviour()
    {
        
    }
}