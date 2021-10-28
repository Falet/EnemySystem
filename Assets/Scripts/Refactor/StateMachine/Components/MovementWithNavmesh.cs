using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//TODO вынести в отдельный интерфейс, так как передвижение еще можно делать несколькими способами
public class MovementWithNavmesh : MonoBehaviour
{
    public Action CompleteMovementToPoint;
    public float SpeedMovement => navMeshAgent.speed;

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float defaultSpeed = 3.5f;

    private Coroutine _onCompleteMovement;

    private void Awake()
    {
        navMeshAgent.speed = defaultSpeed;
    }

    public void CalculatePath(Vector3 targetPoint,NavMeshPath path)
    {
        navMeshAgent.CalculatePath(targetPoint, path);
    }
    
    public void SetDestination(Vector3 targetPosition, float speed)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(targetPosition);
        if (_onCompleteMovement != null)
        {
            StopCoroutine(_onCompleteMovement);
        }
        _onCompleteMovement = StartCoroutine(CompleteMovement());
    }

    public bool SetPath(NavMeshPath path, float speed)
    {
        if (navMeshAgent.SetPath(path) == false)
        {
            return false;
        }
        if (_onCompleteMovement != null)
        {
            StopCoroutine(_onCompleteMovement);
        }

        navMeshAgent.speed  = speed;
        _onCompleteMovement = StartCoroutine(CompleteMovement());
        return true;
    }

    public void Kill()
    {
        if (_onCompleteMovement != null)
        {
            StopCoroutine(_onCompleteMovement);
            navMeshAgent.ResetPath();
        }
    }
    
    private IEnumerator CompleteMovement()
    {
        while(true)
        {
            yield return null;
            if (navMeshAgent.pathPending == false)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        navMeshAgent.speed = defaultSpeed;
                        CompleteMovementToPoint?.Invoke();
                        yield break;
                    }
                }
            }
        }
    }
}
