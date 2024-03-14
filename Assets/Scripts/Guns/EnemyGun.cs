using UnityEngine;
using Hyperfest.Objects;

namespace Hyperfest.Guns
{
    public class EnemyGun : MonoBehaviour
    {
        public GameObject bullet;
        public GameObject lastBullet;
        public Rigidbody lastBulletRb;
        public GameObject gunTip;
        public GameObject player;
        public int pewpew;
        public Enemy enemy;
        public float rechargeTime = 5;
        public float currentRecharge = 0;
        public string shotby = "Enemy";

        void Update()
        {
            if (enemy.distanceNeeded > enemy.currentDistance && currentRecharge >= rechargeTime)
            {
                currentRecharge = 0;
                Shoot();
            }

            if (currentRecharge <= rechargeTime)
            {
                currentRecharge += Time.deltaTime;
            }
        }

        /// <summary>
        /// Call whenever we want to start a grapple
        /// </summary>
        void Shoot()
        {
            this.transform.LookAt(player.transform);
            lastBullet = Instantiate(bullet, gunTip.transform.position, Quaternion.identity) as GameObject;
            lastBullet.GetComponent<Bullet>().shotby = shotby;
            lastBulletRb = lastBullet.GetComponent<Rigidbody>();
            lastBulletRb.AddForce(gunTip.transform.forward * pewpew);
        }
        
        
        private void OnDrawGizmos()
        {
            var forward = transform.forward;
            var pos = gunTip.transform.position;
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(pos, pos + forward * 100);
        }
    }
}