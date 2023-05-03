using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public float fireRate;
    public float projectileSpeed;
    public int currentAmmo = 10;
    public int maxAmmo;
    public int refillAmount;

    public bool canBangBang;
    public GameObject projectilePrefab;
    public Transform projectileSpawnpoint;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canBangBang)
        {
            BangBang();
        }
    }
}