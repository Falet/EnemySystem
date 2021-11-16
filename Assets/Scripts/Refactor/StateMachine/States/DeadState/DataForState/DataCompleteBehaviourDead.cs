public class DataCompleteBehaviourDead
{
    public IBehaviourDead Behaviour { get; }

    public DataCompleteBehaviourDead(IBehaviourDead behaviour)
    {
        Behaviour = behaviour;
    }
}