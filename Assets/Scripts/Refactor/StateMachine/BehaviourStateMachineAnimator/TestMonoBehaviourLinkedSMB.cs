using System;
using UnityEngine;

public class TestMonoBehaviourLinkedSMB : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        SceneLinkedSMB<TestMonoBehaviourLinkedSMB>.Initialise(animator,this);
    }

    public void Test(float value)
    {
        Debug.Log(value);
    }
}