using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool isMoving = false;

    private float moveToUpdateRate = 0.1f;
    private float lastMoveToUpdate;
    private Transform moveTarget;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (moveTarget != null && Time.time - lastMoveToUpdate > moveToUpdateRate)
        {
            lastMoveToUpdate = Time.time;
            MoveToPosition(moveTarget.position);
        }
        isMoving = agent.velocity.magnitude > 0.1f;
    }

    public void MoveToTarget(Transform target)
    {
        moveTarget = target;
    }

    public void LookTowards(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
    }

    public void StopMovement()
    {
        agent.isStopped = true;
        isMoving = false;
    }
}
