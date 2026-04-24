using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // To jest si³a, z któr¹ odepchniemy gracza.
    [SerializeField] float force = 20f;
    // To jest kierunek, w którym gracz zostanie odepchniêty.
    private Vector3 hitDir;
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Sprawdzenie: "Czy to gracz?"
            if (collision.gameObject.tag == "Player")
            {
                // Kierunek, z którego nadszed³ impakt — to znaczy, ¿e musimy odepchn¹æ gracza.
                hitDir = contact.normal;
                // Branie fizyki gracza (Rigidbody) i odpychanie go z dan¹ si³¹.
                collision.gameObject.GetComponent<Rigidbody>().AddForce(-hitDir * force, ForceMode.Impulse);
                // Powiedzenie: "Wystarczy! Ju¿ popchnêliœmy gracza, mo¿emy przestaæ."
                return;
            }

        }
    }

}
