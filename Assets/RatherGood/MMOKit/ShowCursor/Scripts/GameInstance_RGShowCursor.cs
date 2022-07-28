

using UnityEngine;

namespace MultiplayerARPG
{

    public partial class GameInstance
    {

        [Header("Rather Good Toggle Cursor Button")]
        public bool enableToggleCursorOnButtonPress;

        [Tooltip("Add this button name and key to GameInstance InputSettingsManager.")]
        public string toggleCursorButtonName = "ToggleCursorModButton";

    }
}