using System;
using UnityEngine;

public class DieCharacterByNet : MonoBehaviour
{
    //[SerializeField] private ScriptableEvent characterDetectedByEnemy;
    private Action _characterDetectedByEnemyInvocator;
    
    private void OnEnable()
    {
        //_characterDetectedByEnemyInvocator = characterDetectedByEnemy.SubscribeToInvocation(this);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        _characterDetectedByEnemyInvocator.Invoke();
    }
    
    private void OnDisable()
    {
        //characterDetectedByEnemy.UnsubscribeFromInvocation(this);
    }
}