using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperfest.UI
{
    public class YesCursor : MonoBehaviour
    {
        void Update()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}