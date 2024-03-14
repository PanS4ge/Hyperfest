using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyperfest.Managers;

namespace Hyperfest.UI
{
    public class GunSelect : MonoBehaviour
    {
        public GameObject pictures;
        public GameObject player;
        public KeyCode[] numerki;
        public KeyCode forward;
        public KeyCode backward;
        public GameObject[] bronie;
        public int current = 0;

        public KeyManager keyManager;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            forward = keyManager.findInputYouWant("Go Back Weapon").getCurrent().keyCode;
            backward = keyManager.findInputYouWant("Go Forward Weapon").getCurrent().keyCode;
            if (Input.GetKeyDown(forward))
            {
                current = (current + 1);
                if (current > bronie.Length)
                {
                    current = 0;
                }

                for (int j = 0; j < bronie.Length; j++)
                {
                    if (current == j)
                    {
                        bronie[j].SetActive(true);
                    }
                    else
                    {
                        bronie[j].SetActive(false);
                    }
                }

                if (player.GetComponent<SpringJoint>())
                {
                    Destroy(player.GetComponent<SpringJoint>());
                }
            }
            else if (Input.GetKeyDown(backward))
            {
                current = (current - 1);
                if (current == -1)
                {
                    current = bronie.Length - 1;
                }

                for (int j = 0; j < bronie.Length; j++)
                {
                    if (current == j)
                    {
                        bronie[j].SetActive(true);
                    }
                    else
                    {
                        bronie[j].SetActive(false);
                    }
                }

                if (player.GetComponent<SpringJoint>())
                {
                    Destroy(player.GetComponent<SpringJoint>());
                }
            }

            for (int i = 0; i < bronie.Length; i++)
            {
                if (Input.GetKeyDown(numerki[i]))
                {
                    for (int j = 0; j < bronie.Length; j++)
                    {
                        if (i == j)
                        {
                            bronie[j].SetActive(true);
                            current = j;
                        }
                        else
                        {
                            bronie[j].SetActive(false);
                        }
                    }

                    if (player.GetComponent<SpringJoint>())
                    {
                        Destroy(player.GetComponent<SpringJoint>());
                    }
                }

                if (current == 0)
                {
                    pictures.transform.rotation = Quaternion.Lerp(pictures.transform.rotation,
                        Quaternion.Euler(90f, 0f, 0f), 1.0f * Time.deltaTime);
                }
                else
                {
                    pictures.transform.rotation = Quaternion.Lerp(pictures.transform.rotation,
                        Quaternion.Euler(0f, 0f, -20f), 1.0f * Time.deltaTime);
                }
            }
        }
    }
}