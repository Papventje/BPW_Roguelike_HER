using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    GameObject player;

    PlayerMovement movement;
    Shooting shooting;

    PlayerInventory inventory;

    public enum PlayerPowerUp { MovementPowerUp, DashDistancePowerUp, ChargingRatePowerUp, BulletSizePowerUp };

    [Header("PowerUp Type")] //Indicator which type of power up the object represents
    public PlayerPowerUp playerPU;

    [Header("PowerUp Increase")] //Power up increments

    [SerializeField]
    private float movementPUBonus;

    [SerializeField]
    private float dashPUBonus;

    [SerializeField]
    private float chargingRatePUBonus;

    [SerializeField]
    private float bulletSizePUBonus;

    [Header("PowerUp Sprites")] //Sprites used for power ups

    [SerializeField]
    private Sprite movementSprite;

    [SerializeField]
    private Sprite dashSprite;

    [SerializeField]
    private Sprite chargingRateSprite;

    [SerializeField]
    private Sprite bulletSizeSprite;

    private SpriteRenderer[] renderers;
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        playerPU = (PlayerPowerUp)Random.Range(0, System.Enum.GetValues(typeof(PlayerPowerUp)).Length);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        movement = player.GetComponent<PlayerMovement>();
        shooting = player.GetComponent<Shooting>();

        inventory = GameObject.Find("Inventory").GetComponent<PlayerInventory>();

        //playerPU = (PlayerPowerUp)Random.Range(0, 3);

        renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer.gameObject.transform.parent != null)
            {
                spriteRenderer = renderer;
            }
        }

        switch (playerPU) 
        {
            case PlayerPowerUp.MovementPowerUp:

                spriteRenderer.sprite = movementSprite;

                break;
            case PlayerPowerUp.DashDistancePowerUp:

                spriteRenderer.sprite = dashSprite;

                break;
            case PlayerPowerUp.ChargingRatePowerUp:

                spriteRenderer.sprite = chargingRateSprite;

                break;
            case PlayerPowerUp.BulletSizePowerUp:

                spriteRenderer.sprite = bulletSizeSprite;

                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (playerPU)
            {
                case PlayerPowerUp.MovementPowerUp:

                    movement.normalSpeed += movementPUBonus;
                    movement.chargeSpeed += movementPUBonus / 2;

                    inventory.movementPUAmount++;

                    break;
                case PlayerPowerUp.DashDistancePowerUp:

                    movement.dashDistance += dashPUBonus;

                    inventory.dashPUAmount++;

                    break;
                case PlayerPowerUp.ChargingRatePowerUp:

                    shooting.chargeSpeed += chargingRatePUBonus;

                    inventory.chargePUAmount++;

                    break;
                case PlayerPowerUp.BulletSizePowerUp:

                    shooting.bulletSize += bulletSizePUBonus;

                    inventory.bulletPUAmount++;

                    break;
            }
            Destroy(gameObject);
        }
        
    }
}
