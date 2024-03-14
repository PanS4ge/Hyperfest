using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Hyperfest.Objects;

namespace Hyperfest.Mechanics
{
    public class CubeGenerator : MonoBehaviour
    {
        public GameObject[] CubesITakeCare = new GameObject[]{null, null, null, null, null, null, null, null, null, null};
        public Rigidbody[] rb = new Rigidbody[]{null, null, null, null, null, null, null, null, null, null};
        public Material[] materials;
        public Vector3 scale;
        public float power = 30f;
        public float powerAsGnd = 15f;
        public float distance = 30f;
        public float smooth = 20f;
        public float slowdownDistance = 8f;
        public float slowdownCuts = 1.05f;
        public Transform generator;
        public Transform groundplayerpoint;
        public Transform current;
        public Transform[] currents;
        public bool multiple;
        public bool multiplePull;
        public bool debug;
        private float smoothnessDebug = 0;
        public float gndCubeMass = 0.025f;
        public float cubeMass = 1f;
        public GameObject thisobj;
        // Start is called before the first frame update

        float getDistance(Transform trans1, Transform trans2){
            Vector3 v1 = trans1.position;
            Vector3 v2 = trans2.position;
            float x1 = (v2.x - v1.x)*(v2.x - v1.x);
            float z1 = (v2.z - v1.z)*(v2.z - v1.z);
            double c1 = Math.Sqrt(x1+z1)*Math.Sqrt(x1+z1);
            float y1 = (v2.y - v1.y)*(v2.y - v1.y);
            double c2 = Math.Sqrt(c1+y1);
            return (float)c2;
        }

        void Start()
        {
            thisobj = this.gameObject;
            generator = this.transform;
            if(current == null){
                current = generator;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //if(multiple && currents.Length <= CubesITakeCare.Length) {
            //    Debug.Log("On " + this.gameObject.name + " there's a problem with currents and citc count");
            //}
            if(CubesITakeCare.Length != rb.Length) {
                Debug.Log("On " + this.gameObject.name + " there's a problem with citc and rb count");
            }
            for (int i = 0; i < CubesITakeCare.Length; i++) {
                if(CubesITakeCare[i] == null){
                    CubesITakeCare[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    rb[i] = CubesITakeCare[i].AddComponent<Rigidbody>();
                    CubesITakeCare[i].AddComponent<IsItGenerated>();
                    MyGenerator g = CubesITakeCare[i].AddComponent<MyGenerator>();
                    g.gen = this;

                    System.Random random = new System.Random();
                    int start2 = random.Next(0, materials.Length);

                    TrailRenderer tr = CubesITakeCare[i].AddComponent<TrailRenderer>();
                    AnimationCurve curve = new AnimationCurve();
                    curve.AddKey(0.0f, 0.0f);
                    curve.AddKey(1.0f, 0.1f);
                    tr.widthCurve = curve;
                    tr.time = 0.3f;
                    tr.materials[0] = materials[start2];

                    CubesITakeCare[i].layer = 6;
                    CubesITakeCare[i].transform.localScale = scale;
                    CubesITakeCare[i].transform.position = new Vector3(generator.position.x + random.Next(-12, 12), generator.position.y + random.Next(-12, 12), generator.position.z + random.Next(-12, 12));
                    CubesITakeCare[i].GetComponent<MeshRenderer>().material = materials[start2];
                }
                float test = getDistance(CubesITakeCare[i].transform, current) / smooth;
                if(test >= 1) {
                    test = 1;
                } else {
                    test = test - (float)Math.Floor(test);
                }
                if(CubesITakeCare[i].GetComponent<IsItGenerated>()){
                    if(CubesITakeCare[i].GetComponent<IsItGenerated>().isDisabled){
                        continue;
                    }
                }
                if(!multiple) {
                    if(current == generator){
                        rb[i].constraints = RigidbodyConstraints.None;
                        rb[i].mass = cubeMass;
                    //if(getDistance(current, CubesITakeCare[i].transform) <= distance) {
                        if(current.position.y - CubesITakeCare[i].transform.position.y <= 0) {
                            rb[i].AddForce((current.position - CubesITakeCare[i].transform.position).x * test, -1 * power * test, (current.position - CubesITakeCare[i].transform.position).z * test);
                            if(debug) Debug.Log("Above, i: " + i.ToString() + ", Yg: " + current.position.y.ToString() + ", Yc: " + CubesITakeCare[i].transform.position.y.ToString() + ", Dis: " + getDistance(current, CubesITakeCare[i].transform).ToString());
                        }
                        else if(current.position.y - CubesITakeCare[i].transform.position.y >= 0) {
                            rb[i].AddForce((current.position - CubesITakeCare[i].transform.position).x * test, power * test, (current.position - CubesITakeCare[i].transform.position).z * test);
                            if(debug) Debug.Log("Under, i: " + i.ToString() + ", Yg: " + current.position.y.ToString() + ", Yc: " + CubesITakeCare[i].transform.position.y.ToString() + ", Dis: " + getDistance(current, CubesITakeCare[i].transform).ToString());
                        }
                    //}
                    }
                    if(current == groundplayerpoint) {
                        CubesITakeCare[i].transform.rotation = Quaternion.Lerp(CubesITakeCare[i].transform.rotation, Quaternion.Euler(0f, 0f, 0f), 1.0f * Time.deltaTime);
                        rb[i].constraints = RigidbodyConstraints.FreezeRotation;
                        rb[i].mass = gndCubeMass;
                        //if(CubesITakeCare[i].transform.position.y - current.position.y == 0) {
                        //    rb[i].AddForce((current.position - CubesITakeCare[i].transform.position).x * test, 0, (current.position - CubesITakeCare[i].transform.position).z * test);
                        //    if(debug) Debug.Log("Same, i: " + i.ToString() + ", Yg: " + current.position.y.ToString() + ", Yc: " + CubesITakeCare[i].transform.position.y.ToString() + ", Dis: " + getDistance(current, CubesITakeCare[i].transform).ToString());
                        //}
                        //else if(current.position.y - CubesITakeCare[i].transform.position.y < 0) {
                        //    rb[i].AddForce((current.position - CubesITakeCare[i].transform.position).x * test, -1 * powerAsGnd * test, (current.position - CubesITakeCare[i].transform.position).z * test);
                        //    if(debug) Debug.Log("Above, i: " + i.ToString() + ", Yg: " + current.position.y.ToString() + ", Yc: " + CubesITakeCare[i].transform.position.y.ToString() + ", Dis: " + getDistance(current, CubesITakeCare[i].transform).ToString());
                        //}
                        //else if(current.position.y - CubesITakeCare[i].transform.position.y > 0) {
                        //    rb[i].AddForce((current.position - CubesITakeCare[i].transform.position).x * test, powerAsGnd * test, (current.position - CubesITakeCare[i].transform.position).z * test);
                        //    if(debug) Debug.Log("Under, i: " + i.ToString() + ", Yg: " + current.position.y.ToString() + ", Yc: " + CubesITakeCare[i].transform.position.y.ToString() + ", Dis: " + getDistance(current, CubesITakeCare[i].transform).ToString());
                        //}
                        rb[i].velocity = (current.position - CubesITakeCare[i].transform.position) * test * powerAsGnd;
                    }
                    if(getDistance(CubesITakeCare[i].transform, current) <= slowdownDistance) {
                        rb[i].velocity = rb[i].velocity / slowdownCuts;
                    }
                } else {
                    if(multiplePull) {
                        rb[i].constraints = RigidbodyConstraints.FreezeRotation;
                        rb[i].mass = gndCubeMass;
                        CubesITakeCare[i].transform.rotation = Quaternion.Lerp(CubesITakeCare[i].transform.rotation, Quaternion.Euler(0f, 0f, 0f), 1.0f * Time.deltaTime);
                        if(CubesITakeCare[i].transform.position != currents[i].position) {
                            rb[i].velocity = (currents[i].position - CubesITakeCare[i].transform.position) * test * power;
                        } else {
                            rb[i].velocity = new Vector3(0, 0, 0);
                        }
                        if(getDistance(CubesITakeCare[i].transform, current) <= slowdownDistance) {
                            rb[i].velocity = rb[i].velocity / slowdownCuts;
                        }
                    } else {
                        current = generator;
                        rb[i].mass = cubeMass;
                        rb[i].constraints = RigidbodyConstraints.None;
                        if(current.position.y - CubesITakeCare[i].transform.position.y <= 0) {
                            rb[i].AddForce((current.position - CubesITakeCare[i].transform.position).x * test, -1 * power * test, (current.position - CubesITakeCare[i].transform.position).z * test);
                            if(debug) Debug.Log("Above, i: " + i.ToString() + ", Yg: " + current.position.y.ToString() + ", Yc: " + CubesITakeCare[i].transform.position.y.ToString() + ", Dis: " + getDistance(current, CubesITakeCare[i].transform).ToString());
                        }
                        else if(current.position.y - CubesITakeCare[i].transform.position.y >= 0) {
                            rb[i].AddForce((current.position - CubesITakeCare[i].transform.position).x * test, power * test, (current.position - CubesITakeCare[i].transform.position).z * test);
                            if(debug) Debug.Log("Under, i: " + i.ToString() + ", Yg: " + current.position.y.ToString() + ", Yc: " + CubesITakeCare[i].transform.position.y.ToString() + ", Dis: " + getDistance(current, CubesITakeCare[i].transform).ToString());
                        }
                        System.Random random = new System.Random();
                        rb[i].velocity = new Vector3(rb[i].velocity.x + random.Next(-8, 8), rb[i].velocity.y + random.Next(-8, 8), rb[i].velocity.z + random.Next(-8, 8));
                        if(getDistance(CubesITakeCare[i].transform, current) <= slowdownDistance) {
                            rb[i].velocity = rb[i].velocity / slowdownCuts;
                        }
                    }
                }
            }
        }
    }
}