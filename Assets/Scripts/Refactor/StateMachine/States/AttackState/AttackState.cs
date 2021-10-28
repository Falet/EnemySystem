using System;
using System.Collections.Generic;

public class AttackState : State
{
    private int i;
    private List<DataCompleteBehaviourAttack> _queueComplete;
    private DataCompleteBehaviourAttack _lastElementQueue;
    private List<IBehaviourEnemyAttack> _nonCompletedBehaviour;
    private Action StateCompleted;
    
    private void Start()
    {
        _nonCompletedBehaviour = new List<IBehaviourEnemyAttack>(_queueComplete.Count);
    }

    public void SetQueueComplete(List<DataCompleteBehaviourAttack> queueComplete)
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
        for (var j = 0; j < _nonCompletedBehaviour.Count; j++)
        {
            _nonCompletedBehaviour[j].StopBehaviour();
        }
        _nonCompletedBehaviour.Clear();
    }
    
    public override void OnCompletedState(Action callback)
    {
        base.OnCompletedState(callback);
        StateCompleted = callback;
    }

    private void CompleteUpdate(IBehaviourEnemyAttack behaviour)
    {
        behaviour.OnComplete(null);
        behaviour.StopBehaviour();
        _nonCompletedBehaviour.Remove(behaviour);
        if (_nonCompletedBehaviour.Count == 0)
        {
            StateCompleted?.Invoke();
        }
    }

    private void StartBehaviourOfElement()
    {
        for (; i < _queueComplete.Count; i++)
        {
            _lastElementQueue = _queueComplete[i];
        
            _lastElementQueue.Behaviour.StartBehaviour();
            
            _nonCompletedBehaviour.Add(_lastElementQueue.Behaviour);
            
            _lastElementQueue.Behaviour.OnComplete(CompleteUpdate);
            
            if (_lastElementQueue.StartNextBehaviour == false)
            {
                break;
            }
        }
    }
}
