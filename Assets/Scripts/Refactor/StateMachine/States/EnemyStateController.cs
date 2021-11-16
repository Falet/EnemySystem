using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [SerializeField] private List<AlarmAttack> alarmAttackEvents;
    [SerializeField] private AttackState attackState;
    [SerializeField] private AlarmState alarmState;
    [SerializeField] private DeadState deadState;
    [SerializeField] private State defaultState;
    
    private State _currentState;
    private Action _stateCompleted;

    public void Init()
    {
        enabled = false;
        _currentState = defaultState;
        _currentState.OnCompletedState(_stateCompleted);
        _currentState.OnSet();
        _currentState = defaultState;
    }

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }
    
    private void Awake()
    {
        _stateCompleted = OnStateCompleted;
    }

    private void AlarmAttackEventsOnAlarmed()
    {
        SetState(alarmState);
    }

    private void OnEnable()
    {
        /*if (alarmAttackEvents != null)
        {
            for (var i = 0; i < alarmAttackEvents.Count; i++)
            {
                alarmAttackEvents[i].Alarmed += AlarmAttackEventsOnAlarmed;
            }
        }*/
    }

    public void CharacterDetected()
    {
        if (_currentState is AttackState == false && _currentState is DeadState == false)
        {
            SetState(attackState);
        }
    }
    
    private void SetState(State state)
    {
        if(enabled == false && state != defaultState) return;
        _currentState.OnCompletedState(null);
        _currentState.OnUnset();
        _currentState = state;
        _currentState.OnCompletedState(_stateCompleted);
        _currentState.OnSet();
    }

    private void OnStateCompleted()
    {
        SetState(defaultState);
    }

    private void OnDisable()
    {
        /*for (var i = 0; i < alarmAttackEvents.Count; i++)
        {
            alarmAttackEvents[i].Alarmed -= AlarmAttackEventsOnAlarmed;
        }*/
    }

    public void Die()
    {
        _currentState.OnCompletedState(null);
        _currentState.OnUnset();
        deadState.OnSet();
        _currentState = deadState;
        enabled = false;
    }
}
