using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperfest.Objects
{
    public class ShowTutorial : MonoBehaviour
    {
        public GameObject tutorial;

        private void OnTriggerEnter(Collider other)
        {
            tutorial.SetActive(true);
        }
     
        private void OnTriggerExit(Collider other)
        {
            tutorial.SetActive(false);
        }
    }
}