using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public float worldMoveSpeed;
    public float moveSpeedMultiplier;
    public float MaxmoveSpeed;

    [Space]
    public float spawnTime;
    private float spawnTimeCountDown;
    public float spawnTimeMultiplier;
    public float minimumSpawnTime;

    public GameObject[] bloquesParaSpawnear;
    public List<GameObject> bloquesSpawneados;

    public GameObject[] zombiesParaSpawnear;
    public List<GameObject> zombiesSpawneados;

    private float distanceTraveledPerFrame;
    public float totalDistanceTraveled;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI recorridoTExt;

    public bool canSpawnZombie;


    void Start()
    {
        spawnTimeCountDown = spawnTime;

    }

    void Update()
    {
        for (int i = 0; i < bloquesSpawneados.ToList().Count; i++)
        {
            bloquesSpawneados[i].GetComponent<Rigidbody>().velocity = Vector3.forward * -worldMoveSpeed;
        }

        foreach (var bloque in bloquesSpawneados.ToList())
        {
            if (bloque.transform.position.z < -60)
            {
                bloquesSpawneados.Remove(bloque);
                Destroy(bloque);
            }
        }

        for (int i = 0; i < zombiesSpawneados.ToList().Count; i++)
        {
            zombiesSpawneados[i].GetComponent<Rigidbody>().velocity = Vector3.forward * -worldMoveSpeed;
        }

        foreach (var zombie in zombiesSpawneados.ToList())
        {
            if (zombie.transform.position.z < -60)
            {
                zombiesSpawneados.Remove(zombie);
                Destroy(zombie);
            }
        }

        if (worldMoveSpeed < MaxmoveSpeed)
        {
            worldMoveSpeed += moveSpeedMultiplier * Time.deltaTime;

        }
        else if (worldMoveSpeed>= MaxmoveSpeed)
        {
            worldMoveSpeed = MaxmoveSpeed;
        }


        if (spawnTime > minimumSpawnTime)
        {
            spawnTime -= spawnTimeMultiplier * Time.deltaTime;
        }
        else if (spawnTime <= minimumSpawnTime)
        {
            spawnTime = minimumSpawnTime;

        }


        spawnTimeCountDown -= Time.deltaTime;


        if (spawnTimeCountDown<=0)
        {
            spawnTimeCountDown = spawnTime;
            SpawnBloque();

        }

        if (spawnTimeCountDown == spawnTime && !canSpawnZombie)
        {
            canSpawnZombie = true;

            SpawnZombie();

        }
        if (spawnTimeCountDown <1 && canSpawnZombie)
        {
            canSpawnZombie = false;

            SpawnZombie();
        }

        ChangeTextOnScreen();
    }

    void SpawnBloque()
    {
        int i = Random.Range(0, bloquesParaSpawnear.Length);

        var spawnedBloque = GameObject.Instantiate(bloquesParaSpawnear[i]);

        spawnedBloque.transform.position = new Vector3(0, spawnedBloque.transform.position.y, this.transform.position.z);

        bloquesSpawneados.Add(spawnedBloque);

        int randomNumber = Random.Range(0, 1);

        if (randomNumber == 0)
        {
            canSpawnZombie = false;
        }
        else
        {
            canSpawnZombie = true;
        }

    }

    void SpawnZombie()
    {
        int i = Random.Range(0, zombiesParaSpawnear.Length);
        float zZ = Random.Range(-10, 10);


        var spawnedZombie = GameObject.Instantiate(zombiesParaSpawnear[i]);

        spawnedZombie.transform.position = new Vector3(spawnedZombie.transform.position.x, spawnedZombie.transform.position.y, this.transform.position.z+zZ);

        zombiesSpawneados.Add(spawnedZombie);

    }

    void ChangeTextOnScreen()
    {
        distanceTraveledPerFrame = worldMoveSpeed * Time.deltaTime;
        totalDistanceTraveled += distanceTraveledPerFrame;

        speedText.text = worldMoveSpeed.ToString("F0") + " km/h";
        recorridoTExt.text = (totalDistanceTraveled / 1000).ToString("F1") + " Kms";

    }

}


