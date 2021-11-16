using System;

public interface IBehaviourStandOnLine
{
    public void StartBehaviour();

    public void StopBehaviour();
    
    public void OnComplete(Action callBack);
}