using System.Collections.Generic;
using UnityEngine;

//TODO Возможно нужно сделать инитер состояния и интер поведений раздельно
public class InitializationStateStandOnLine : MonoBehaviour
{
    [SerializeField] private BehaviourStandOnLineFire behaviourStandOnLineFire;
    [SerializeField] private StandOnLineFireState standOnLineFireState;
    
    private List<DataCompleteBehaviourStandOnLine> queueComplete;
    
    public void Init(DataSinusiod sinusiodForMove)
    {
        behaviourStandOnLineFire.Init(sinusiodForMove);
        
        queueComplete = new List<DataCompleteBehaviourStandOnLine> {new DataCompleteBehaviourStandOnLine(behaviourStandOnLineFire)};
        
        standOnLineFireState.SetQueueComplete(queueComplete);
    }
}