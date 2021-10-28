using UnityEngine;
public class DeadState : State
{
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private EnemyAnimationController animationController;
    public override void OnSet()
    {
        base.OnSet();
        fieldOfView.gameObject.SetActive(false);
        animationController.PlayAnimationDeath();
    }

    public override void OnUnset()
    {
        base.OnUnset();
    }
}