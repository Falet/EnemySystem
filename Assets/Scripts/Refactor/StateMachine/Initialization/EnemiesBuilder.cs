using System.Collections.Generic;
using UnityEngine;

public class EnemiesBuilder : MonoBehaviour
{
    [SerializeField] private ControllerForStateStandOnLine controllerForStateStandOnLine;
    [SerializeField] private AvailabilityControlPoint availabilityControlPoint;
    [SerializeField] private ThrowObject throwObject;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private List<EnemyInfo> spawnInfo;

    public void Init(DataForShurikenTrajectory dataForShurikenTrajectory)
    {
        controllerForStateStandOnLine.Init(spawnInfo.Count,availabilityControlPoint.EnemiesAtControlPoint);
        for (int i = 0; i < spawnInfo.Count; i++)
        {
            var initializationEnemy = Instantiate(spawnInfo[i].enemyPrefab,spawnInfo[i].spawnPoint);
            initializationEnemy.transform.position = spawnInfo[i].spawnPoint.position;

            availabilityControlPoint.Init(dataForShurikenTrajectory);
            availabilityControlPoint.AddEnemyToList(initializationEnemy.EnemyStateController);
            
            initializationEnemy.InitPatrolState(spawnInfo[i].pointsForPatrol,spawnInfo[i].queueCompleteForSerialize, spawnInfo[i].spawnPoint.position);
            initializationEnemy.InitDeadState(availabilityControlPoint);
            initializationEnemy.InitStandOnLineState(controllerForStateStandOnLine.GetPosition(initializationEnemy.gameObject));
            initializationEnemy.InitAttackState(playerPosition);
            initializationEnemy.InitStateController(throwObject);
        }
    }
}