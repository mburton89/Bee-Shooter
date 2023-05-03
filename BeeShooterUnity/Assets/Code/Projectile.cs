using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageToGive;
    [HideInInspector]public GameObject firingShip;
    public SoundName soundOnFire;
    public SoundName soundOnHit;

    void Awake()
    {
        SoundManager.Instance.PlaySFXOnce(soundOnFire, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>() && collision.gameObject != firingShip)
        {
            collision.GetComponent<Ship>().TakeDamage(damageToGive);
            Destroy(gameObject);

            SoundManager.Instance.PlaySFXOnce(soundOnHit, transform.position);

        }

        if (collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().TakeDamage(damageToGive);
            Destroy(gameObject);
        }
    }
}

