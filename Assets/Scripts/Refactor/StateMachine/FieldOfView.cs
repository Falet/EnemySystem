using System;
using UnityEngine;

public class FieldOfView : MonoBehaviour 
{
    [SerializeField] private LayerMask _layerMaskWall;
    [SerializeField] private Vector3 _origin = Vector3.zero;
    [SerializeField] private int _angelFieldOfView = 30;
    [SerializeField] private float _viewDistance = 10;
    [SerializeField] private int _rayCount = 10;
    [SerializeField] private Transform parentAngle;
    
    public float GetDistanceFieldOfView => _viewDistance;
    public float GetAngelFieldOfView => _angelFieldOfView;
    
    private float _rotationParent;
    private Mesh _mesh;
    
    
    private void Awake()
    {
        _rotationParent = parentAngle.localEulerAngles.y;
        _vertices = new Vector3[_rayCount + 2];
        _uv = new Vector2[_rayCount + 2];
        _triangles = new int[_rayCount * 3];
    }

    public void Start()
    {
        Quaternion target = Quaternion.Euler(-90, _rotationParent - 90 - _angelFieldOfView/2, 0);
        transform.rotation = target;
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private Vector3[] _vertices;
    private Vector2[] _uv;
    private int[] _triangles;
    private void LateUpdate()
    {
        var angle = 0f;
        var angleIncrease = _angelFieldOfView / _rayCount;
        
        
        _vertices[0] = _origin;

        var vertexIndex = 1;
        var triangleIndex = 0;
        for (int i = 0; i <= _rayCount; i++)
        {
            Vector3 vertex;
            
            var raycast = Physics.Raycast(transform.position, transform.TransformDirection(GetVectorFromAngle(angle)), out var hit, _viewDistance, _layerMaskWall);
            DrawRayCast(hit, angle, raycast);
            if (!raycast)
            {
                vertex = _origin + GetVectorFromAngle(angle) * _viewDistance;
            }
            else
            {
                vertex = _origin + GetVectorFromAngle(angle) * hit.distance;
            }
            _vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                _triangles[triangleIndex + 0] = 0;
                _triangles[triangleIndex + 1] = vertexIndex - 1;
                _triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle += angleIncrease;
        }

        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;
    }
    private bool RaycastCustom(out RaycastHit hit, float angle)
    {
        var pos = transform.position;
        var direction = transform.TransformDirection(GetVectorFromAngle(angle));
        
        var raycast1 = Physics.Raycast(pos, direction,
            out var hit1, _viewDistance, _layerMaskWall);
        var raycast2 = Physics.Raycast(pos, direction,
            out var hit2, _viewDistance, 0);

        if (raycast1 && raycast2)
        {
            hit = hit1.distance > hit2.distance ? hit2 : hit1;
            return true;
        }

        if (raycast1)
        {
            hit = hit1;
            return true;
        }

        if (raycast2)
        {
            hit = hit2;
            return true;
        }
        hit = new RaycastHit();
        return false;
    }

    private Vector3 GetVectorFromAngle(float angle) {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    
    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    
    private void DrawRayCast(RaycastHit hit, float angle, bool raycast)
    {
        if (raycast)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(GetVectorFromAngle(angle)) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(GetVectorFromAngle(angle)) * _viewDistance, Color.white);
        }
    }
    


}
