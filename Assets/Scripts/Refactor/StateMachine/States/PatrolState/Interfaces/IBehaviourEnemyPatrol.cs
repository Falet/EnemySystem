using System;
public interface IBehaviourEnemyPatrol
{
    public void StartBehaviour(bool startOver);

    public void StopBehaviour();

    //TODO Переосмыслить эти два эвента, ибо где то нет частичного выполнения
    public void OnPartComplete(Action callBack);

    public void OnAllComplete(Action callBack);
}
