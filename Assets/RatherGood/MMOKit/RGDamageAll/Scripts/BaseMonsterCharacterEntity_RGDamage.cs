using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Serialization;
using LiteNetLibManager;
using LiteNetLib;
using Cysharp.Threading.Tasks;

namespace MultiplayerARPG
{
    public abstract partial class BaseMonsterCharacterEntity
    {

        public override bool CanReceiveDamageFrom(EntityInfo instigator)
        {

            if (!CurrentGameInstance.allowDamageToSelfAndAlliesFromAllSources)
            {
                return base.CanReceiveDamageFrom(instigator);
            }

            if (IsImmune)
                return false;

            if (string.IsNullOrEmpty(instigator.Id))
                return true;

            if (instigator.IsInSafeArea)
                return false;

            return true;

        }



    }
}