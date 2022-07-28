using LiteNetLibManager;
using MultiplayerARPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{

    /// <summary>
    /// Place this on something you want the player to look at and will display info about. 
    /// </summary>
    public class InteractableEntityRG : BaseGameEntity
    {
        [Category("RGStuff")]
        [TextArea(5,10)]
        [SerializeField] string infoToShow;


        [SerializeField] bool _updateEntityComponents = false;
        protected override bool UpdateEntityComponents { get { return _updateEntityComponents; } }
        protected override void EntityAwake()
        {
            base.EntityAwake();
            gameObject.tag = GameInstance.Singleton.itemDropTag;
            gameObject.layer = GameInstance.Singleton.itemDropLayer;

            Title = infoToShow;
        }


    }


}