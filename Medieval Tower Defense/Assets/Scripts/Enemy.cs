using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    private Animator animator;

    void Start()
    {
        target = Waypoints.points[0];
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 directionToMove = target.position - transform.position;
        transform.Translate(directionToMove.normalized * speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(directionToMove);

        if (Vector3.Distance(transform.position, target.position) <= .1f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
