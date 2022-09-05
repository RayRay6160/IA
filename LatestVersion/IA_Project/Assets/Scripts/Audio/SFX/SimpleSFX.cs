using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "SFX/SimpleSFX/Create")]
public class SimpleSFX : SFX {

    public AudioClip[] clips;

    public RangedFloat volume;

    [MinMaxRange(0, 2)]
    public RangedFloat pitch;

    public override void Play(AudioSource audio) {

        if (clips == null || clips.Length == 0) {
            return;
        }

        audio.pitch = Random.Range(pitch.minValue, pitch.maxValue); //set random pitch
        audio.PlayOneShot(clips[Random.Range(0, clips.Length)], Random.Range(volume.minValue, volume.maxValue)); //play random clip with random volume

    }
}