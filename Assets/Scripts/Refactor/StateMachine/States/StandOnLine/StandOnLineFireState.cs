using System;
using System.Collections.Generic;

public class StandOnLineFireState : State
{
    private int _index;
    private List<DataCompleteBehaviourStandOnLine> _queueComplete;
    private DataCompleteBehaviourStandOnLine _currentElementQueue;
    private Action _stateCompleted;
    
    public void SetQueueComplete(List<DataCompleteBehaviourStandOnLine> queueComplete)
    {
        _queueComplete = queueComplete;
    }
    
    public override void OnSet()
    {
        base.OnSet();
        _index = 0;
        StartBehaviourOfElement();
    }

    public override void OnUnset()
    {
        base.OnUnset();
        _currentElementQueue?.Behaviour.StopBehaviour();
    }

    public override void OnCompletedState(Action callback)
    {
        base.OnCompletedState(callback);
        _stateCompleted = callback;
    }
    
    private void StartBehaviourOfElement()
    {
        if (_index < _queueComplete.Count)
        {
            _currentElementQueue = _queueComplete[_index];
            
            _currentElementQueue.Behaviour.StartBehaviour();
            
            _index++;
        }
        else
        {
            _stateCompleted?.Invoke();
        }
    }
}