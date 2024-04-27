using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    float actualHealth;

    private void OnEnable()
    {
        EventsManager.OnEnemyHitted += hit;
    }

    private void OnDisable()
    {
        EventsManager.OnEnemyHitted -= hit;
    }

    // Start is called before the first frame update
    void Start()
    {
        actualHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hit(GameObject enemy, float damage)
    {
        if(enemy == gameObject) 
        {
            actualHealth -= damage;
        }

        if(actualHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
