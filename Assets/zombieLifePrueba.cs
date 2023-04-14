using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class zombieLifePrueba : MonoBehaviour
{
    public bool isHanging;
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
