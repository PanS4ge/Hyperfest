using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using Hyperfest.Objects;
using Hyperfest.UI;

namespace Hyperfest.Guns
{
    public class Bullet : MonoBehaviour
    {
        public GameObject bulletspinner;
        public int radius;
        public bool juzzuzyte;
        public int damage = 3;
        public string shotby = "";

        void Update()
        {
            RaycastHit hit;

            if (juzzuzyte)
            {
                return;
            }

            if (Physics.SphereCast(this.transform.position, radius, transform.forward, out hit, 10))
            {
                if (hit.transform.gameObject.name.Contains(shotby))
                {
                    return;
                }

                if (hit.transform.gameObject.GetComponent<MyGenerator>())
                {
                    // Usuń gameobject z tablicy CubesITakeCare
                    MyGenerator myGenerator = hit.transform.gameObject.GetComponent<MyGenerator>();
                    GameObject[] cubesArray = myGenerator.gen.CubesITakeCare;

                    // Znajdź indeks gameobjectu do usunięcia
                    int indexToRemove = System.Array.IndexOf(cubesArray, hit.transform.gameObject);

                    // Usuń rigidbody z tablicy rb
                    Rigidbody[] rbArray = myGenerator.gen.rb;

                    // Sprawdź, czy indeks istnieje w tablicy
                    if (indexToRemove != -1 && indexToRemove < rbArray.Length)
                    {
                        // Usuń rigidbody z tablicy rb
                        System.Collections.Generic.List<Rigidbody> rbList =
                            new System.Collections.Generic.List<Rigidbody>(rbArray);
                        Destroy(rbList[indexToRemove]);
                        rbList.RemoveAt(indexToRemove);
                        myGenerator.gen.rb = rbList.ToArray();
                    }

                    // Sprawdź, czy indeks istnieje w tablicy
                    if (indexToRemove != -1)
                    {
                        // Usuń gameobject z tablicy CubesITakeCare
                        System.Collections.Generic.List<GameObject> cubesList =
                            new System.Collections.Generic.List<GameObject>(cubesArray);
                        Destroy(cubesList[indexToRemove]);
                        cubesList.RemoveAt(indexToRemove);
                        myGenerator.gen.CubesITakeCare = cubesList.ToArray();
                    }

                    bulletspinner.SetActive(false);
                    juzzuzyte = true;
                }

                if (hit.transform.gameObject.GetComponent<Health>())
                {
                    if (hit.transform.gameObject.GetComponent<Health>().hp > damage)
                    {
                        hit.transform.gameObject.GetComponent<Health>().hp -= damage;
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<Health>().hp = 0;
                    }

                    bulletspinner.SetActive(false);
                    juzzuzyte = true;
                }

                Debug.Log(shotby + ": " + hit.transform.gameObject);
            }
        }
    }
}