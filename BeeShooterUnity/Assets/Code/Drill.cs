using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drill : MonoBehaviour
{
    public float distanceToMoveOut;
    public float secondsToMoveOut;
    public float secondsToPause;
    public float secondsToMoveIn;

    float initialYPosition;

    void Start()
    {
        initialYPosition = transform.localPosition.y;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        transform.DOLocalMoveY(initialYPosition + distanceToMoveOut, secondsToMoveOut, false);
        yield return new WaitForSeconds(secondsToMoveOut);
        yield return new WaitForSeconds(secondsToPause);
        transform.DOLocalMoveY(initialYPosition, secondsToMoveIn, false);
        yield return new WaitForSeconds(secondsToMoveIn);
        StartCoroutine(Animate());
    }
}
