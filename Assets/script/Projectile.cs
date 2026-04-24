using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Czas przed eksplozj¹
    [SerializeField] float explosionDelay = 5f;
    // Promieñ wybuchu (dystans od bomby, w którym gracz zostanie odepchniêty)
    [SerializeField] float explosionRadius = 10f;
    // Si³a eksplozji (jak mocno gracz zostanie odepchniêty)
    [SerializeField] float explosionForce = 700f;
    // Odniesienie do efektu eksplozji
    [SerializeField] GameObject explosion;
    // Odniesienie do prefabu Canvas
    [SerializeField] GameObject timerUIPrefab;
    // Obiekt do klonowania Canvasa
    GameObject timerUI;
    // Odniesienie do tekstu timera
    TextMeshProUGUI timerText;

    void Explode()
    {
        Collider[] coliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in coliders)
        {
            if (hit.CompareTag("Player") && hit.attachedRigidbody != null)
            {
                hit.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(timerUI);
    }
    void Update()
    {
        explosionDelay -= Time.deltaTime;

        if (explosionDelay <= 0f)
        {
            Explode();
        }
        timerUI.transform.position = transform.position + Vector3.up * 1.5f;
        timerUI.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        timerText.text = explosionDelay.ToString("F1");
        
    }
    void Start()
    {
        // Tworzenie UI nad pociskiem
        timerUI = Instantiate(timerUIPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        // Od³¹czanie go od pocisku
        timerUI.transform.SetParent(null);
        // Znajdowanie tekstu timera i przechowywanie go w zmiennej
        timerText = timerUI.GetComponentInChildren<TextMeshProUGUI>();
        explosionDelay = Random.Range(1, explosionDelay);
    }
}
