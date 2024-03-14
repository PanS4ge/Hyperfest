using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperfest.Mechanics
{
    public class ButtonKill : MonoBehaviour
    {
        public Material active;

        public GameObject destroy;

        public bool pressed;

        public void Activate()
        {
            if(!pressed) {
                this.gameObject.GetComponent<MeshRenderer>().material = active;
                pressed = true;
                Destroy(destroy);
            }
        }
    }
}