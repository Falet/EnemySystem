using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private int i;
    private List<DataCompleteBehaviourPatrol> _queueComplete;
    private DataCompleteBehaviourPatrol currentElementQueue;
    private Action stateCompleted;
    
    public void SetQueueComplete(List<DataCompleteBehaviourPatrol> queueComplete)
    {
        _queueComplete = queueComplete;
    }

    public override void OnSet()
    {
        base.OnSet();
        i = 0;
        StartBehaviourOfElement();
    }

    public override void OnUnset()
    {
        base.OnUnset();
        currentElementQueue?.Behaviour.StopBehaviour();
    }

    public override void OnCompletedState(Action callback)
    {
        base.OnCompletedState(callback);
        stateCompleted = callback;
    }

    private void AllCompleteUpdate()
    {
        currentElementQueue.Behaviour.OnAllComplete(null);
        currentElementQueue.Behaviour.StopBehaviour();
        StartBehaviourOfElement();
    }

    private void PartCompleteUpdate()
    {
        currentElementQueue.Behaviour.OnPartComplete(null);
        currentElementQueue.Behaviour.StopBehaviour();
        StartBehaviourOfElement();
    }


    private void StartBehaviourOfElement()
    {
        if (i < _queueComplete.Count)
        {
            currentElementQueue = _queueComplete[i];
            
            currentElementQueue.Behaviour.StartBehaviour(currentElementQueue.StartOver);
            
            switch (currentElementQueue.CompleteBehaviour)
            {
                case CompleteBehaviour.Part:
                    currentElementQueue.Behaviour.OnPartComplete(PartCompleteUpdate);
                    break;
                case CompleteBehaviour.All:
                    currentElementQueue.Behaviour.OnAllComplete(AllCompleteUpdate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            i++;
        }
        else
        {
            stateCompleted?.Invoke();
        }
    }
}
