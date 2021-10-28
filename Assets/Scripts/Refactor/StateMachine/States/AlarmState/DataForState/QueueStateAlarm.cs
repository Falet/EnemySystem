using System.Collections.Generic;
using UnityEngine;

public class QueueStateAlarm : MonoBehaviour
{
    [SerializeField] private List<DataCompleteBehaviourAlarm> queueComplete;
    [SerializeField] private BehaviourMovementToAlarm behaviourAlarm;
    [SerializeField] private AlarmState state;

    private void Awake()
    {
        queueComplete = new List<DataCompleteBehaviourAlarm>
        {
            new DataCompleteBehaviourAlarm(behaviourAlarm)
        };
        
        state.SetQueueComplete(queueComplete);
    }
}