using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Odniesienie do prefabu bomby
    [SerializeField] GameObject projectilePrefab;
    // Punkt, w którym pojawi siê bomba
    [SerializeField] Transform firePoint;
    // Czas pomiêdzy wystrza³ami armaty
    [SerializeField] float fireInterval = 3f;
    // Si³a wystrza³u
    [SerializeField] float launchForce = 500f;
    private void Start()
    {
        InvokeRepeating("Fire", 0f, fireInterval);
    }
    void Fire()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * launchForce);
    }
}
