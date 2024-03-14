using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperfest.Objects
{
    public class Vizjer : MonoBehaviour
    {
        public float r, g, b;
        void Start(){
            r = GetComponent<Renderer>().material.color.r / 255;
            g = GetComponent<Renderer>().material.color.g / 255;
            b = GetComponent<Renderer>().material.color.b / 255;
        }
        void Update()
        {
            if(GetComponent<Renderer>().material.color.r <= 0){
                GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            }
            if(this.transform.localScale.y < Vector3.zero.y) {
                return;
            }
            GetComponent<Renderer>().material.color = new Color(r,g,b);
            this.transform.localScale = new Vector3(this.transform.localScale.x - Time.deltaTime / 20, this.transform.localScale.y - Time.deltaTime / 20, this.transform.localScale.z - Time.deltaTime / 20);
            r = Math.Max(r - Time.deltaTime, 0);
            g = Math.Max(g - Time.deltaTime, 0);
            b = Math.Max(b - Time.deltaTime, 0);
        }
    }
}