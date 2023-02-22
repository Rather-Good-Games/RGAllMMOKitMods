namespace MultiplayerARPG.GameData.Model.Playables
{
    /// <summary>
    /// Originally named "PlayableCharacterModel_Custom" based on Callepo Synty character creator compatability. 
    /// </summary>
    public partial class PlayableCharacterModel_Custom : PlayableCharacterModel
    {

        //Dummy class to place component on char
        protected override void Awake()
        {
            base.Awake();
            this.InvokeInstanceDevExtMethods("RG_Awake");
        }
    }
}
