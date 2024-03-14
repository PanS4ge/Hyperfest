using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hyperfest.Managers
{
    public class KeyManager : MonoBehaviour
    {
        public enum InputType
        {
            KeyBoard,
            Gamepad
        }
        [System.Serializable]
        public class StringKey {
            public string key;
            public KeyInput input;
        }

        [System.Serializable]
        public class TexturedKeyCode
        {
            public KeyCode keyCode;
            public Texture2D texture2D;
        }

        [System.Serializable]
        public class KeyInput
        {
            public KeyManager keyManager;
            public TexturedKeyCode keyboard;
            public TexturedKeyCode gamepad;
            public TexturedKeyCode getCurrent() {
                if(keyManager.inputType == InputType.KeyBoard) {
                    return keyboard;
                } else {
                    return gamepad;
                }
            }
            public RawImage toChange;
        }

        public InputType inputType = InputType.KeyBoard;

        void Start()
        {
            // Set initial input type
            UpdateInputType(inputType);
        }

        void Update()
        {
            // Check for input type change
            if (Input.anyKeyDown)
            {
                UpdateInputType(InputType.KeyBoard);
            }
            else if (Input.GetJoystickNames().Length > 0)
            {
                UpdateInputType(InputType.Gamepad);
            }
        }

        public void UpdateInputType(InputType newInputType)
        {
            inputType = newInputType;
            // Update texture based on input type
            foreach(StringKey sk in keyInputs) {
                if(sk.input.toChange == null) continue;
                sk.input.toChange.texture = (inputType == InputType.KeyBoard) ? sk.input.keyboard.texture2D : sk.input.gamepad.texture2D;
            }
        }

        public List<StringKey> keyInputs = new List<StringKey>();
        public KeyInput findInputYouWant(string key) {
            foreach(StringKey sk in keyInputs) {
                if(sk.key == key) {
                    return sk.input;
                }
            }
            return null;
        }
    }
}