using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Hyperfest.Guns
{
    public class Gun : MonoBehaviour
    {
        public GameObject bullet;
        public GameObject lastBullet;
        public Rigidbody lastBulletRb;
        public GameObject gunTip;
        public int pewpew;
        public int cooldown = 1;
        public float lastpewpew = 0;
        public string shotby = "Player";

        public LineRenderer lr;

        private void Awake()
        {
            if (lr == null)
            {
                lr = gunTip.GetComponent<LineRenderer>();
            }
        }

        void Update()
        {
            lr.enabled = false;
            lr.SetPosition(0, gunTip.transform.position);

            RaycastHit hit;
                
            if(Physics.Raycast(transform.position, transform.forward, out hit,Mathf.Infinity)) {
                lr.SetPosition(1, hit.point);
                lr.enabled = true;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            lastpewpew += Time.deltaTime;
        }

        /// <summary>
        /// Call whenever we want to start a grapple
        /// </summary>
        void Shoot()
        {
            if (!(cooldown < lastpewpew))
            {
                return;
            }

            lastBullet = Instantiate(bullet, gunTip.transform.position, Quaternion.identity) as GameObject;
            lastBullet.GetComponent<Bullet>().shotby = shotby;
            lastBulletRb = lastBullet.GetComponent<Rigidbody>();
            lastBulletRb.AddForce(gunTip.transform.forward * pewpew);
            lastpewpew = 0;
        }
        
        private void OnDrawGizmos()
        {
            var forward = transform.forward;
            var pos = gunTip.transform.position;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pos, pos + forward * 100);
        }
    }
}