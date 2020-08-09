using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int movementPUAmount, dashPUAmount, chargePUAmount, bulletPUAmount;

    [Space]
    [SerializeField]
    private Image movementImage, dashImage, chargeImage, bulletImage;

    [Space]
    [SerializeField]
    private Text movementText, dashText, chargeText, bulletText;

    void DisplayAmount(Text text, Image image, int amount)
    {
        text.text = "" + amount;
        if (amount == 0)
            image.enabled = false;
        else
            image.enabled = true;
    }

    private void Update()
    {
        DisplayAmount(movementText, movementImage, movementPUAmount);
        DisplayAmount(dashText, dashImage, dashPUAmount);
        DisplayAmount(chargeText, chargeImage, chargePUAmount);
        DisplayAmount(bulletText, bulletImage, bulletPUAmount);
    }

}
