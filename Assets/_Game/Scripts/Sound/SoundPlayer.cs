using UnityEngine;

public class SoundPlayer : MonoBehaviour{
    private AudioSource source;

    private void Awake(){
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(){
        transform.SetParent(null);
        source.PlayOneShot(source.clip);
    }
}