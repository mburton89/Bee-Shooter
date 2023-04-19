using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    bool wasBattleMusicPlayingLastFrame;
    public float battleMusicProximity;
    public string battleMusicObjectTag;


    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        HandleInput();
        HandleMusic();
    }
    void HandleInput()
    {
        if (Input.GetMouseButton(1))
        {
            Thrust();
        }

        if(Input.GetMouseButtonDown(0))
        {
            BangBang();
        }
        
    }

    void FollowMouse ()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector2 directionToFace = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = directionToFace;
    }


    void HandleMusic()
    {
        bool objectHit = false;
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, battleMusicProximity))
        {
            objectHit = objectHit || col.gameObject.CompareTag(battleMusicObjectTag);
        }

        if (objectHit)
        {
            if (!wasBattleMusicPlayingLastFrame)
            {
                SoundManager.Instance.PlayMainMusic(SoundName.DrillEngaged, false, false);
            }
        }
        else
        {
            if (SoundManager.Instance.GetCurrentBGMSoundName() != SoundName.MainBGM)
            {
                SoundManager.Instance.PlayMainMusic(SoundName.MainBGM, true, false);
            }
        }

        wasBattleMusicPlayingLastFrame = SoundManager.Instance.GetCurrentBGMSoundName() == SoundName.DrillEngaged;
    }

}
