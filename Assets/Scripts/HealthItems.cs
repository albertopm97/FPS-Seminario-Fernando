using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItems : MonoBehaviour
{
    public int value;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventsManager.OnModifyHealth?.Invoke(value);
            Destroy(gameObject);
        }
    }
}
