using System.Collections;
using System.Collections.Generic;
using Hyperfest.Mechanics;
using UnityEngine;

public class ButtonPull : MonoBehaviour
{
    public Material active;
    public Material disable;

    public CubeGenerator cube;

    public bool oneway;
    public bool pressed;

    public void Activate()
    {
        if(!pressed) {
            this.gameObject.GetComponent<MeshRenderer>().material = active;
            pressed = true;
            if(cube.multiple) {
                cube.multiplePull = true;
            } else {
                cube.current = cube.groundplayerpoint;
            }
        } else if(!oneway) {
            this.gameObject.GetComponent<MeshRenderer>().material = disable;
            pressed = false;
            if(cube.multiple) {
                cube.multiplePull = false;
            } else {
                cube.current = cube.generator;
            }
        }
    }
}
