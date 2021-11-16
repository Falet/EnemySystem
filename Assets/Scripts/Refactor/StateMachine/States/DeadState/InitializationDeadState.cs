using System.Collections.Generic;
using UnityEngine;

public class InitializationDeadState : MonoBehaviour
{
    [SerializeField] private DieNotification dieNotification;
    [SerializeField] private DeadState deadState;
    
    private List<DataCompleteBehaviourDead> _queueComplete;
    
    public void Init(AvailabilityControlPoint availabilityControlPoint)
    {
        dieNotification.EnemyDead += availabilityControlPoint.RemoveEnemyFromList;//TODO придумать отписку и понять
                                                                                //нужна ли она если скорее всего
                                                                                //будет удаляться только объект с эвентом
                                                                                //или объект с эвентом и подписчик сразу

        _queueComplete = new List<DataCompleteBehaviourDead> { new DataCompleteBehaviourDead(dieNotification) };

        deadState.SetQueueComplete(_queueComplete);
    }
}