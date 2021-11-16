using UnityEngine;

public class DataCompleteBehaviourStandOnLine
{
    public IBehaviourStandOnLine Behaviour { get; }

    public DataCompleteBehaviourStandOnLine(IBehaviourStandOnLine behaviour)
    {
        Behaviour = behaviour;
    }
}