using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Missile : Plane
    {
        public Transform player;

        private void Start()
        {
            base.Start();
            player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }

        private void Update()
        {
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, GetAngleTowards(player) - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            base.Update();
        }

        float GetAngleTowards(Transform _target)
        {
            Vector3 diff = _target.position - transform.position;
            diff.Normalize();

            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            return rotZ;
        }
    }
