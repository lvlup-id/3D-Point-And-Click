using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Color playerHealthColor;
    [SerializeField] private Color enemyHealthColor;
    private Character character;

    private void OnEnable()
    {
        character.onTakeDamage += UpdateHealthBar;
        character.onDie += HideHealthBar;
    }

    private void OnDisable()
    {
        character.onTakeDamage -= UpdateHealthBar;
        character.onDie -= HideHealthBar;
    }

    private void Awake()
    {
        character = GetComponentInParent<Character>();
        healthFill.color = transform.root.tag == "Player" ? playerHealthColor : enemyHealthColor;
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    private void UpdateHealthBar()
    {
        healthFill.fillAmount = (float)character.currentHP / (float)character.maxHP;
    }

    private void HideHealthBar()
    {
        gameObject.SetActive(false);
    }
}
