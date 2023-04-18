using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class screenShakeManager : MonoBehaviour
{
    public static screenShakeManager Instance;
    public float duration;
    public float strength;
    public int vibrato;
    public float randomness;


    private void Awake()
    {
        Instance = this;
    }

    public void ShakeScreen ()
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness, false, true);
    }
}
