using System.Collections.Generic;
using UnityEngine;

public enum WithoutOdin
{
    BehaviourMovement,
    BehaviourRotation,
    BehaviourMoveFieldOfView,
    BehaviourRecurrentCheck,
    BehaviourMovementToDefault
}

//TODO брать заглушки на создание очереди, сделать чтобы можно было редактировать из инспектора
public class QueueStatePatrol : MonoBehaviour
{
    [SerializeField] private List<WithoutOdin> queueCompleteForSerialize;
    [SerializeField] private BehaviourMovement movement;
    [SerializeField] private BehaviourRotation rotation;
    [SerializeField] private BehaviourMoveFieldOfView behaviourMoveFieldOfView;
    [SerializeField] private BehaviourRecurrentCheck behaviourRecurrentCheck;
    [SerializeField] private BehaviourMovementToPath behaviourMovementToPath;
    [SerializeField] private PatrolState state;

    private List<DataCompleteBehaviourPatrol> queueComplete;
    
    private void Awake()
    {
        queueComplete = new List<DataCompleteBehaviourPatrol>();
        for (int i = 0; i < queueCompleteForSerialize.Count; i++)
        {
            switch (queueCompleteForSerialize[i])
            {
                case WithoutOdin.BehaviourMovementToDefault:
                {
                    if (behaviourMovementToPath != null)
                    {
                        queueComplete.Add(new DataCompleteBehaviourPatrol(behaviourMovementToPath,CompleteBehaviour.Part,false));
                    }
                    break;
                }
                case WithoutOdin.BehaviourMovement:
                {
                    if (movement != null)
                    {
                        queueComplete.Add(new DataCompleteBehaviourPatrol(movement,CompleteBehaviour.Part,false));
                    }

                    break;
                }
                case WithoutOdin.BehaviourRotation:
                {
                    if (rotation != null)
                    {
                        queueComplete.Add(new DataCompleteBehaviourPatrol(rotation,CompleteBehaviour.Part,false));
                    }

                    break;
                }
                case WithoutOdin.BehaviourMoveFieldOfView:
                {
                    if (behaviourMoveFieldOfView != null)
                    {
                        queueComplete.Add(new DataCompleteBehaviourPatrol(behaviourMoveFieldOfView,CompleteBehaviour.All,false));
                    }
                    break;
                }
                case WithoutOdin.BehaviourRecurrentCheck:
                {
                    if (behaviourRecurrentCheck != null)
                    {
                        queueComplete.Add(new DataCompleteBehaviourPatrol(behaviourRecurrentCheck,CompleteBehaviour.All,false));
                    }
                    break;
                }
            }
        }

        state.SetQueueComplete(queueComplete);
    }
}
