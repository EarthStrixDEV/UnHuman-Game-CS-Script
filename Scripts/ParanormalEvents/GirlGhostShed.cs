using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlGhostShed : MonoBehaviour
{
    public GameObject GirlGhostCharacter;
    public GameObject EventBox;
    public AudioClip EventSoundClip;
    private AudioSource EventSoundSource;
    private bool isPlayerCollision = false;
    private bool disableEvent = false;

    public float speedMovement = 0f;
    private float currentPosition = 0f;

    // Start is called before the first frame update
    void Start()
    {
        EventSoundSource = GetComponent<AudioSource>();
        GirlGhostCharacter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestController.GirlGhostShedEventisActive) {
            if (isPlayerCollision && disableEvent == false) {
                currentPosition = Mathf.Clamp(speedMovement * Time.deltaTime, 0f, 10f);
                GirlGhostCharacter.transform.Translate(0f ,0f ,currentPosition);
                StartCoroutine(ObjectDisappear());
            }
        }

    }

    IEnumerator ObjectDisappear() {
        yield return new WaitForSeconds(2f);
        GirlGhostCharacter.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && QuestController.GirlGhostShedEventisActive) {
            if (disableEvent == false) {
                GirlGhostCharacter.SetActive(true);
                EventSoundSource.PlayOneShot(EventSoundClip);
                isPlayerCollision = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && QuestController.GirlGhostShedEventisActive)
        {
            disableEvent = true;
        }
    }
}
