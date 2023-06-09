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
    public string autoWinKey;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;

    public bool canBangBang;
    ParticleSystem thrustParticles;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentArmor = maxArmor;
        currentAmmo = maxAmmo;
        thrustParticles = GetComponentInChildren<ParticleSystem>();
        //canBangBang = true;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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

    public void win()
    {
        GameManager.Instance.winGame();
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
        List<SoundName> hitSounds = new List<SoundName>();

        currentArmor -= DamageToGive;
        if (currentArmor <= 0)
        {
            Explode();

            if (this.GetType() == typeof(PlayerShip))
            {
                SoundManager.Instance.PlaySFXOnce(SoundName.PlayerDies, transform.position);
            }
            else
            {
                SoundManager.Instance.PlaySFXOnce(SoundName.EnemyDies, transform.position);
            }
        }
        

        if(GetComponent<PlayerShip>())
        {
            HUD.Instance.DisplayHealth(currentArmor, maxArmor);

            hitSounds.Add(SoundName.PlayerHit1);
            hitSounds.Add(SoundName.PlayerHit2);
            hitSounds.Add(SoundName.PlayerHit3);
        }
        else
        {
            hitSounds.Add(SoundName.EnemyHit1);
            hitSounds.Add(SoundName.EnemyHit2);
        }

        SoundManager.Instance.PlaySFXOnce(hitSounds[Random.Range(0, hitSounds.Count)]);

        Flash();
    }
    public void Explode()
    {// todo: Make particle effects
        
        ScreenShakeManager.Instance.ShakeScreen();
        //FindObjectOfType<EnemySpawner>().CountEnemyShips();
        

        if (GetComponent<PlayerShip>())
        {
            Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
            StartCoroutine(DelayGameOver());
        }
        else
        {
            Instantiate(Resources.Load("BOOM BOOM"), transform.position, transform.rotation);
            Destroy(gameObject);
        }
 
    }

    IEnumerator DelayGameOver()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<PlayerShip>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<CameraFollowPlayer>().enabled = false;

        AudioClip clip = SoundManager.Instance.GetBGMClip(SoundName.PlayerDies);
        SoundManager.Instance.PlayMainMusic(SoundName.PlayerDies);
        SoundManager.Instance.bgmMusicManager.loop = false;
        yield return new WaitForSeconds(clip.length - 1);

        GameManager.Instance.GameOver();
    }

    public void Flash()
    {
        StartCoroutine(FlashCo());
    }

    private IEnumerator FlashCo()
    {
        spriteRenderer.color = new Color(.42f, .14f, .78f);
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
    }
}

