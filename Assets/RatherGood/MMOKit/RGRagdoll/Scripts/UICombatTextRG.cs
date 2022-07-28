using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if USE_TEXT_MESH_PRO
using TMPro;
#endif


namespace MultiplayerARPG
{

    public class UICombatTextRG : UICombatText
    {

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                textComponent.text = text;
            }
        }

    }
}
