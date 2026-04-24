using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    public enum TriggerType { Start, End }
    public TriggerType triggerType;
    public Lava lava;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (triggerType == TriggerType.Start)
        {
            lava.StartLava();
        }
        else if (triggerType == TriggerType.End)
        {
            lava.StopLava();
        }
    }
}
