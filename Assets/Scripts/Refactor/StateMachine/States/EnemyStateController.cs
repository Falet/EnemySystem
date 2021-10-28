using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [SerializeField] private CharacterDetection characterDetection;
    [SerializeField] private List<AlarmAttack> alarmAttackEvents;
    [SerializeField] private AttackState attackState;
    [SerializeField] private AlarmState alarmState;
    [SerializeField] private DeadState deadState;
    [SerializeField] private State defaultState;
    
    private State currentState;
    private Action _stateCompleted;
    
    private void Awake()
    {
        _stateCompleted = OnStateCompleted;
        characterDetection.CharacterDetected += CharacterDetected;
    }

    private void AlarmAttackEventsOnAlarmed()
    {
        SetState(alarmState);
    }

    private void OnEnable()
    {
        if (alarmAttackEvents != null)
        {
            for (var i = 0; i < alarmAttackEvents.Count; i++)
            {
                alarmAttackEvents[i].Alarmed += AlarmAttackEventsOnAlarmed;
            }
        }
    }

    private void Start()
    {
        currentState = defaultState;
        currentState.OnCompletedState(_stateCompleted);
        currentState.OnSet();
        currentState = defaultState;
    }

    private void CharacterDetected()
    {
        if (!(currentState is AttackState))
        {
            SetState(attackState);
        }
    }
    
    private void SetState(State state)
    {
        currentState.OnCompletedState(null);
        currentState.OnUnset();
        currentState = state;
        currentState.OnCompletedState(_stateCompleted);
        currentState.OnSet();
    }

    private void OnStateCompleted()
    {
        SetState(defaultState);
    }

    private void OnDisable()
    {
        characterDetection.CharacterDetected -= CharacterDetected;
        for (var i = 0; i < alarmAttackEvents.Count; i++)
        {
            alarmAttackEvents[i].Alarmed -= AlarmAttackEventsOnAlarmed;
        }
    }

    public void Die()
    {
        currentState.OnCompletedState(null);
        currentState.OnUnset();
        deadState.OnSet();
        currentState = deadState;
        enabled = false;
    }
}
