using System;
public interface IBehaviourAlarmEnemy
{
    public void StartBehaviour();

    public void StopBehaviour();
    
    public void OnComplete(Action callBack);
}
