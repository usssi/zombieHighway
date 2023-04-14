using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class carMovement : MonoBehaviour
{
    public float acceleration = 10f; 
    public float turnSpeed = 0f;
    public float turningSpeed; //la que se actualiza con dificultad

    public float turnSpeedMultiplier = 1f; // se actualiza con dificultad

    public bool isKeyReleased = false;
    public bool isKeyPressed = false;

    public float lerpTimeRotation = 0.5f;

    public GameObject worldMover;

    public bool isAlive;
    public bool canDie;


    private void Start()
    {
        isAlive = true;
    }
    private void Update()
    {
        if (!canDie)//to test invincibility 
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;
        }

        if (isAlive)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.z = -40f;
            transform.position = currentPosition;

            float horizontalInput = Input.GetAxis("Horizontal");

            if (transform.rotation.eulerAngles.y < 25 || transform.rotation.eulerAngles.y > 335)
            {
                transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);
            }

            transform.Translate(-Vector3.left * horizontalInput * acceleration * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isKeyReleased = false;
                isKeyPressed = true;

            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                isKeyReleased = true;
                isKeyPressed = false;
            }

            if (isKeyReleased == true)
            {
                Quaternion currentRotation = transform.rotation;
                Quaternion newRotation = Quaternion.Lerp(currentRotation, Quaternion.identity, lerpTimeRotation);
                transform.rotation = newRotation;

                if (turnSpeed > turningSpeed)
                {
                    turnSpeed = Mathf.Lerp(turnSpeed, turningSpeed, (Time.time - 0) / 3);
                }
                else if (turnSpeed < turningSpeed)
                {
                    turnSpeed = turningSpeed;
                }
            }

            if (isKeyPressed)
            {
                turnSpeed += turnSpeedMultiplier * Time.deltaTime;
            }
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.forward * -worldMover.GetComponent<WorldMoveRespawn>().worldMoveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "obstaculo")
        {
            if (canDie)
            {
                isAlive = false;
                print("chocaste");
                Invoke("RecargarEscena", 2f);
            }
        }
    }
    private void RecargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator PushCarSmoothly(float pushForce, float pushTime)
    {
        Vector3 direction = transform.right;

        float timeElapsed = 0f;
        float forceMagnitude = 0f;

        while (timeElapsed < pushTime)
        {
            timeElapsed += Time.deltaTime;

            forceMagnitude = Mathf.Lerp(0f, pushForce, timeElapsed / pushTime);

            transform.position += direction * forceMagnitude * Time.deltaTime;

            yield return null;
        }
    }
}
