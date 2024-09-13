using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("GameObject")]
    public static Vector3 PlayerPosition;
    public AudioClip flashlightClip;
    private AudioSource flashlightSound;
    public TextMeshProUGUI HealthPlayerUI;
    public Canvas InteractUILabel;
    public Canvas PlayerDiedUI;
    public Slider BatteryUI;
    public Canvas PauseUI;
    public static bool isPlayerDied = false;
    public static bool isPlayerRunning;
    public static bool isPlayerMoving;
    public static bool isTriggerPlayer;
    public static float HealthPlayer = 100f;
    private float HealthPlayerDamage = 5f;
    private float HealthPlayerHealing = 0.45f;
    [Header("Variable")]
    public float speedUsageBattery = 2f;
    public float speedChargingBattery = 5f;
    public static float BatteryCapacity = 100f;
    private float FlashLightIntensity = 0.8f;
    private float FlashLightZoomIntensity = 2.0f;
    private Light FlashlightSpotlight;
    private bool toggleFlashlight = false;
    private bool toggleZoomFlashlight = false;

    public static bool PauseUIToggle = false;
    // Start is called before the first frame update
    void Start()
    {
        flashlightSound = GetComponent<AudioSource>();
        FlashlightSpotlight = GameObject.FindGameObjectWithTag("Spotlight").GetComponent<Light>();
        FlashlightSpotlight.enabled = true;
        InteractUILabel.enabled = false;
        PlayerDiedUI.enabled = false;
        PauseUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            isPlayerRunning = true;
        } else {
            isPlayerRunning = false;
        }

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.A)
        ) {
            isPlayerMoving = true;
        } else {
            isPlayerMoving = false;
        }

        HealthPlayer = Mathf.Clamp(HealthPlayer, 0, 100);
        HealthPlayer += HealthPlayerHealing * Time.deltaTime;
        FlashlightSpotlight.intensity -= BatteryCapacity / 2;
        FlashlightSpotlight.intensity = Mathf.Clamp(FlashlightSpotlight.intensity , 0, 3.8f);

        PlayerPosition = transform.position;

        FlashLightController();
        CanvasUpdator();
        isPlayerDie();
    }

    private void isPlayerDie() {
        if (HealthPlayer < 1f) {
            PlayerDiedUI.enabled = true;
            isPlayerDied = true;
        }
    }

    private void FlashLightController() {
        if (FlashlightSpotlight.enabled) {
            BatteryCapacity -= speedUsageBattery * Time.deltaTime;
        } else {
            BatteryCapacity += speedChargingBattery * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            toggleFlashlight = !toggleFlashlight;
            FlashlightSpotlight.enabled = toggleFlashlight;
            flashlightSound.PlayOneShot(flashlightClip);
        }

        if (Input.GetMouseButtonDown(1)) {
            flashlightSound.PlayOneShot(flashlightClip);
        }

        if (Input.GetMouseButton(1)) {
            FlashlightSpotlight.range = 400f;
            FlashlightSpotlight.spotAngle = 10;
            FlashlightSpotlight.intensity = FlashLightZoomIntensity;
            toggleZoomFlashlight = !toggleZoomFlashlight;
        } else {
            FlashlightSpotlight.range = 200.0f;
            FlashlightSpotlight.spotAngle = 45f;
            FlashlightSpotlight.intensity = FlashLightIntensity;
            toggleZoomFlashlight = !toggleZoomFlashlight;
        }

        if (BatteryCapacity < 100f && BatteryCapacity >= 70)
        {
            FlashLightIntensity = 0.8f;
            FlashLightZoomIntensity = 2.00f;
        }
        else if (BatteryCapacity < 70f && BatteryCapacity >= 50)
        {
            FlashLightIntensity = 0.6f;
            FlashLightZoomIntensity = 1.60f;
        }
        else if (BatteryCapacity < 50f && BatteryCapacity >= 30)
        {
            FlashLightIntensity = 0.4f;
            FlashLightZoomIntensity = 1.20f;
        }
        else if (BatteryCapacity < 30f && BatteryCapacity >= 10)
        {
            FlashLightIntensity = 0.2f;
            FlashLightZoomIntensity = 0.9f;
        }
        else if (BatteryCapacity < 10f && BatteryCapacity >= 0)
        {
            FlashLightIntensity = 0.09f;
            FlashLightZoomIntensity = 0.6f;
        }
    }

    private void CanvasUpdator() {
        HealthPlayerUI.text = "HP : " + HealthPlayer.ToString("F" + 0);
        BatteryUI.value = BatteryCapacity;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUIToggle = !PauseUIToggle;
            PauseUI.enabled = PauseUIToggle;
            Time.timeScale = PauseUIToggle ? 0f : 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Slender")) {
            isTriggerPlayer = true;
        }

        if (other.gameObject.CompareTag("medkit_box_1") ||
            other.gameObject.CompareTag("supply_box_1") ||
            other.gameObject.CompareTag("Quest")) {
            InteractUILabel.enabled = true;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Slender")) {
            HealthPlayer -= HealthPlayerDamage * Time.deltaTime;
        }

        if (other.gameObject.CompareTag("medkit_box_1") ||
            other.gameObject.CompareTag("supply_box_1") ||
            other.gameObject.CompareTag("Quest"))
        {
            InteractUILabel.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Slender"))
        {
            isTriggerPlayer = false;
        }

        if (other.gameObject.CompareTag("medkit_box_1") ||
            other.gameObject.CompareTag("supply_box_1") ||
            other.gameObject.CompareTag("Quest"))
        {
            InteractUILabel.enabled = false;
        }
    }
}
