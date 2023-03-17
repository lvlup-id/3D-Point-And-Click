using UnityEngine;

public class Enemy : Character
{
    public enum State
    {
        Idle, Chase, Attack
    }

    public State currentState = State.Idle;

    private Animator anim;
    private float lastAttackTime;
    private float targetDistance;
    [SerializeField] private float chaseRange;

    private void Start()
    {
        target = Player.current;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (target == null) return;
        if (target.isDead)
        {
            target = null;
            controller.StopMovement();
            anim.SetBool("isMoving", false);
            return;
        }

        targetDistance = Vector3.Distance(transform.position, target.transform.position);

        switch (currentState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
        }
        anim.SetBool("isMoving", currentState == State.Chase);
    }

    void IdleUpdate()
    {
        controller.StopMovement();
        if (targetDistance < chaseRange && targetDistance > attackRange)
            SetState(State.Chase);
        else if (targetDistance < attackRange)
            SetState(State.Attack);
    }

    void ChaseUpdate()
    {
        if (targetDistance > chaseRange)
            SetState(State.Idle);
        else if (targetDistance < attackRange)
            SetState(State.Attack);
    }

    void AttackUpdate()
    {
        if (targetDistance > attackRange) SetState(State.Chase);

        controller.LookTowards(target.transform.position - transform.position);

        if (Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;
            anim.SetTrigger("isAttacking");
        }
    }

    void SetState(State newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case State.Idle:
            case State.Attack:
                controller.StopMovement();
                break;
            case State.Chase:
                controller.MoveToTarget(target.transform);
                break;
        }
    }

    public override void Die()
    {
        base.Die();
        anim.SetBool("isDead", true);
    }
}
