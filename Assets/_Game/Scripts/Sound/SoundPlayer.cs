using System;
using _Game.Scripts;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour{
    private AudioSource source;

    private float volume;

    private void Awake(){
        source = GetComponent<AudioSource>();
        volume = source.volume;
    }

    private void Start(){
        //PauseMenu.Instance.OnPause += ToggleSound;
    }

    private void OnDestroy(){
        //PauseMenu.Instance.OnPause -= ToggleSound;
    }

    private void ToggleSound(bool value){
        if (value){
            source.volume = volume;
            return;
        }

        source.volume = 0f;
    }

    public void PlaySound(){
        transform.SetParent(null);
        source.PlayOneShot(source.clip);
    }
}