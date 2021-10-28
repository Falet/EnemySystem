public class DataCompleteBehaviourAlarm
{
    public IBehaviourAlarmEnemy Behaviour { get; }

    public DataCompleteBehaviourAlarm(IBehaviourAlarmEnemy behaviour)
    {
        Behaviour = behaviour;
    }
}