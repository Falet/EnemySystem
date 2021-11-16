using UnityEngine;

public class InfoEnemy : MonoBehaviour
{
    public ColliderTriggered ColliderTriggered => colliderTriggered;

    [SerializeField] private ColliderTriggered colliderTriggered;
}