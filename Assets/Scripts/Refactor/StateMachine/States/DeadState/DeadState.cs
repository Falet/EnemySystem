using System.Collections.Generic;

public class DeadState : State
{
    private int i;
    private List<DataCompleteBehaviourDead> _queueComplete;
    private DataCompleteBehaviourDead _lastElementQueue;
    private List<IBehaviourDead> _nonCompletedBehaviour;
    
    public void SetQueueComplete(List<DataCompleteBehaviourDead> queueComplete)
    {
        _queueComplete = queueComplete;
        _nonCompletedBehaviour = new List<IBehaviourDead>(_queueComplete.Count);
    }

    public override void OnSet()
    {
        base.OnSet();
        i = 0;
        StartBehaviours();
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

    private void StartBehaviours()
    {
        for (; i < _queueComplete.Count; i++)
        {
            _lastElementQueue = _queueComplete[i];
        
            _lastElementQueue.Behaviour.StartBehaviour();
            
            _nonCompletedBehaviour.Add(_lastElementQueue.Behaviour);
        }
    }
}