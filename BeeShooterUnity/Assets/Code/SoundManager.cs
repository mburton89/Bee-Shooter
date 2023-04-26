﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class SoundManager : MonoBehaviour {

    public Camera cam;
    public AudioSource bgmMusicManager;
    private AudioSource[] sfxManager;
    public AudioSource narrationManager;
    private AudioSource[] bgmAdditionalLayers;
    public float masterVolume;
    public float bgmVolume;
    public float narrationVolume;
    public float sfxVolumeMultiplier;
    public float fadeTime;
    private int bgmMusicIdentifier; 
    private bool isMusicSwitching = false;
    public float minMusicPlayDuration;
    public bool isSound3D;
    public bool usePositionalVolume;
    public bool speedUpSFXByTimescale;
    float sfxPitchMultiplier = 1;
    public bool isIntro;
    public int postIntroIdentifier;


    public List<Sound> bgmLibrary;
    public List<Sound> sfxLibrary;
    public List<Sound> narrationLibrary;

    [System.Serializable]
    public class Sound
    {
        public SoundName soundName;
        public AudioClip audioClip;
    }

    public void SaveSoundPrefs(){
        BinaryFormatter formatter = new BinaryFormatter ();
        FileStream file = File.Open (Application.persistentDataPath + "/soundManager.dat", FileMode.OpenOrCreate);

        SoundPrefs data = new SoundPrefs ();
        data.masterVolume = masterVolume;
        data.bgmVolume = bgmVolume;
        data.narrationVolume = narrationVolume;
        data.sfxVolumeMultiplier = sfxVolumeMultiplier;
        data.bgmMusicIdentifier = bgmMusicIdentifier;

        formatter.Serialize (file, data);
        file.Close();
    }

    public void LoadSoundPrefs(){

        if (File.Exists(Application.persistentDataPath + "/soundManager.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter ();
            FileStream file = File.Open (Application.persistentDataPath + "/soundManager.dat", FileMode.Open);
            SoundPrefs data = (SoundPrefs)formatter.Deserialize (file);
            file.Close();

            masterVolume = data.masterVolume;
            bgmVolume = data.bgmVolume;
            narrationVolume = data.narrationVolume;
            sfxVolumeMultiplier = data.sfxVolumeMultiplier;
            bgmMusicIdentifier = data.bgmMusicIdentifier;
        }
    }

    public static SoundManager Instance { get; private set; }
    
    
    public void Start()
    {
        SetCamera(cam);

        sfxManager = transform.Find("SFXSources").GetComponentsInChildren<AudioSource>();
        bgmAdditionalLayers = transform.Find("LayeredBGMAudioSources").GetComponentsInChildren<AudioSource>();
        LoadSoundPrefs ();

        bgmMusicManager.volume = bgmVolume * masterVolume;
        narrationManager.volume = narrationVolume * masterVolume;
        foreach (AudioSource aud in bgmAdditionalLayers)
        {
            aud.volume = bgmMusicManager.volume * .75f;
        }

        
    }

    private void Update()
    {
        if (isIntro && bgmMusicManager.time >= bgmMusicManager.clip.length * .9999f)
        {
            isIntro = false;
            SetMainGameMusicIdentifier(postIntroIdentifier);
            PlayMainMusicNoFade(false);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    //Anytime you switch cameras, set the camera to make sure spatial sound functions properly.
    public void SetCamera(Camera camera = null)
    {
        if (camera == null)
        {
            cam = Camera.main;
        }
        else
        {
            cam = camera;
        }
    }

    public void adjustMasterVolume(float newVolume)
    {
        masterVolume = newVolume;
    }


    #region BGM
    public void adjustBGMVolume(float newVolume)
    {
        bgmVolume = newVolume;
        bgmMusicManager.volume = bgmVolume * masterVolume;

        foreach (AudioSource aud in bgmAdditionalLayers)
        {
            aud.volume = bgmMusicManager.volume;
        }

    }

    public float GetBGMTimeIndex()
    {
        return bgmMusicManager.time;
    }

    public AudioClip GetAudioClip(SoundName soundName, SoundType soundType)
    {
        AudioClip audioClip = null;

        switch (soundType)
        {
            case SoundType.BGM:
                audioClip = bgmLibrary.Where(x => x.soundName == soundName).FirstOrDefault().audioClip;
                break;
            case SoundType.SFX:
                audioClip = sfxLibrary.Where(x => x.soundName == soundName).FirstOrDefault().audioClip;
                break;
            case SoundType.Narration:
                audioClip = narrationLibrary.Where(x => x.soundName == soundName).FirstOrDefault().audioClip;
                break;
            default:
                break;
        }

        return audioClip;
    }

    public AudioClip GetBGMClip(SoundName soundName)
    {
        return GetAudioClip(soundName, SoundType.BGM);
    }

    public SoundName? GetCurrentBGMSoundName()
    {
        SoundName? soundName = null;
        Sound sound = bgmLibrary.Where(x => x.audioClip == bgmMusicManager.clip).FirstOrDefault();
        if (sound != null)
        {
            soundName = sound.soundName;
        }

        return soundName;
    }

    public void PlayMainMusic(int musicIdentifier)
    {
        SetMainGameMusicIdentifier(musicIdentifier);
        PlayMainMusic(true, false);
    }

    public void PlayMainMusic(AudioClip audioClip)
    {
        SetMainGameMusicIdentifier(audioClip);
        PlayMainMusic(true, false);
    }

    public void PlayMainMusic(SoundName soundName)
    {
        SetMainGameMusicIdentifier(GetBGMClip(soundName));
        PlayMainMusic(true, false);
    }

    private void PlayMainMusic(bool useFade, bool keepTimeIndex)
    {
        if (!isMusicSwitching && bgmMusicManager.clip != bgmLibrary[bgmMusicIdentifier].audioClip)
        {
            if (useFade)
            {
                StartCoroutine(FadeInMusic(bgmLibrary[bgmMusicIdentifier].audioClip, keepTimeIndex));
            }
            else
            {
                PlayMainMusicNoFade(keepTimeIndex);
            }
        }
    }

    public void PlayMainMusic(int musicIdentifier, bool useFade, bool keepTimeIndex)
    {
        SetMainGameMusicIdentifier (musicIdentifier);
        PlayMainMusic(useFade, keepTimeIndex);
    }

    public void PlayMainMusic (AudioClip audioClip, bool useFade, bool keepTimeIndex)
    {
        SetMainGameMusicIdentifier(audioClip);
        PlayMainMusic(useFade, keepTimeIndex);

    }

    public void PlayMainMusic(SoundName soundName, bool useFade, bool keepTimeIndex)
    {
        SetMainGameMusicIdentifier(GetBGMClip(soundName));
        PlayMainMusic(useFade, keepTimeIndex);

    }

    private void PlayMainMusicNoFade(bool keepTimeIndex)
    {
        if (keepTimeIndex)
        {
            SwapAtClipSameTimeIndex ();
            
        }
        else
        {
            bgmMusicManager.clip = bgmLibrary[bgmMusicIdentifier].audioClip;
        }
        bgmMusicManager.Play ();
    }

    private void SwapAtClipSameTimeIndex()
    {
        float timeIndex = bgmMusicManager.time;
        bgmMusicManager.clip = bgmLibrary[bgmMusicIdentifier].audioClip;
        bgmMusicManager.time = timeIndex;
    }

    private IEnumerator FadeInMusic(AudioClip newClip, bool keepTimeIndex)
    {

        if (newClip != bgmMusicManager.clip && !isMusicSwitching)
        {
            isMusicSwitching = true;

            float initialVolume = bgmMusicManager.volume;	
            while (bgmMusicManager.volume > initialVolume / 5)
            {
                bgmMusicManager.volume -= initialVolume * Time.deltaTime / fadeTime;
                yield return null;
            }
            if (keepTimeIndex)
            {
                SwapAtClipSameTimeIndex ();
            }
            else
            {
                bgmMusicManager.clip = newClip;
            }
            bgmMusicManager.Play ();

            while (bgmMusicManager.volume < initialVolume)
            {
                bgmMusicManager.volume += initialVolume * Time.deltaTime / fadeTime;

                if (bgmMusicManager.volume > initialVolume)
                {
                    bgmMusicManager.volume = initialVolume;
                }
                yield return null;
            }

            yield return new WaitForSeconds (minMusicPlayDuration);
            isMusicSwitching = false;
        }

        yield return null;
    }


    public void SetMainGameMusicIdentifier(float determiningValue)
    {
        bgmMusicIdentifier = Mathf.RoundToInt(determiningValue);
        if (bgmMusicIdentifier >= bgmLibrary.Count)
        {
            bgmMusicIdentifier = bgmLibrary.Count - 1;
        }
    }

    public void SetMainGameMusicIdentifier(AudioClip audioClip)
    {
        Sound sound = bgmLibrary.Where(x => x.audioClip == audioClip).First();
        if (sound != null)
        {
            bgmMusicIdentifier = bgmLibrary.IndexOf(sound);
        }
        else
        {
                Debug.LogWarning("Audio Clip " + audioClip.name + " is not in the list of Main Game Music.  Add it to get this clip to play.");
        }
    }
    #endregion

    #region Narration

    public AudioClip GetNarrationClip(SoundName soundName)
    {
        return GetAudioClip(soundName, SoundType.Narration);
    }

    public void adjustNarrationVolume(float newVolume)
    {
        narrationVolume = newVolume;
        narrationManager.volume = narrationVolume * masterVolume;

    }

    public void PlayNarration(AudioClip clip)
    {
        StartCoroutine (PlayNarrationInternal (clip));
    }

    public void PlayNarrartion(SoundName soundName)
    {
        AudioClip clip = narrationLibrary.Where(x => x.soundName == soundName).FirstOrDefault().audioClip;
        if (clip != null)
        {
            StartCoroutine(PlayNarrationInternal(clip));
        }
    }

    public void PlayNarrartion(Sound sound)
    {
        StartCoroutine(PlayNarrationInternal(sound.audioClip));
    }

    private IEnumerator PlayNarrationInternal(AudioClip clip)
    {
        if (narrationManager.isPlaying)
        {
            yield return new WaitForSeconds (narrationManager.clip.length - narrationManager.time + .5f);
        }
        float prevBGMVolume = bgmMusicManager.volume;
        bgmMusicManager.volume = prevBGMVolume * .5f;
        narrationManager.clip = clip;
        narrationManager.Play ();
        yield return new WaitForSeconds (clip.length);
        bgmMusicManager.volume = prevBGMVolume;

    }
    #endregion

    #region SFX

    public AudioClip GetSFXClip(SoundName soundName)
    {
        return GetAudioClip(soundName, SoundType.SFX);
    }

    //Will only affect the next SFX that plays, then be reset to 1 after audio plays.
    public void SetSFXPitchMultiplier(float pitchMultiplier)
    {
        sfxPitchMultiplier = pitchMultiplier;
    }

    public void SetRandomSFXPitchMultiplier(float min = .5f, float max = 2f)
    {
        SetSFXPitchMultiplier(Random.Range(min, max));
    }

    public void PlaySFXOnce(SoundName soundName, Vector3? position = null)
    {
        AudioClip clip = sfxLibrary.Where(x => x.soundName == soundName).FirstOrDefault().audioClip;
        if (clip != null)
        {
            StartCoroutine(PlaySFXRepeatedInternal(clip, 1, 0, 0, position));
        }
    }

    public void PlaySFXOnce(Sound sound, Vector3? position = null)
    {
        StartCoroutine(PlaySFXRepeatedInternal(sound.audioClip, 1, 0, 0, position));
    }

    public void PlaySFXOnce (AudioClip clip, Vector3? position = null)
    {
        StartCoroutine(PlaySFXRepeatedInternal(clip, 1, 0, 0, position));
    }

    public void PlaySFXRepeated(SoundName soundName, int timesToPlay = 1, float volumeReductionMultiplierPerPlay = 0, float secondsOfOverlap = 0, Vector3? position = null)
    {
        AudioClip clip = sfxLibrary.Where(x => x.soundName == soundName).FirstOrDefault().audioClip;
        if (clip != null)
        {
            StartCoroutine(PlaySFXRepeatedInternal(clip, timesToPlay, volumeReductionMultiplierPerPlay, secondsOfOverlap, position));
        }
    }

    public void PlaySFXRepeated(Sound sound, int timesToPlay = 1, float volumeReductionMultiplierPerPlay = 0, float secondsOfOverlap = 0, Vector3? position = null)
    {
        StartCoroutine(PlaySFXRepeatedInternal(sound.audioClip, timesToPlay, volumeReductionMultiplierPerPlay, secondsOfOverlap, position));
    }

    public void PlaySFXRepeated(AudioClip clip, int timesToPlay = 1, float volumeReductionMultiplierPerPlay = 0, float secondsOfOverlap = 0, Vector3? position = null)
    {
        StartCoroutine(PlaySFXRepeatedInternal(clip, timesToPlay, volumeReductionMultiplierPerPlay, secondsOfOverlap, position));
    }

    private IEnumerator PlaySFXRepeatedInternal(AudioClip clip, int timesToPlay, float volumeReductionMultiplierPerPlay, float secondsOfOverlap, Vector3? position = null)
    {
        position = (position != null ? position : Vector3.zero);

        for (int i = 0; i < timesToPlay; i++)
        {
            float volume = (usePositionalVolume ? GetVolumeBasedOnPosition((Vector3)position) : 1) * sfxVolumeMultiplier * masterVolume;
            AudioSource sfxSource = ChooseSFXSource (volume);
            if (sfxSource != null)
            {
                if (isSound3D)
                {
                    sfxSource.panStereo = GetStereoPanBasedOnPosition ((Vector3)position);
                }
                sfxSource.loop = false;
                sfxSource.clip = clip;
                sfxSource.volume = volume;
                if (speedUpSFXByTimescale)
                {
                    if (Time.timeScale > 1)
                    {
                        sfxSource.pitch = 1 + (Time.timeScale / 10);
                    }
                    else
                    {
                        sfxSource.pitch = 1;
                    }
                }
                
                sfxSource.pitch *= sfxPitchMultiplier;

                sfxSource.volume = sfxSource.volume -  (i * volumeReductionMultiplierPerPlay);
                sfxSource.Play();
            }
            yield return new WaitForSeconds (clip.length);
            sfxPitchMultiplier = 1;
        }
    }
        
    public AudioSource ChooseSFXSource(float volume)
    {
        AudioSource chosenSource = new AudioSource();
        foreach(AudioSource source in sfxManager)
        {
            if (!source.isPlaying)
            {
                return source;
            }
            else
            {
                if (source.volume < volume)
                {
                    if (chosenSource == null || source.volume < chosenSource.volume)
                    {
                        chosenSource = source;
                    }
                }
            }
        }
        return chosenSource;
    }

    public float GetVolumeBasedOnPosition(Vector3 position)
    {
        Vector3 checkValues = cam.WorldToViewportPoint (position);
        float xVol = (1 - Mathf.Abs ((checkValues.x - .5f) * 2));
        float yVol = (1 - Mathf.Abs ((checkValues.y - .5f) * 2 / cam.aspect));
        float totalVol = xVol * yVol;


        return  (totalVol > 1 ? 1 : totalVol);

    }

    public float GetStereoPanBasedOnPosition (Vector3 position)
    {
        Vector3 checkValues = cam.WorldToViewportPoint (position);

        return ((checkValues.x -.5f)*2);
    }
    #endregion

}

[System.Serializable]
class SoundPrefs
{
    public float masterVolume;
    public float bgmVolume;
    public float narrationVolume;
    public float sfxVolumeMultiplier;
    public int bgmMusicIdentifier; 
}

[System.Serializable]
public enum SoundType
{
    BGM = 0,
    SFX = 1,
    Narration = 2,
}

[System.Serializable]
public enum SoundName
{
    Buzz = 0,
    Shoot = 1,
    HoneySlurp = 2,
    Drill = 3,
    DroneBee = 4,
    EnemyShoot = 5,
    ClawSnap = 6,
    PlayerDies = 7,
    PlayerHit = 8,
    EnemyDies = 9,
    EnemyHit = 10,
    DrillDestroyed = 11,
    OtherExplosion = 12,

    UIConfirm = 101,
    UISelection = 102,

    MainBGM = 1001,
    TitleBGM = 1002,
    DrillEngaged = 1003,
    DrillEngagedDistorted = 1004,
}
