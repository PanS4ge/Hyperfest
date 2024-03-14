using UnityEngine;

namespace Hyperfest.Guns
{
    public class RotatePullGun : MonoBehaviour {

        public PullGun grappling;

        private Quaternion desiredRotation;
        private float rotationSpeed = 5f;

        void Update() {
            if (!grappling.IsGrappling()) {
                desiredRotation = transform.parent.rotation;
            }
            else {
                desiredRotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }

    }
}