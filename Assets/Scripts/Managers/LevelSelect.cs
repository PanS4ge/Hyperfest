using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

namespace Hyperfest.Managers
{
    public class LevelSelect : MonoBehaviour
    {
        public int level;
        public TextMeshPro text;
        public GameObject cube;
        public Button button;
        public Material material;
        public static string Base64Decode(string base64EncodedData) 
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        void Start()
        {
            if(!System.IO.File.Exists(Application.persistentDataPath + "/level" + level.ToString() + ".proof") && level != 0) {
                cube.GetComponent<MeshRenderer>().material = material;
                button.interactable = false;
                text.text = "BLOCKED";
                text.color = new Color(0, 0, 0);
            }
            if(System.IO.File.Exists(Application.persistentDataPath + "/level" + level.ToString() + ".proof")){
                if(Base64Decode(System.IO.File.ReadAllText(Application.persistentDataPath + "/level" + level.ToString() + ".proof")) != Application.persistentDataPath + "/level" + level.ToString() + ".proof"){
                    cube.GetComponent<MeshRenderer>().material = material;
                    button.interactable = false;
                    text.text = "BLOCKED";
                    text.color = new Color(0, 0, 0);
                }
            }
        }
    }
}