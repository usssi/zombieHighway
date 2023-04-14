using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class detectZombie : MonoBehaviour
{
    public Transform puntoDeTp;
    private SphereCollider thisCollider;

    private bool isMoving = false;
    public float moveSpeed = 5f;

    public List<GameObject> zombieEnSpot;

    void Start()
    {
        thisCollider = gameObject.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (zombieEnSpot.Count > 0 && zombieEnSpot[0] == null)
        {
            thisCollider.enabled = true;
            zombieEnSpot.Clear();
        }
        if (zombieEnSpot.Count > 0 && zombieEnSpot[0] != null)
        {
            thisCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Zombie")
        {
            //print(other.name);
            zombieEnSpot.Add(other.gameObject);

            Transform spot = puntoDeTp;
            other.transform.parent = spot.transform;

            StartCoroutine(MoveZombie(other.gameObject.transform, spot));
        }
    }

    IEnumerator MoveZombie(Transform zombie, Transform spot)
    {
        isMoving = true;

        while (isMoving)
        {
            zombie.position = Vector3.MoveTowards(zombie.position, spot.position, moveSpeed * Time.deltaTime);

            if (zombie.position == spot.position)
            {
                isMoving = false;
                yield break; // exit coroutine
            }

            yield return null;
        }
    }
}

