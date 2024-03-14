using System;
using Hyperfest.Mechanics;
using UnityEngine;

namespace Hyperfest.Player
{
    public class MoveCamera : MonoBehaviour {
        [SerializeField]
        public InPlayCondition ipc = new InPlayCondition();
        public Transform player;

        public ParticleSystem animespeedps;
        public ParticleSystem.EmissionModule animespeed;

        private void Awake()
        {
            animespeed = animespeedps.emission;
        }

        float Pithagoras(Vector3 speed)
        {
            float speed2d = (float)Math.Sqrt((speed.x * speed.x) + (speed.z * speed.z));
            float speed3d = (float)Math.Sqrt((speed2d * speed2d) + (speed.y * speed.y));
            return speed3d;
        }
        void Update() {
            if(ipc.playing){
                transform.position = player.transform.position;
            } else {
                transform.rotation = Quaternion.Euler(70, transform.rotation.y, transform.rotation.z);
            }

            //ParticleSystem.MinMaxCurve mmcurve = new ParticleSystem.MinMaxCurve();
            //mmcurve.curveMin = new AnimationCurve(new Keyframe(0, (int)Math.Floor(Pithagoras(player.gameObject.GetComponent<Rigidbody>().velocity))));
            //mmcurve.curveMax = new AnimationCurve(new Keyframe(10, (int)Math.Floor(Pithagoras(player.gameObject.GetComponent<Rigidbody>().velocity))));
            
            //animespeed.rateOverTime = mmcurve;
            animespeedps.maxParticles =
                (int)Math.Floor(Pithagoras(player.gameObject.GetComponent<Rigidbody>().velocity) * 2.666666666f);
        }
    }
}