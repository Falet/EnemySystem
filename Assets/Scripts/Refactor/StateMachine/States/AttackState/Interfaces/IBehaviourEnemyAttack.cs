using System;

public interface IBehaviourEnemyAttack
{
    public void StartBehaviour();

    public void StopBehaviour();
    
    public void OnComplete(Action<IBehaviourEnemyAttack> callBack);
}
