using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LaunchEnemyState { Targeting, Attacking }

public class LaunchEnemy : MonoBehaviour
{
    private Transform target;

    public int damage;

    [Space]
    [SerializeField]
    private GameObject body;
 
    [SerializeField]
    private Transform rightHandOrigin, leftHandOrigin;

    [SerializeField]
    private GameObject rightHand, leftHand;

    Vector2 lookDir;

    Rigidbody2D rb;

    [Space]
    public GameObject particleEffect;

    [Space]
    public LaunchEnemyState currentState;

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
            case LaunchEnemyState.Targeting:
                {
                    RotateToEnemy();

                    body.transform.Translate(Vector3.up * Time.deltaTime);
                }
                break;
            case LaunchEnemyState.Attacking:
                {
                    rightHand.transform.position = rightHandOrigin.position;
                    leftHand.transform.position = leftHandOrigin.position;
                }
                break;
        }
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

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2.5f));

        rb.AddForce(lookDir * 2, ForceMode2D.Impulse);
        currentState = LaunchEnemyState.Attacking;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            Instantiate(particleEffect, body.transform.position, transform.rotation);
            GetComponentInChildren<Health>().DropItem();
            GetComponentInParent<RoomEnter>().currentEnemies.Remove(gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Health>() != null)
            {
                other.gameObject.GetComponent<Health>().DoDamage(damage);
            }
            Instantiate(particleEffect, body.transform.position, transform.rotation);
            GetComponentInChildren<Health>().DropItem();
            GetComponentInParent<RoomEnter>().currentEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
