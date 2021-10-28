public class DataCompleteBehaviourAttack
{
    public IBehaviourEnemyAttack Behaviour { get; }
    
    public bool StartNextBehaviour { get; }

    public DataCompleteBehaviourAttack(IBehaviourEnemyAttack behaviour, bool startNextBehaviour)
    {
        Behaviour = behaviour;
        StartNextBehaviour = startNextBehaviour;
    }
}
