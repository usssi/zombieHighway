using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class pullCarDependingonZombies : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private List<GameObject> zombiesLeft;
    [SerializeField] private List<GameObject> zombiesRight;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    private float nextActionTime2 = 0.0f;
    public float period2 = 0.1f;

    [SerializeField] private float leftStMultipier;
    [SerializeField] private float rigthStMultipier;
    [SerializeField] private float coolDownEmpujon;

    private void Start()
    {
        player = FindObjectOfType<carMovement>().gameObject;
    }
    private void Update()
    {
        if (coolDownEmpujon == 0)
        {
            if (zombiesLeft.Count > 0)
            {
                leftStMultipier += .1f * Time.deltaTime;

                if (Time.time > nextActionTime)
                {
                    nextActionTime += period + Random.Range(0.0f, 1.0f);

                    foreach (var item in zombiesLeft)
                    {
                        var force = item.gameObject.GetComponent<zombieLifePrueba>().force;
                        //var peso = item.gameObject.GetComponent<zombieLifePrueba>().peso;

                        if (force>1)
                        {
                            StartCoroutine(player.GetComponent<carMovement>().PushCarSmoothly(/*force **/ -3f /*- leftStMultipier*/, .25f + Random.Range(0.0f, 1.0f)));
                        }
                        //else if (force==1)
                        //{
                        //    StartCoroutine(player.GetComponent<carMovement>().PushCarConstantly(-force, 5));
                        //}
                    }
                }
            }
            if (zombiesRight.Count > 0)
            {
                rigthStMultipier += .1f * Time.deltaTime;

                if (Time.time > nextActionTime2)
                {
                    nextActionTime2 += period2 + Random.Range(0.0f, 1.0f);

                    foreach (var item in zombiesRight)
                    {
                        var force = item.gameObject.GetComponent<zombieLifePrueba>().force;
                        if (force>1)
                        {
                            StartCoroutine(player.GetComponent<carMovement>().PushCarSmoothly(/*force * */3f /*+ rigthStMultipier*/, .25f + Random.Range(0.0f, 1.0f)));
                        }
                        //else if (force == 1)
                        //{
                        //    StartCoroutine(player.GetComponent<carMovement>().PushCarConstantly(-force, 5));
                        //}
                    }
                }
            }
        }
        else if (coolDownEmpujon<0)
        {
            coolDownEmpujon = 0;
        }
        else if (coolDownEmpujon>0)
        {
            coolDownEmpujon -= 1*Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Zombie")
        {
            var peso = other.gameObject.GetComponent<zombieLifePrueba>().peso;

            if (other.transform.position.x < player.transform.position.x)
            {
                coolDownEmpujon = 5;

                other.transform.tag = "zombieH";
                print("this object is to the left");
                zombiesLeft.Add(other.gameObject);

                StartCoroutine(player.GetComponent<carMovement>().PushCarSmoothly(1f*peso, .25f));
            }
            else if (other.transform.position.x > player.transform.position.x)
            {
                coolDownEmpujon = 5;

                other.transform.tag = "zombieH";
                print("this object is to the right");
                zombiesRight.Add(other.gameObject);

                StartCoroutine(player.GetComponent<carMovement>().PushCarSmoothly(-1f * peso, .25f));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "zombieH")
        {
            if (other.transform.position.x < player.transform.position.x)
            {
                print("this object is to the left");
                zombiesLeft.Remove(other.gameObject);
                leftStMultipier = 0;
            }
            else if (other.transform.position.x > player.transform.position.x)
            {
                print("this object is to the right");
                zombiesRight.Remove(other.gameObject);
                rigthStMultipier = 0;
            }
        }
    }
}
