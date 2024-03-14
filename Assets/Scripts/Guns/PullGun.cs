using UnityEngine;

namespace Hyperfest.Guns
{
    public class PullGun : MonoBehaviour
    {

        private LineRenderer lr;
        private Vector3 grapplePoint;
        public LayerMask whatIsGrappleable;
        public Transform gunTip, camera, player;
        private SpringJoint joint;
        public Rigidbody playerRb;
        public GameObject last;
        public GameObject halo;
        public float maxDistance = 100f;
        public float spring = 6f;
        public float damper = 7f;
        public float massScale = 4.5f;
        public float PullPower = 69f;

        void Awake()
        {
            lr = GetComponent<LineRenderer>();
        }

        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
            {
                if (last != hit.transform.gameObject && hit.transform.gameObject.GetComponent<ButtonPull>())
                {
                    Destroy(last);
                    last = hit.transform.gameObject;
                    //indicator = hit.transform.gameObject.AddComponent<Halo>();
                    last = Instantiate(halo, last.transform.position, Quaternion.identity) as GameObject;
                    last.transform.parent = last.transform;
                }
            }
            else
            {
                Destroy(last);
            }

            if (Input.GetMouseButtonDown(0))
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
            }
        }

        //Called after Update
        void LateUpdate()
        {
            DrawRope();
        }

        /// <summary>
        /// Call whenever we want to start a grapple
        /// </summary>
        void StartGrapple()
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
            {
                grapplePoint = hit.point;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;

                float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

                //The distance grapple will try to keep from grapple point. 
                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance = distanceFromPoint * 0.25f;

                //Adjust these values to fit your game.
                joint.spring = spring;
                joint.damper = damper;
                joint.massScale = massScale;

                lr.positionCount = 2;
                currentGrapplePosition = gunTip.position;
            }
        }

        private Vector3 currentGrapplePosition;

        /// <summary>
        /// Call whenever we want to stop a grapple
        /// </summary>
        void StopGrapple()
        {
            if (lr.positionCount != 0 && Vector3.Distance(player.position, grapplePoint) <= maxDistance)
            {
                playerRb.AddForce((currentGrapplePosition - player.position) * PullPower *
                    Vector3.Distance(player.position, grapplePoint) / maxDistance);
            }

            lr.positionCount = 0;
            Destroy(joint);
        }


        void DrawRope()
        {
            //If not grappling, don't draw rope
            if (!joint) return;

            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, currentGrapplePosition);
        }

        public bool IsGrappling()
        {
            return joint != null;
        }

        public Vector3 GetGrapplePoint()
        {
            return grapplePoint;
        }
    }
}