using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyInfo
{
    [SerializeField]public InitializationEnemy enemyPrefab;
    [SerializeField]public Transform spawnPoint;
    [SerializeField]public Transform[] pointsForPatrol;
    [SerializeField]public List<WithoutOdin> queueCompleteForSerialize;
}