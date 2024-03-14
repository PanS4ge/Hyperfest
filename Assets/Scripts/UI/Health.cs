using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Hyperfest.Objects;
using Hyperfest.Mechanics;

namespace Hyperfest.UI
{
    public class Health : MonoBehaviour
    {
        private float lastHp;
        public float hp;
        public int maxHp;
        public Slider hpslider;
        public TextMeshProUGUI tmpugui;
        public Transform fullindicator;
        public Transform emptyindicator;
        public float empty, full;
        public float breakbetween;
        public GameObject indicator;
        public GameObject error_indicator;
        public float quick_error_blink;
        public float set_error_blink;
        public GameObject gameover;
        public bool isPlayer = false;
        public GameObject meNotPlayer;
        public GameObject meNotPlayerGlasses;
        public CubeGenerator generators;
        public GameObject meNotPlayerDestroyGO;
        public float meNotPlayerSizeX = -1;
        [Header("Regeneration info")]
        public int regenCooldown;
        public float regenTime;
        // Start is called before the first frame update
        void Start()
        {
            if(!isPlayer) {
                meNotPlayerSizeX = meNotPlayer.transform.localScale.x;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //if(Math.Floor(hp) == Math.Floor(hpslider.value * maxHp)) {
            //} else if(Math.Floor(hp) > Math.Floor(hpslider.value * maxHp)) {
            //    hpslider.value += Time.deltaTime;
            //} else if(Math.Floor(hp) < Math.Floor(hpslider.value * maxHp)) {
            //    hpslider.value -= Time.deltaTime;
            //}
            if(isPlayer) {
                hpslider.value = (float)Math.Floor(hp) / maxHp;
                tmpugui.SetText((Math.Floor(hpslider.value * 1000) / 10).ToString() + "%");
                full = fullindicator.position.y;
                empty = emptyindicator.position.y;
                breakbetween = full - empty;
                indicator.transform.position = new Vector3(indicator.transform.position.x, empty + (float)Math.Floor(breakbetween * hpslider.value), indicator.transform.position.z);
                if(Math.Floor(hp) < Math.Floor(maxHp * 0.25) && Math.Floor(hp) != 0) {
                    set_error_blink = (float)Math.Floor(hp) / (maxHp * 0.25f) * 5;
                    quick_error_blink += Time.deltaTime;
                    if(quick_error_blink > set_error_blink) {
                        error_indicator.SetActive(!error_indicator.activeSelf);
                        quick_error_blink = 0;
                    }
                } else if (Math.Floor(hp) == 0) {
                    gameover.SetActive(true);
                } else {
                    error_indicator.SetActive(false);
                }
                
                if(lastHp > hp) {
                    regenTime = 0;
                }
                if(regenTime < regenCooldown) {
                    regenTime += Time.deltaTime;
                }
                if(regenTime > regenCooldown && hp <= maxHp) {
                    hp += Time.deltaTime;
                }
                lastHp = hp;
            }
            else {
                if (hpslider)
                {
                    hpslider.value = (float)Math.Floor(hp) / maxHp;
                }
                if (Math.Floor(hp) == 0)
                {
                    meNotPlayerGlasses.transform.parent = null;
                    Destroy(meNotPlayer);
                    meNotPlayerGlasses.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    meNotPlayerGlasses.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
                    System.Random rand = new System.Random();
                    meNotPlayerGlasses.GetComponent<Rigidbody>().angularVelocity =
                        new Vector3(rand.Next(30), rand.Next(30), rand.Next(30));
                    meNotPlayerGlasses.GetComponent<BoxCollider>().enabled = true;
                    meNotPlayerGlasses.AddComponent<Vizjer>();
                    if (generators != null)
                    {
                        generators.current = generators.groundplayerpoint;
                    }

                    if (meNotPlayerDestroyGO != null)
                    {
                        Destroy(meNotPlayerDestroyGO);
                    }
                }

                meNotPlayerGlasses.GetComponent<Renderer>().material.color =
                    new Color(255 * ((maxHp - hp) / maxHp), 0, 0);
            }
        }
    }
}
