using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerRefillAmmo : MonoBehaviour
{
    float changePerSecond; //how much we add per second while player is on flower (may change later)

    PlayerShip playerShip;

    private void Start()
    {
        playerShip = FindObjectOfType<PlayerShip>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>() && collision.gameObject.GetComponent<PlayerShip>().currentAmmo < 10)
        {
            StartCoroutine("Refill");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerShip>())
        {
            StopCoroutine("Refill");
        }
    }
    IEnumerator Refill()
    {
        print("Refill");
        for (int i = playerShip.currentAmmo; playerShip.currentAmmo <= 10; i++)
        {
            print("Refill for loop");
            playerShip.currentAmmo += 1;
            HUD.instance.DisplayAmmo(playerShip.currentAmmo, playerShip.maxAmmo);
            yield return new WaitForSeconds(1);
        }
    }
}

