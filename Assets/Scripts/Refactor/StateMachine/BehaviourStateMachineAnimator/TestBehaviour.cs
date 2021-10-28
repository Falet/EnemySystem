using UnityEngine;

public class TestBehaviour : SceneLinkedSMB<TestMonoBehaviourLinkedSMB>
{
    
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        m_MonoBehaviour.Test(stateInfo.length);
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);
    }
}