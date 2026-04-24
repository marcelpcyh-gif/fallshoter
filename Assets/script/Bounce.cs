using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] Transform springObject;
    [SerializeField] float compressScale = 0.7f;
    [SerializeField] float stretchSpeed = 5f;
    [SerializeField] int force;
    bool isCompressing = false;
    Vector3 one;

    float compressDuration = 0.5f;
    float compressTime = 0f;
    float bounceDelay = 0.1f;
    float bounceTime = 0f;
    bool canBounce = false;
    void Start()
    {
        one = springObject.localScale;
    }

    void Update()
    {
        if (isCompressing)
        {
            springObject.localScale = Vector3.Lerp(springObject.localScale, new Vector3(1, compressScale, 1), Time.deltaTime * stretchSpeed);
            compressTime += Time.deltaTime;

            if (compressTime >= compressDuration)
            {
                canBounce = true;
            }
        }
        else
        {
            springObject.localScale = Vector3.Lerp(springObject.localScale, one, Time.deltaTime * stretchSpeed);
        }
        if (canBounce)
        {
            Collider playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
            Rigidbody playerRigidbody = playerCollider.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            isCompressing = false;
            canBounce = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isCompressing = true;
            compressTime = 0f;
            bounceTime = 0f;
            canBounce = false;
        }
    }
}
