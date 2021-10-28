using System;
using DG.Tweening;
using UnityEngine;

public class AttackWithProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private float flightTimeToCharacter = 0.5f;
    
    public Action CompleteAttack;

    private TweenCallback _callbackMoveComplete;
    private TweenCallback _callbackScaleComplete;
    private Tweener _moveNet;
    private Tweener _scaleNet;
    private bool _moveNetTweenerIsComplete;
    private bool _scaleNetTweenerIsComplete;
    private Vector3 _defaultPosition;
    private Vector3 _startScale;
    private Vector3 _finalPosition;
    private Vector3 _finalScaleNet;
    private Transform transformProjectile;
    
    //TODO rework хуйня формулы,магические числа
    private void Start()
    {
        transformProjectile = projectile.transform;
        
        var distance = fieldOfView.GetDistanceFieldOfView;
        var angle = fieldOfView.GetAngelFieldOfView;
        var finalScale = 2 * distance * Mathf.Sin(Mathf.Deg2Rad * (angle/2)) * 8;
        _finalScaleNet = new Vector3(finalScale,finalScale, finalScale);
        var position = projectile.transform.position;
        _finalPosition = new Vector3(position.x, position.y, distance);
    }
    
    private void OnEnable()
    {
        _callbackMoveComplete = MoveIsComplete;
        _callbackScaleComplete = ScaleIsComplete;
    }

    public void Attack()
    {
        projectile.SetActive(true);
        
        _defaultPosition = transformProjectile.localPosition;
        _startScale = transformProjectile.localScale;

        _moveNet = transformProjectile.DOLocalMove(_finalPosition, flightTimeToCharacter);
        _moveNet.OnComplete(_callbackMoveComplete);

        _scaleNet = transformProjectile.DOScale(_finalScaleNet, flightTimeToCharacter);
        _scaleNet.OnComplete(_callbackScaleComplete);
    }

    public void Kill()
    {
        _moveNet?.Kill();
        _scaleNet?.Kill();

        SetAtStartState();
    }

    private void MoveIsComplete()
    {
        _moveNetTweenerIsComplete = true;

        AttackIsComplete();
    }
    
    private void ScaleIsComplete()
    {
        _scaleNetTweenerIsComplete = true;

        AttackIsComplete();
    }

    private void AttackIsComplete()
    {
        if(_moveNetTweenerIsComplete && _scaleNetTweenerIsComplete)
        {
            SetAtStartState();
                
            CompleteAttack?.Invoke();
        }
    }

    private void SetAtStartState()
    {
        projectile.SetActive(false);

        transformProjectile.localPosition = _defaultPosition;
        transformProjectile.localScale = _startScale;

        _moveNetTweenerIsComplete = false;
        _scaleNetTweenerIsComplete = false;
    }
    
    private void OnDisable()
    {
        _callbackMoveComplete = null;
        _callbackScaleComplete = null;
    }
}
