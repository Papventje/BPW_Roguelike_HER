using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretEnemyState { Targeting, Attacking }

public class TurretEnemy : MonoBehaviour
{
    public int damage;

    private Transform target;

    Vector2 lookDir;

    Rigidbody2D rb;

    public GameObject bullet;
    public Transform muzzle;

    [Space]
    public GameObject particleEffect;

    [Space]
    public TurretEnemyState currentState;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(Attack());
    }

    private void Update()
    {
        switch (currentState)
        {
            case TurretEnemyState.Targeting:
                {
                    RotateToEnemy();
                }
                break;
            case TurretEnemyState.Attacking:
                {
                    Shoot();

                    StartCoroutine(Attack());
                    currentState = TurretEnemyState.Targeting;
                }
                break;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));

        currentState = TurretEnemyState.Attacking;
    }

    void RotateToEnemy()
    {
        if(target != null)
        {
            lookDir = new Vector2(target.position.x, target.position.y) - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;

            rb.rotation = angle;
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, muzzle.position, muzzle.rotation);

        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.up * 10, ForceMode2D.Impulse);
        rb.gameObject.GetComponent<Projectile>().damage = damage;
    }
}
