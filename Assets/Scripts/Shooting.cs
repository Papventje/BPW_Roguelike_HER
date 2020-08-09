using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    PlayerMovement movementClass;

    public Transform muzzle;
    public GameObject bullet;

    private GameObject[] indicators;

    [SerializeField]
    private float bulletForce;
    private float minBulletForce = 2f;
    private float maxBulletForce = 20f;

    public float chargeSpeed;
    public float bulletSize;

    [Space]
    [Header("Bullet Damage")]
    private int damage;
    [SerializeField]
    private int[] chargeDamage;


    private void Start()
    {
        movementClass = GetComponent<PlayerMovement>();

        bulletForce = minBulletForce;
        indicators = GameObject.FindGameObjectsWithTag("Indicators");

        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ChargeBullet();

            movementClass.currentState = PlayerState.Charging;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Shoot(bulletForce);

            movementClass.currentState = PlayerState.Normal;

            bulletForce = minBulletForce;
        }

        ShowChargingIndicators();
    }

    void ChargeBullet()
    {
        if(bulletForce < maxBulletForce)
        {
            bulletForce += chargeSpeed;
        }

        if (bulletForce > maxBulletForce)
        {
            bulletForce = maxBulletForce;
        }
    }

    void Shoot(float speed)
    {
        GameObject go = Instantiate(bullet, muzzle.position, muzzle.rotation);

        go.transform.localScale = new Vector3(1.2f + bulletSize * (speed / 15), 2.2f + bulletSize * (speed / 15), 1);

        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        rb.gameObject.GetComponent<Projectile>().damage = damage;
    }

    void ShowChargingIndicators()
    {
        if(bulletForce >= 5f)
        {
            indicators[0].SetActive(true);
            damage = chargeDamage[0];
        }
            
        if(bulletForce >= 12.5f)
        {
            indicators[1].SetActive(true);
            damage = chargeDamage[1];
        }
            
        if(bulletForce >= 20f)
        {
            indicators[2].SetActive(true);
            damage = chargeDamage[2];
        }


        if (bulletForce < 5)
        {
            indicators[0].SetActive(false);
            indicators[1].SetActive(false);
            indicators[2].SetActive(false);
        }
    }
}
