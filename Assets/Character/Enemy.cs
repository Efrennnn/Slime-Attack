using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySpawner spawner; 
    public int spawnPointIndex; 

    public void TakeDamage()
    {
        Die();
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDestroyed(spawnPointIndex);
        }

        Destroy(gameObject);
    }
}
