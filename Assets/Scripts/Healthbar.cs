using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    Health health;

    [SerializeField]
    Image fill;

    private void Start()
    {
    }

    private void Update()
    {
        float currentHealth;
        currentHealth = (float)health.health;
        fill.fillAmount = currentHealth / 100;
    }

}
