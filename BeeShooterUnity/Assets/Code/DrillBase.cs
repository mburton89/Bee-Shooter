using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBase : MonoBehaviour
{
    bool isMusicDistorted;
    public float drillDamagedMusicDistortTime = .75f;


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
