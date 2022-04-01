using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(Animator))]
public class BaseEnemy : PoolableObject
{
    [SerializeField] BaseEnemyScriptable _settings;
    private int moneyValue;
    private float speed;
    private bool attacking;
    private HealthManager healthManager;
    private GameManager gm;
    private Coroutine currentActivityCoroutine;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        speed = _settings.baseSpeed;
    }

    private void OnEnable()
    {
        currentActivityCoroutine = StartCoroutine(MoveForward());
        attacking = false;
    }

    public void InitEnemy(float bonusHealth, float bonusSpeed, float bonusValue)
    {
        InitHealth(_settings.baseHealth * bonusHealth);
        moneyValue = Mathf.RoundToInt(_settings.baseValue * bonusValue);
        speed = _settings.baseSpeed * bonusSpeed;
    }

    private void InitHealth(float health)
    {
        healthManager.SetUp(health);
        healthManager.NoHealth += Die;
    }

    private void Die()
    {
        StopCoroutine(currentActivityCoroutine);
        attacking = false;
        gm.Die(moneyValue);
        animator.SetBool("Dies", true);
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        healthManager.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        attacking = true;
        StopCoroutine(currentActivityCoroutine);
        currentActivityCoroutine = StartCoroutine(AttackCoroutine(collision));
    }

    private IEnumerator MoveForward()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x - (Time.deltaTime * speed), transform.position.y, transform.position.z);
            yield return null;
        }
    }

    private IEnumerator AttackCoroutine(Collision2D collision)
    {
        while (true)
        {
            // perform attack
            print("attack");
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(_settings.enemyDamage);
            yield return new WaitForSeconds(_settings.attackCooldown);
        }
    }
}
