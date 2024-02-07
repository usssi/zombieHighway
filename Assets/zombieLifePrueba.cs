using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class zombieLifePrueba : MonoBehaviour
{
    public ZombieData[] zombiesDataArray;
    public ZombieData thisZombieData;


    public bool isHanging;

    public string type;
    public int life;
    public float size;
    public float force;
    public float peso;



    private void Start()
    {
        int i = Random.Range(0, zombiesDataArray.Length);

        thisZombieData = zombiesDataArray[i];

        type = thisZombieData.type;
        life = thisZombieData.life;
        size = thisZombieData.size;
        force = thisZombieData.forceMultiplier;
        peso = thisZombieData.peso;

        this.transform.localScale = new Vector3(transform.localScale.x+size, transform.localScale.y, transform.localScale.z + size);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (isHanging)
        {
            if (other.transform.tag == "obstaculo")
            {
                this.gameObject.transform.parent = null;

                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * -600);
                this.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 60);

                Invoke("KillZombies", 2);
            }
        }
    }

    private void Update()
    {
        if (transform.tag == "zombieH")
        {
            isHanging = true;
        }
        else
        {
            isHanging = false;
        }
    }

    void KillZombies()
    {
        Destroy(this.gameObject);
    }

}
