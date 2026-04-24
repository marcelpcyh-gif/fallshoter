using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pendulum : MonoBehaviour
{
    [SerializeField] float limit = 75f;
    [SerializeField] float speed = 1f;
    void Update()
    {
        float angle = limit * Mathf.Sin(Time.time * speed);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

}
