using System;
using System.Collections.Generic;

public class AlarmState : State
{
    private int i;
    private List<DataCompleteBehaviourAlarm> _queueComplete;
    private DataCompleteBehaviourAlarm currentElementQueue;
    private Action StateCompleted;

    public override void OnSet()
    {
        base.OnSet();
        i = 0;
        StartBehaviourOfElement();
    }
    
    public override void OnUnset()
    {
        base.OnUnset();
        currentElementQueue.Behaviour.StopBehaviour();
    }

    public void SetQueueComplete(List<DataCompleteBehaviourAlarm> queueComplete)
    {
        _queueComplete = queueComplete;
    }

    public override void OnCompletedState(Action callback)
    {
        base.OnCompletedState(callback);
        StateCompleted = callback;
    }

    private void CompleteUpdate()
    {
        currentElementQueue.Behaviour.OnComplete(null);
        currentElementQueue.Behaviour.StopBehaviour();
        StartBehaviourOfElement();
    }
    
    private void StartBehaviourOfElement()
    {
        if (i < _queueComplete.Count)
        {
            currentElementQueue = _queueComplete[i];
            
            currentElementQueue.Behaviour.StartBehaviour();
            
            currentElementQueue.Behaviour.OnComplete(CompleteUpdate);

            i++;
        }
        else
        {
            StateCompleted?.Invoke();
        }
    }

    private void OnDisable()
    {
        StateCompleted = null;
    }
}
