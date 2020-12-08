using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{

    public AudioClip ClipVoice1;
    public AudioClip ClipVoice2;
    public AudioClip ClipVoice3;
    public AudioClip ClipWalk;

    private FMOD.Studio.EventInstance LongGrowl;
    FMOD.Studio.PLAYBACK_STATE PbState;

    private AudioSource[] VoiceSource = new AudioSource[3];
    //private AudioSource WalkSource;

    private float timer = 3f;
    private int source = 1;


    private AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {

        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        newAudio.maxDistance = 40;
        newAudio.rolloffMode = AudioRolloffMode.Custom;
        return newAudio;
    }


    public void Awake()
    {
        //LongGrowl = FMODUnity.RuntimeManager.CreateInstance("event:/Long Zombie Growl");

        // add the necessary AudioSources
        VoiceSource[0] = AddAudio(ClipVoice1, false, false, 0.8f);
        VoiceSource[1] = AddAudio(ClipVoice2, false, false, 0.8f);
        VoiceSource[2] = AddAudio(ClipVoice3, false, false, 0.8f);
        //WalkSource = AddAudio(ClipWalk, true, false, 0.8f);
    }

    private void PlayAudio(AudioSource source)
    {

        source.Play();
    }


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            source = Random.Range(0, 2);
            PlayAudio(VoiceSource[source]);
            timer = Random.Range(1f, 4f);
        }


    }

}
