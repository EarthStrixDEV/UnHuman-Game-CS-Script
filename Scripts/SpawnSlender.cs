using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlender : MonoBehaviour
{
    public GameObject Slenderman;
    public static bool IsRespawned = false;
    private Vector3 RespawnDistance;
    Vector3 PlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = PlayerController.PlayerPosition;
        RespawnController();
    }

    public void RespawnController()
    {
        RespawnDistance = new Vector3(Random.Range(50 ,100) ,0f ,Random.Range(50 ,100));
        if (IsRespawned)
        {
            Instantiate(Slenderman, PlayerPosition + RespawnDistance, Quaternion.identity);
        }
        IsRespawned = false;
    }
}
