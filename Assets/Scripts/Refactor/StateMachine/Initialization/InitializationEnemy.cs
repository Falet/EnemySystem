using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Реализация в данном проекте, этот класс всегда будет собираемым. Тогда нужно сделать для
//каждого инита состояния свой интерфейс(или не делать потому что не известно какие данные будут подаваться на вход)
public class InitializationEnemy: MonoBehaviour
{
    public EnemyStateController EnemyStateController => enemyStateController;
    
    [SerializeField] private EnemyStateController enemyStateController;
    [SerializeField] private QueueStatePatrol queueStatePatrol;
    [SerializeField] private InitializationDeadState initializationDeadState;
    [SerializeField] private InitializationStateStandOnLine initializationStateStandOnLine;
    [SerializeField] private InitializationAttackState initializationAttackState;

    public void InitStateController(ThrowObject throwObject)
    {
        throwObject.CompletedThrow += enemyStateController.CharacterDetected;//TODO сделать отписку
        enemyStateController.Init();
    }
    
    //TODO Сделать отдельные иниты для каждого состояния(ибо общий инит быстро перегрузиться параметрами) и выше контролировать их очередность
    public void InitPatrolState(Transform[] pointsForPatrol, List<WithoutOdin> queueCompleteForSerialize, Vector3 targetPosition)
    {
        queueStatePatrol.Init(pointsForPatrol.ToList(),queueCompleteForSerialize, targetPosition);
    }
    
    public void InitDeadState(AvailabilityControlPoint availabilityControlPoint)
    {
        initializationDeadState.Init(availabilityControlPoint);
    }

    public void InitStandOnLineState(DataSinusiod sinusiodForMove)
    {
        //initializationStateStandOnLine.Init(positionForMove);
    }

    public void InitAttackState(Transform playerPosition)
    {
        initializationAttackState.Init(playerPosition);
    }
}