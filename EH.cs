using Exiled.Events.Handlers;
using Exiled.API.Interfaces;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using SniperItem;
using Exiled.CustomItems.API;
using Audio;

namespace EH
{
    public class EH : Plugin<global::Config.Config>
    {
        public override void OnEnabled()
        {
           AudioClipStorage.LoadClip("C:\\Users\\jorda\\Downloads\\snipershoot.ogg", "shot");
           AudioClipStorage.LoadClip("C:\\Users\\jorda\\Downloads\\snipereload.ogg", "reload");
           new Sniper().Register();
        }

        public override void OnDisabled()
        {
        }

        private void RegisterEvents()
        {
        }


        private void UnregisterEvents()
        {
        }
    }
}