using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    private Transform target;
    public float range = 15f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
    }

    void UpdateTarget()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
			{
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
			}
		}

        if(nearestEnemy != null && shortestDistance <= range)
		{
            target = nearestEnemy.transform;
		}
        else
		{
            target = null;
		}
	}

    void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
	}
}
