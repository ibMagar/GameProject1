using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("sources")]
    public AudioSource fxSource;
    public AudioSource musicSource;
    public AudioClip musicClip;
    [Header("Clips")]
    public AudioClip obstacleDropSound;
    public AudioClip objectDropSound;
    public AudioClip magnetDropSound;
    public AudioClip magnetDropShortSound;
    public AudioClip camoSound;
    public AudioClip magnetOutSound;
    public AudioClip slideSound;
    public AudioClip chargeObjectJumpSound;

    [Header("UI sounds")]
    public AudioClip clickSound;

    #region UI sounds region
    public void PlayClickSound()
    {
        fxSource.PlayOneShot(clickSound);
    }

    #endregion

    private void Awake() => instance = this;

    private void Start()
    {
        //PlayMusic();
        musicSource.volume = .01f;
    }
    #region volumes
    public void SetSFXVolume(float value)
    {
        fxSource.DOFade(value, .1f);
    }
    public void setMusicvolume(float value)
    {
        musicSource.DOFade(value, .1f);
    }
    #endregion
    #region gameplay Sounds
    public void PlayMusic()
    {
        //musicSource.clip = musicClip;
        musicSource.DOFade(.3f, .02f);
    }
    public void FadeOutMusic()
    {
        musicSource.DOFade(.05f, .5f);
    }
    public void PlayObstacleDropSound()
    {
        fxSource.PlayOneShot(obstacleDropSound);
    }
    public void PlayObjectDropSound()
    {
        fxSource.PlayOneShot(objectDropSound);
    }
    public void PlayMagnetDropSound()
    {
        fxSource.PlayOneShot(magnetDropSound);
    }
    public void PlayMagnetDropShortSound()
    {
        fxSource.PlayOneShot(magnetDropShortSound);
    }
    public void PlayCamoSound()
    {
        fxSource.PlayOneShot(camoSound);
    }
    public void PlayObstacleJumpSound()
    {
        fxSource.PlayOneShot(camoSound);    // for now using camo sound.
    }
    public void PlayMagnetoutSound()
    {
        fxSource.PlayOneShot(magnetOutSound);
    }
    public void stopMagnetSound()
    {
        if (fxSource.isPlaying)
            fxSource.Stop();
    }
    public void PlayslideSound()
    {
        fxSource.PlayOneShot(slideSound);
    }
    public void PlayJumpSound()
    {
        fxSource.PlayOneShot(chargeObjectJumpSound);
    }
    #endregion
}
