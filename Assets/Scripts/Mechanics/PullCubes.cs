using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hyperfest.Mechanics
{
    public class PullCubes : MonoBehaviour
    {
        public CubeGenerator[] generators;
        public GameObject player;
        public int pullDistance;
        public bool reverse;

        float getDistance(Transform trans1, Transform trans2)
        {
            Vector3 v1 = trans1.position;
            Vector3 v2 = trans2.position;
            float x1 = (v2.x - v1.x) * (v2.x - v1.x);
            float z1 = (v2.z - v1.z) * (v2.z - v1.z);
            double c1 = Math.Sqrt(x1 + z1) * Math.Sqrt(x1 + z1);
            float y1 = (v2.y - v1.y) * (v2.y - v1.y);
            double c2 = Math.Sqrt(c1 + y1);
            return (float)c2;
        }

        // Start is called before the first frame update
        void Start()
        {
            foreach (CubeGenerator cube in generators)
            {
                cube.groundplayerpoint = this.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (getDistance(player.transform, this.transform) < pullDistance)
            {
                foreach (CubeGenerator cube in generators)
                {
                    if (cube.multiple)
                    {
                        if (reverse)
                        {
                            cube.multiplePull = false;
                        }
                        else
                        {
                            cube.multiplePull = true;
                        }
                    }
                    else
                    {
                        cube.current = this.transform;
                    }
                }
            }
            else
            {
                foreach (CubeGenerator cube in generators)
                {
                    if (cube.multiple)
                    {
                        if (!reverse)
                        {
                            cube.multiplePull = false;
                        }
                        else
                        {
                            cube.multiplePull = true;
                        }
                    }
                    else
                    {
                        cube.current = cube.thisobj.transform;
                    }
                }
            }
        }
    }
}