using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager> {

    #region Music

    [SerializeField] AudioClip clampMusic, fastMusic;

    [SerializeField] float fadeSpeed = 5f;

    float t;

    AudioSource musicTrack01, musicTrack02;

    #endregion

    protected override void Awake() {
        base.Awake();

        musicTrack01 = gameObject.AddComponent<AudioSource>();
        musicTrack02 = gameObject.AddComponent<AudioSource>();

        musicTrack01.clip = clampMusic;
        musicTrack02.clip = fastMusic;
    }

    #region SFX

    [SerializeField] AudioSource sFXPlayer;

    const float MIN_PITCH = 0.9f;

    const float MAX_PITCH = 1.1f;

    #endregion

    public void PlaySFX(SFX sFX) { //play with scriptable object sfx
        sFX.Play(sFXPlayer);
    }

    #region AudioData

    public void PlaySFX(AudioData audioData) { //play with audio data
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
    }

    public void PlayRandomSFX(AudioData audioData) {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioData);
    }

    public void PlayRandomSFX(AudioData[] audioData) {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }

    #endregion

    #region MusicPlay

    public void PlayMusic(bool isPlayCalmMusic) {
        if (isPlayCalmMusic) {
            StopAllCoroutines();
            StartCoroutine(FadeTrack(musicTrack01, musicTrack02));
        } else {
            StopAllCoroutines();
            StartCoroutine(FadeTrack(musicTrack02, musicTrack01));
        }
    }

    IEnumerator FadeTrack(AudioSource track01, AudioSource track02) {

        t = 0;

        track01.Play();

        while (t < 1) {
            track01.volume = Mathf.Lerp(0, 1, t);
            track02.volume = Mathf.Lerp(1, 0, t);
            t += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        track02.Stop();
    }

    #endregion

}


[System.Serializable]
public class AudioData {

    public AudioClip audioClip;

    public float volume;

}