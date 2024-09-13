using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WomanGhostFactory : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource EventSoundSource;
    public AudioClip EventSoundClip;
    public GameObject WomanghostCharacter;
    public GameObject[] FactoryPropsObject;
    private bool isPlayerCollision = false;
    private bool disableEvent = false;

    int RandomPhysicVector;
    int FactoryPropsObjectLength;
    Rigidbody ObjectRigid;
    void Start()
    {
        EventSoundSource = GetComponent<AudioSource>();
        FactoryPropsObjectLength = FactoryPropsObject.Length;
        WomanghostCharacter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayEvent() {
        if (isPlayerCollision && disableEvent == false)
        {
            StartCoroutine(GhostDisappear());
            for (int index = 0; index < FactoryPropsObjectLength; index++)
            {
                FactoryPropsObject[index].AddComponent<Rigidbody>();
                FactoryPropsObject[index].GetComponent<Rigidbody>().mass = 1f;
                FactoryPropsObject[index].GetComponent<Rigidbody>().useGravity = true;
                FactoryPropsObject[index].GetComponent<Rigidbody>().AddForce(Vector3.left, ForceMode.Impulse);
            }
        }
    }

    private void PlaySound() {
        EventSoundSource.PlayOneShot(EventSoundClip);
    }

    IEnumerator GhostDisappear() {
        WomanghostCharacter.SetActive(true);
        yield return new WaitForSeconds(1f);
        WomanghostCharacter.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            if(disableEvent == false){
                isPlayerCollision = true;
                PlaySound();
                PlayEvent();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            disableEvent = true;
        }
    }
}
