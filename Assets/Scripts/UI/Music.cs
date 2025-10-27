using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource source;
    AudioClip clip;
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.clip = clip;
        source.Play();
    }
}