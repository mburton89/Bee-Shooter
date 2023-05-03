using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honey : MonoBehaviour
{
    float changePerSecond; //how much we add per second while player is on flower (may change later)

    PlayerShip playerShip;
    public int usesLeft = 8;
    public float flowerScaleSize;
    public float scaleChangeValue;

    public List<SoundName> slurpSounds;

    private void Start()
    {
        playerShip = FindObjectOfType<PlayerShip>();

        if (slurpSounds.Count == 0)
        {
            slurpSounds.Add(SoundName.Slurp1);
            slurpSounds.Add(SoundName.Slurp2);
            slurpSounds.Add(SoundName.Slurp3);
            slurpSounds.Add(SoundName.Slurp4);
            slurpSounds.Add(SoundName.Slurp5);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>() && collision.gameObject.GetComponent<PlayerShip>().currentArmor < 10)
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
        for (int i = usesLeft; i > 0; i--)
        {
            print("Refill for loop");

            if (slurpSounds.Count > 0)
            {
                SoundManager.Instance.PlaySFXOnce(slurpSounds[Random.Range(0,slurpSounds.Count)]);
            }
            playerShip.currentArmor += 1;
            usesLeft -= 1;
            flowerScaleSize -= scaleChangeValue;
            transform.localScale = new Vector3(originalScale.x * flowerScaleSize, originalScale.y * flowerScaleSize, originalScale.z * flowerScaleSize);
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