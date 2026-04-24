using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    [SerializeField] GameObject[] plates;
    [SerializeField] int platesCount;
    [SerializeField] int platesLength;
    private void Start()
    {
        for (var i = 0; i < plates.Length; i += plates.Length / platesLength)
        {
            var x = Random.Range(i, i + platesCount);
            plates[x].gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
