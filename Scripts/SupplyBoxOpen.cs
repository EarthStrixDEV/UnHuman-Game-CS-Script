using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBoxOpen : MonoBehaviour
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
            PlayerController.BatteryCapacity = 100f;
        }
    }
    
}
