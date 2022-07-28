using System.Collections;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class GameInstance
    {


        [Header("Rather Good Intro Video")]
        [Tooltip("Can use global setting if user wants to always skip video intro video.")]
        public bool enableIntroVideo = false;

        [Tooltip("Can also load enable from player prefs integer (\"PLAY_INTRO_VIDEO\" == 1) => play video")]
        public bool loadIntroVideoSettingsFromPlayerPrefs = true;

    }
}