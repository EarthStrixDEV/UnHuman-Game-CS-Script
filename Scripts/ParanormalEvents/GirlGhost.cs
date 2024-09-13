using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlGhost : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GirlGhostCharacter;
    public AudioClip SoundEffectClip;
    private AudioSource SoundEffect;
    public static bool isPlayerCollision = true;

    private void Start()
    {
        SoundEffect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isPlayerCollision) {
            GirlGhostCharacter.SetActive(false);
            SoundEffect.PlayOneShot(SoundEffectClip);
            isPlayerCollision = false;
        }
    }
}
