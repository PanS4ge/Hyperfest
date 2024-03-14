using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperfest.Objects
{
    public class Enemy : MonoBehaviour
    {
        public Agent agent;
        public float distanceNeeded = 5;
        public float currentDistance = -1;
        public GameObject player;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            currentDistance = Vector3.Distance(player.transform.position, this.transform.position);
            agent.isEnabled = distanceNeeded <= currentDistance;
        }
    }
}