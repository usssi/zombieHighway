using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class matarChocando : MonoBehaviour
{
    private GameObject zombieCopia;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Zombie")
        {
            print("choque contra zombie");

            zombieCopia = other.gameObject;
            Destroy(other.gameObject);

            zombieCopia.transform.parent = null;
            //zombieCopia.GetComponent<CapsuleCollider>().enabled = false;

            zombieCopia.gameObject.GetComponent<Rigidbody>().useGravity = true;
            zombieCopia.GetComponent<Rigidbody>().isKinematic = false;

            Instantiate(zombieCopia);

            zombieCopia.GetComponent<Rigidbody>().velocity = Vector3.forward * 500;
            zombieCopia.GetComponent<Rigidbody>().velocity = Vector3.up * 500;
        }
    }
}
