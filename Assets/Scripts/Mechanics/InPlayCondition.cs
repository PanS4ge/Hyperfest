using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyperfest.UI;
using Hyperfest.Managers;

namespace Hyperfest.Mechanics
{
    public class InPlayCondition : MonoBehaviour
    {
        public bool playing = true;

        public Transform posminus;
        public Transform posplus;
        public Transform posplay;

        public GameObject gameover;

        public KeyManager keymanager;

        public MoveScene ms;
        public string inputkey;
        public float dmg_multiply = 1;
        void Start()
        {
            posplay = this.gameObject.transform;
            gameover.SetActive(false);
        }

        void Update()
        {
            if(!playing) {
                if(Input.GetKeyDown(keymanager.findInputYouWant(inputkey).getCurrent().keyCode)){
                    ms.ResetScene();
                }
            }
            if((posminus.position.x < posplay.position.x && posplay.position.x < posplus.position.x) &&
               (posminus.position.y < posplay.position.y && posplay.position.y < posplus.position.y) &&
               (posminus.position.z < posplay.position.z && posplay.position.z < posplus.position.z)){
                dmg_multiply = -1;
                return;
            } else {
                if(this.GetComponent<Health>().hp <= 0) {
                    gameover.SetActive(true);
                    dmg_multiply = 1;
                    playing = false;
                } else {
                    this.GetComponent<Health>().hp -= Time.deltaTime * dmg_multiply;
                    dmg_multiply += 0.3f;
                    dmg_multiply = Math.Min(50, dmg_multiply);
                }
            }
        }
    }
}