using DG.Tweening;
using UnityEngine;

//TODO вынести в отдельный интерфейс, так как вращение можно делать еще несколькими способами
public class Rotation : MonoBehaviour
{
    private void Awake()
    {
        /*DOTween.Init(true);//recycleAllByDefault	If TRUE all new tweens will be set for recycling,
                           //meaning that when killed, instead of being destroyed, they will be put in a pool and
                           //reused instead of creating new tweens. This option allows you to avoid GC allocations by
                           //reusing tweens, but you will have to take care of tween references, since they might result
                           //active even if they were killed (since they might have been respawned and are now being
                           //used for other tweens).*/
    }

    public Tweener LookAt(Vector3 target, float duration)
    {
        return transform.DOLookAt(target, duration, AxisConstraint.Y);
    }
    
    public Tweener SetRotate(Vector3 targetRotation, float duration)
    {
        return transform.DORotate(targetRotation, duration);
    }

    public Tweener TurnTo(Vector3 targetRotation, float duration)
    {
        var rotation = transform.localEulerAngles;
        targetRotation.x += rotation.x;
        targetRotation.y += rotation.y;
        targetRotation.z += rotation.z;
        return transform.DORotate(targetRotation, duration);
    }
}
