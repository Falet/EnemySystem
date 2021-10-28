using System;
using UnityEngine;

public class CharacterDetection : MonoBehaviour
{
    public Action CharacterDetected;
    public bool CharacterIsDetect
    {
        private set
        {
            if (_characterIsDetect != value)
            {
                _characterIsDetect = value;
                if (value)
                {
                    CharacterDetected?.Invoke();
                }
            }
        }
        get => _characterIsDetect;
    }

    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private Transform characterPosition;
    [SerializeField] private Transform followTransform;

    private float _distanceFieldOfView;
    private float _angelFieldOfView;
    private readonly RaycastHit[] _hitsCache = new RaycastHit[20];
    private bool _characterIsDetect;

    private void Start()
    {
        _distanceFieldOfView = _fieldOfView.GetDistanceFieldOfView;
        _angelFieldOfView = _fieldOfView.GetAngelFieldOfView;
    }
    
    private void Update()
    {
        if (!_fieldOfView.gameObject.activeSelf)
        {
            return;
        }
        var characterPos = characterPosition.position;
        var originPos = followTransform.position;
        var distance = Vector3.Distance(originPos, characterPos);
        var direction = (characterPos - originPos).normalized;
        var angle = Vector3.Angle(direction, followTransform.forward);
        
        Debug.DrawRay(originPos, direction * _distanceFieldOfView, Color.green);

        if (distance > _distanceFieldOfView || angle > _angelFieldOfView / 2)
        {
            CharacterIsDetect = false;
            return;
        }
        
        var size = Physics.RaycastNonAlloc(originPos, direction, _hitsCache, distance);

        if (size <= 0)
            return;

        for (var i = 0; i < size; i++)
        {
            var hit = _hitsCache[i];
            var layer = hit.transform.gameObject.layer;
            if (layer == LayerMask.NameToLayer("Wall") || layer == LayerMask.NameToLayer("BWall") ||
                layer == LayerMask.NameToLayer("StealthLocker"))
            {
                CharacterIsDetect = false;
                return;
            }

            if (layer != LayerMask.NameToLayer("Player"))
            {
                continue;
            }
            
            CharacterIsDetect = true;
        }
    }
}
