using UnityEngine;

public class InitializationAttackState : MonoBehaviour
{
    [SerializeField] private BehaviourAttackWithProjectileNinja behaviourAttackWithProjectileNinja;
    public void Init(Transform playerPosition)
    {
        behaviourAttackWithProjectileNinja.Init(playerPosition);
    }
}