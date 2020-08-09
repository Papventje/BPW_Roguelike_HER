using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;

    [SerializeField]
    private GameObject powerup;

    public enum ObjectType { Player, Enemy };
    public ObjectType type;

    public void DoDamage(int damage)
    {
        health -= damage;
    }

    private void Update()
    {
        switch (type)
        {
            case ObjectType.Player:

                if (health <= 0)
                {
                    Destroy(gameObject);
                }

                break;
            case ObjectType.Enemy:

                if (health <= 0)
                {
                    if (gameObject.transform.parent.tag != "Enemy")
                    {
                        DropItem();
                        GetComponentInParent<RoomEnter>().currentEnemies.Remove(gameObject);
                        Destroy(gameObject);
                    }
                    else
                    {
                        DropItem();
                        GetComponentInParent<RoomEnter>().currentEnemies.Remove(transform.parent.gameObject);
                        Destroy(transform.parent.gameObject);
                    }
                }
                break;
        }
    }

    public void DropItem()
    {
        float dropChance = 1f / 4f;
        if(Random.Range(0f,1f) <= dropChance)
        {
            Instantiate(powerup, transform.position, Quaternion.identity);
        }
    }
}
