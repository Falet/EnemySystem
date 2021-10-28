public class DataCompleteBehaviourPatrol
{
    public IBehaviourEnemyPatrol Behaviour { get; }
    public CompleteBehaviour CompleteBehaviour { get; }
    public bool StartOver { get; }

    public DataCompleteBehaviourPatrol(IBehaviourEnemyPatrol behaviour, CompleteBehaviour completeBehaviour,bool startOver)
    {
        Behaviour = behaviour;
        CompleteBehaviour = completeBehaviour;
        StartOver = startOver;
    }
}
