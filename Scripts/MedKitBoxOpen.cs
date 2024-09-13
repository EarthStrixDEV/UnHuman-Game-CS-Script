using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitBoxOpen : MonoBehaviour
{
    private Animator boxAnimator;
    void Start()
    {
        boxAnimator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            boxAnimator.Play("opened_closed", 0, 0f);
            PlayerController.HealthPlayer = 100f;
        }
    }
}
