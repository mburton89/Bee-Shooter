using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drill : MonoBehaviour
{
    public float distanceToMoveOut;
    public float secondsToMoveOut;
    public float secondsToPauseIn;
    public float secondsToPauseOut;
    public float secondsToMoveIn;

    public float secondsToDelay;

    float initialYPosition;

    public float drillDamagedMusicDistortTime = 1.25f;
    bool isMusicDistorted = false;


    void Start()
    {
        initialYPosition = transform.localPosition.y;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(secondsToDelay);
        secondsToDelay = 0;

        transform.DOLocalMoveY(initialYPosition + distanceToMoveOut, secondsToMoveOut, false);
        yield return new WaitForSeconds(secondsToMoveOut);
        yield return new WaitForSeconds(secondsToPauseIn);
        transform.DOLocalMoveY(initialYPosition, secondsToMoveIn, false);
        yield return new WaitForSeconds(secondsToMoveIn);
        yield return new WaitForSeconds(secondsToPauseOut);
        StartCoroutine(Animate());
    }


    //call when drill damaged
    public void HandleMusic()
    {
        if (!isMusicDistorted)
        {
            StartCoroutine(DistortMusic());
        }
    }

    private IEnumerator DistortMusic()
    {
        if (SoundManager.Instance.GetCurrentBGMSoundName() == SoundName.DrillEngaged)
        {
            isMusicDistorted = true;

            SoundManager.Instance.PlayMainMusic(SoundName.DrillEngagedDistorted, false, true);

            yield return new WaitForSeconds(drillDamagedMusicDistortTime);

            SoundManager.Instance.PlayMainMusic(SoundName.DrillEngaged, false, true);

        }
        isMusicDistorted = false;
    }


}
