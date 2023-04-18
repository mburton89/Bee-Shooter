using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageToGive;
    [HideInInspector]public GameObject firingShip;

    public AudioClip tic;

    void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>() && collision.gameObject != firingShip)
        {
            collision.GetComponent<Ship>().TakeDamage(damageToGive);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(tic, Camera.main.transform.position);
        }

        if (collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().TakeDamage(damageToGive);
            Destroy(gameObject);
        }
    }
}

