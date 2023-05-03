using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honey : MonoBehaviour
{
    float changePerSecond; //how much we add per second while player is on flower (may change later)

    PlayerShip playerShip;
    public int maxUses = 8;
    int usesLeft;
    public float flowerScaleSize;
    public float scaleChangeValue;

    private void Start()
    {
        playerShip = FindObjectOfType<PlayerShip>();
        usesLeft = maxUses;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>() && collision.gameObject.GetComponent<PlayerShip>().currentArmor < playerShip.maxArmor)
        {
            StartCoroutine("Heal");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerShip>())
        {
            StopCoroutine("Heal");
           
        }
    }
    IEnumerator Heal()
    {
        Vector3 originalScale = transform.localScale;

        print("Heal");
        for (int i = 0; i < usesLeft; i++)
        {
            print("Refill for loop");
            playerShip.currentArmor += 1;
            usesLeft -= 1;
            flowerScaleSize = (float)usesLeft / (float)maxUses;
            transform.localScale = new Vector3(flowerScaleSize, flowerScaleSize, flowerScaleSize);
            //theHoney.transform.localScale *= flowerScaleSize;

            HUD.Instance.DisplayHealth(playerShip.currentArmor, playerShip.maxArmor);
            if (usesLeft <= 0)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1);
            
           
            // flowerScaleSize = Mathf.MoveTowards(flowerScaleSize, flowerScaleSize - scaleChangeValue, Time.deltaTime * shrinkSpeed);
            //Honey.transform.localScale = new Vector3(flowerScaleSize, flowerScaleSize, flowerScaleSize);

        }
    }
}