using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlenderController : MonoBehaviour
{
    public float blinkMin = 1f;
    public float blinkMax = 5f;
    private float nextTimeBlink;
    public GameObject slenderObject;
    private AudioSource slenderSound;
    public Renderer characterRenderer;
    private NavMeshAgent Ai_Controller;
    private bool respawnSlender = false;
    // Start is called before the first frame update
    void Start()
    {
        Ai_Controller = GetComponent<NavMeshAgent>();
        slenderSound = GetComponent<AudioSource>();
        nextTimeBlink = Time.time + Random.Range(blinkMin, blinkMax);
        characterRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Ai_Controller.SetDestination(PlayerController.PlayerPosition);
        
        if (PlayerController.isPlayerRunning && PlayerController.isPlayerMoving) {
            Ai_Controller.speed = 10;
            Ai_Controller.acceleration = 8;
        } else if (PlayerController.isPlayerMoving) {
            Ai_Controller.speed = 6;
            Ai_Controller.acceleration = 6;
        } else {
            Ai_Controller.speed = 4;
            Ai_Controller.acceleration = 4;
        }

        if (Time.time >= nextTimeBlink) {
            StartCoroutine(Blink());
            nextTimeBlink = Time.time + Random.Range(blinkMin, blinkMax);
        }

        if (PlayerController.isTriggerPlayer)
        {
            slenderSound.volume = 0.2f;
        }
        else
        {
            slenderSound.volume = 0f;
        }

        if (PlayerController.isTriggerPlayer) {
            StartCoroutine(SlenderDisappear());
        }
    }

    IEnumerator Blink() {
        characterRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        characterRenderer.enabled = true;
    }

    IEnumerator SlenderDisappear() {
        yield return new WaitForSeconds(40f);
        Destroy(gameObject);
        PlayerController.isTriggerPlayer = false;
    }
}
