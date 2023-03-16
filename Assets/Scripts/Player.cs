using UnityEngine;

public class Player : Character
{
    public static Player current;

    private Animator anim;
    private float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();
        current = this;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (target != null && !target.isDead)
        {
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (targetDistance < attackRange)
            {
                controller.StopMovement();
                controller.LookTowards(target.transform.position - transform.position);

                // Check attack speed
                if (Time.time - lastAttackTime > attackRate)
                {
                    lastAttackTime = Time.time;
                    anim.SetTrigger("isAttacking");
                }
            }
            else
            {
                controller.MoveToTarget(target.transform);
            }
        }

        anim.SetBool("isMoving", controller.isMoving);
    }

    public override void Die()
    {
        base.Die();
        anim.SetBool("isDead", true);
    }
}
