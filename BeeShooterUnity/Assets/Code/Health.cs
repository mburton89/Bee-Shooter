using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    SpriteRenderer spriteRenderer;
    Material initialMaterial;
    [SerializeField] Material whiteMaterial;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialMaterial = spriteRenderer.material;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;
        Flash();

        if (currentHealth <= 0)
        {
            Explode();
        }
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

    public void Explode()
    {
        ScreenShakeManager.Instance.ShakeScreen();
        Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
        Destroy(gameObject);

        FindObjectOfType<EnemyShipSpawner>().CountEnemyShips();
    }
}
