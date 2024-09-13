using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanGhost : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WomanGhostCharacter;
    private AudioSource EventSoundSource;
    public AudioClip EventSoundClip;
    private bool isPlayerCollision = true;
    void Start()
    {
        WomanGhostCharacter.SetActive(false);
        EventSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CharacterDisappear() {
        WomanGhostCharacter.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        WomanGhostCharacter.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            if (isPlayerCollision) {
                StartCoroutine(CharacterDisappear());
                EventSoundSource.PlayOneShot(EventSoundClip);
                isPlayerCollision = false;
            }
        }
    }
}
