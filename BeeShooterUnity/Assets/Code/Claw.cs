using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Claw : MonoBehaviour
{
    public float fireRate;
    public float projectileSpeed;

    public bool canBangBang;
    public GameObject projectilePrefab;
    public Transform projectileSpawnpoint1;
    public Transform projectileSpawnpoint2;

    public Transform pivot;

    public float leftRotation;
    public float rightRotation;

    void Start()
    {
        StartCoroutine(RotateCo());
        StartCoroutine(FireProjectiles());
        
    }

    private IEnumerator RotateCo()
    {
        pivot.DORotate(new Vector3(0, 0, leftRotation), 1, RotateMode.Fast).SetEase(Ease.InOutQuad);

        yield return new WaitForSeconds(0.5f);

        GameObject projectile1 = Instantiate(projectilePrefab, projectileSpawnpoint1.position, transform.rotation);
        projectile1.GetComponent<Rigidbody2D>().AddForce(projectileSpawnpoint1.transform.up * projectileSpeed);
        Destroy(projectile1, 2);

        yield return new WaitForSeconds(0.5f);

        pivot.DORotate(new Vector3(0, 0, rightRotation), 1, RotateMode.Fast).SetEase(Ease.InOutQuad);

        yield return new WaitForSeconds(0.5f);

        GameObject projectile2 = Instantiate(projectilePrefab, projectileSpawnpoint2.position, transform.rotation);
        projectile2.GetComponent<Rigidbody2D>().AddForce(projectileSpawnpoint2.transform.up * projectileSpeed);
        Destroy(projectile2, 2);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(RotateCo());
    }

    private IEnumerator FireProjectiles()
    {



        yield return new WaitForSeconds(fireRate);
        StartCoroutine(FireProjectiles());
    }
}