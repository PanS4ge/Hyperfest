using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperfest.Objects
{
    public class Spinner : MonoBehaviour
    {
        public Vector3 speed;
        void Update()
        {
            this.transform.Rotate(speed);
        }
    }
}