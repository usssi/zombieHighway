using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMoveRespawn : MonoBehaviour
{
    public float worldMoveSpeed;
    public float moveSpeedMultiplier;

    private float distanceTraveledPerFrame;
    public float totalDistanceTraveled;

    public GameObject[] bloques;

    void Update()
    {
        distanceTraveledPerFrame = worldMoveSpeed * Time.deltaTime;
        totalDistanceTraveled += distanceTraveledPerFrame;

        for (int i = 0; i < bloques.Length; i++)
        {
            bloques[i].GetComponent<Rigidbody>().velocity = Vector3.forward * -worldMoveSpeed;

            if (bloques[i].transform.position.z < -180)
            {
                bloques[i].transform.position = new Vector3(0,0,320);
            }
        }

        worldMoveSpeed += moveSpeedMultiplier * Time.deltaTime;

    }
}
