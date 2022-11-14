using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : Plane
{
    public Transform player;
    public float radarSize;
    public LayerMask ENEMY_LAYER;
    public int safety;

    private void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    private void Update()
    {
        ApplyDirection();
        base.Update();
    }

    void ApplyDirection()
    {
        List<Collider2D> enemies = new List<Collider2D>(EnemiesInRange(radarSize, ENEMY_LAYER));  // Put all nearby Enemies Into a List
        enemies.Remove(gameObject.GetComponent<Collider2D>()); // Remove itself from that list

        float angleTowardsPlayer = GetAngleTowards(player.transform);
        float angleAwayFromEnemies = 0;

        // Calculate Angle Away From Nearby Enemies
        for (int i = 0; i < enemies.Count; i++)
        {
            angleAwayFromEnemies = (angleAwayFromEnemies + (GetAngleAway(enemies[i].gameObject.transform)) / 2);
        }

        float targetAngle;
        if (enemies.Count > 0)
        {
            targetAngle = (angleTowardsPlayer + (angleAwayFromEnemies * (safety - 1))) / safety;
        }
        else
        {
            targetAngle = angleTowardsPlayer;
        }
        enemies.Clear();
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    float GetAngleTowards(Transform _target)
    {
        Vector3 diff = _target.position - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    float GetAngleAway(Transform _target)
    {
        Vector3 diff = transform.position - _target.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    Collider2D[] EnemiesInRange(float _radarSize, LayerMask _avoidThese)
    {
        return Physics2D.OverlapCircleAll(transform.position, _radarSize, _avoidThese);
    }
}

