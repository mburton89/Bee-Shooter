using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject projectilePrefab;
    public Transform projectileSpawnpoint;

    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float fireRate;
    public float projectileSpeed;
    public int currentAmmo = 10;
    public int maxAmmo;
    public int refillAmount;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;

    public bool canBangBang;
    ParticleSystem thrustParticles;

    private void Awake()
    {
        currentArmor = maxArmor;
        currentAmmo = maxAmmo;
        thrustParticles = GetComponentInChildren<ParticleSystem>();
        //canBangBang = true;

        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    public void Thrust()
    {
        rb.AddForce(transform.up * acceleration);
        thrustParticles.Emit(1);
    }

    public void BangBang()
    {
        if (canBangBang && currentAmmo > 0) //and ammo amount is more than 0
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnpoint.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
            currentAmmo -= 1;
            projectile.GetComponent<Projectile>().firingShip = gameObject;
            if (GetComponent<PlayerShip>())
            {
                HUD.Instance.DisplayAmmo(currentAmmo, maxAmmo);
            }
            Destroy(projectile, 4);
            StartCoroutine(FireRateBuffer());
        }
    }
    private IEnumerator FireRateBuffer()
    {
        canBangBang = false;
        yield return new WaitForSeconds(fireRate);
        canBangBang = true;

    }
    public void TakeDamage(int DamageToGive)
    {
        currentArmor -= DamageToGive;
        if (currentArmor <= 0)
        {
            Explode();
        }
        if(GetComponent<PlayerShip>())
        {
            HUD.Instance.DisplayHealth(currentArmor, maxArmor);
        }
    }
    public void Explode()
    {// todo: Make particle effects
        Instantiate(Resources.Load("BOOM BOOM"), transform.position, transform.rotation);
        ScreenShakeManager.Instance.ShakeScreen();
        //FindObjectOfType<EnemySpawner>().CountEnemyShips();
        Destroy(gameObject);

        if (GetComponent<PlayerShip>())
        {
            GameManager.Instance.GameOver();
        }
 
    } 
}

