using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    public int currentHP;
    public int maxHP;
    public int damage;
    [SerializeField] protected float attackRange, attackRate;

    [Header("Components")]
    public GameObject healthbarPrefab;
    public Character target;
    public bool isDead;
    [HideInInspector] public CharacterController controller;

    public event UnityAction onTakeDamage;
    public event UnityAction onDie;

    public delegate void OnTakeDamageD(float newHp);

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        healthbarPrefab = Instantiate(
            healthbarPrefab,
            transform.position + healthbarPrefab.transform.position,
            Quaternion.identity,
            transform
        );
    }

    public virtual void SetTarget(Character t)
    {
        target = t;
    }

    public virtual void TakeDamage(int value)
    {
        currentHP -= value;
        onTakeDamage?.Invoke();

        if (currentHP <= 0) Die();
    }

    public virtual void AttackTarget()
    {
        if (target != null)
            target.TakeDamage(damage);
    }

    public virtual void Die()
    {
        isDead = true;
        target = null;
        controller.StopMovement();
        onDie?.Invoke();
        Destroy(gameObject, 3f);
    }
}
