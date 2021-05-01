using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    private Transform target;
    public float range = 15f;
    //public float rotationSpeed = 10f;

    public Transform defenderBody;

    private float rotationOffset = 83f;

    private Animator defenderAnimator;

    // Start is called before the first frame update
    void Start()
    {
        defenderAnimator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        RotatePrefab(defenderBody);
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
            defenderAnimator.SetTrigger("EnemyDetected");
        }
        else
		{
            target = null;
		}
	}

    void RotatePrefab(Transform partToRotate)
	{
        Vector3 directionToRotate = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToRotate);
		//Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		Vector3 rotation = lookRotation.eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0, rotation.y + rotationOffset, 0f);
    }

    void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
	}
}
