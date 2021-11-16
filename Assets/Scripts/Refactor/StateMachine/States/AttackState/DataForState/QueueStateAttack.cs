using System.Collections.Generic;
using UnityEngine;

public class QueueStateAttack : MonoBehaviour
{
    [SerializeField] private List<DataCompleteBehaviourAttack> queueComplete;
    [SerializeField] private BehaviourAttackWithProjectile behaviourAttackWithProjectile;
    [SerializeField] private BehaviourAlarmAttack behaviourAlarmAttack;
    [SerializeField] private BehaviourMovementAtAlarm behaviourMovementAtAlarm;
    [SerializeField] private BehaviourAttack behaviourAttack;
    [SerializeField] private BehaviourAttackWithProjectileNinja behaviourAttackWithProjectileNinja;
    [SerializeField] private AttackState state;
    
    private void Awake()
    {
        queueComplete = new List<DataCompleteBehaviourAttack>();
        
        if (behaviourMovementAtAlarm != null)
        {
            queueComplete.Add(new DataCompleteBehaviourAttack(behaviourMovementAtAlarm, true));
        }
        
        if (behaviourAttack != null)
        {
            queueComplete.Add(new DataCompleteBehaviourAttack(behaviourAttack, true));
        }

        if (behaviourAttackWithProjectile != null)
        {
            queueComplete.Add(new DataCompleteBehaviourAttack(behaviourAttackWithProjectile,true));
            
        }

        if (behaviourAlarmAttack != null)
        {
            queueComplete.Add(new DataCompleteBehaviourAttack(behaviourAlarmAttack, true));
        }
        
        if (behaviourAttackWithProjectileNinja != null)
        {
            queueComplete.Add(new DataCompleteBehaviourAttack(behaviourAttackWithProjectileNinja, true));
        }
        
        state.SetQueueComplete(queueComplete);
    }
}
