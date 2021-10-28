using System;
using UnityEngine;

//TODO: Полностью убрать моно
//попробовать перенести все состояния с моно на xNode
public abstract class State : MonoBehaviour
{
    public virtual void OnSet() { }
    public virtual void TransitionTo(State nextState) { }
    public virtual void OnUnset() { }
    public virtual void OnCompletedState(Action callback) {}
}
