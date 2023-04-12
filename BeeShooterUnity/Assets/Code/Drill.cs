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
}
