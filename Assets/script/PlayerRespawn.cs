using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerRespawn : MonoBehaviour
{
    Vector3 curentCheckpoint;
    [SerializeField] float fallPoint = -20f;
    public Lava lava;
    public void Respawn()
    {
        transform.position = curentCheckpoint;
        lava.StopLava();
    }

    // Start is called before the first frame update
    void Start()
    {
        curentCheckpoint = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallPoint)
        {
            Respawn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("checpoint")) { 
            curentCheckpoint = other.transform.position;
        }
    }
}
