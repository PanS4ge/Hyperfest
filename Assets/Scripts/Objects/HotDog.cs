using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace Hyperfest.Objects
{
    public class HotDog : MonoBehaviour
    {
        public GameObject player;

        public GameObject[] neededToDie;
        public Vector3 localScale;

        public void Awake()
        {
            localScale = transform.localScale;
        }

        public int count()
        {
            int cnt = 0;
            if(neededToDie.Length != 0) {
                foreach(GameObject go in neededToDie) {
                    if(go == null)
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
        private void Update()
        {
            if (neededToDie.Length == 0)
            {
                return;
            }

            transform.localScale = localScale * (count() / neededToDie.Length);
        }

        public static string Base64Encode(string plainText) 
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData) 
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject != player) {
                return;
            }
            if(neededToDie.Length != 0) {
                foreach(GameObject go in neededToDie) {
                    if(go != null) {
                        return;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                File.WriteAllText(Application.persistentDataPath + "/level" + (SceneManager.GetActiveScene().buildIndex - 1).ToString() + ".proof", Base64Encode(Application.persistentDataPath + "/level" + (SceneManager.GetActiveScene().buildIndex - i).ToString() + ".proof"));
            }
            if(SceneManager.sceneCount > SceneManager.GetActiveScene().buildIndex) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else {
                SceneManager.LoadScene(1);
            }
        }
    }
}