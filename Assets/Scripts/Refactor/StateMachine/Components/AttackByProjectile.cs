using DG.Tweening;
using UnityEngine;

public class AttackByProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    private Tweener _projectileFly;
    private GameObject _projectileObject;
    
    public void Fire(Transform from,Transform target, float duration)
    {
        _projectileObject = Instantiate(projectile);

        var transform = _projectileObject.transform;
        transform.position = from.position;
        _projectileFly = transform.DOMove(target.position, duration);
        _projectileFly.OnComplete(CompletedFly);
    }

    private void CompletedFly()
    {
        //_projectileObject.SetActive(false);
    }
}