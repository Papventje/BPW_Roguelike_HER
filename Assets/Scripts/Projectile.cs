using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string[] tags;

    public int damage;

    public GameObject particleEffect;

    private void Start()
    {
        Destroy(gameObject, 1.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (other.gameObject.CompareTag(tags[i]))
            {
                if (other.gameObject.GetComponent<Health>() != null)
                {
                    other.gameObject.GetComponent<Health>().DoDamage(damage);
                }
                Instantiate(particleEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Instantiate(particleEffect, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
