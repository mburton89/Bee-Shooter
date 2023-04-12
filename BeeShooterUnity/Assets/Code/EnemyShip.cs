using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    Transform target;



    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerShip>().transform;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canBangBang && collision.gameObject.GetComponent<PlayerShip>())
        {
            collision.gameObject.GetComponent<PlayerShip>().TakeDamage(1);
            Explode();
        }
    }
    // Update is called once per frame
    void Update()
    {
        FlyForwardTarget();
        if (canBangBang)
        {
            BangBang();
        }
    }
    void FlyForwardTarget()
    {
        if (target == null) return;
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = directionToFace;
        Thrust();
    }
    

    }
