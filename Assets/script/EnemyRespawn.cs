using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    Vector3 curentCheckpoint;
    [SerializeField] float fallPoint = -20f;
    public void Respawn()
    {
        transform.position = curentCheckpoint;
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
}
