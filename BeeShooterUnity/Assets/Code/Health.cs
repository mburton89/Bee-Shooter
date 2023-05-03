using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        else
        {
            Drill drill = GetComponent<Drill>();
            if (drill != null)
            {   
                drill.HandleMusic();
            }

            DrillBase drillBase = GetComponent<DrillBase>();
            if (drillBase != null)
            {
                drillBase.HandleMusic();
            }
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

        //FindObjectOfType<EnemyShipSpawner>().CountEnemyShips();
    }
}
