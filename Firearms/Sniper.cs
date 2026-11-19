namespace SniperItem
{
    using System.Linq;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Item;
    using Exiled.API.Enums;
    using Exiled.API.Structs;
    using InventorySystem.Items.Firearms.Attachments;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.Handlers;
    using Exiled.API.Features.Spawn;
    using Exiled.API.Features.Attributes;
    using Audio;
    using UnityEngine;
    using Exiled.API.Extensions;
    using Exiled.API.Features.DamageHandlers;

    [CustomItem(ItemType.GunE11SR)]
    public class Sniper : CustomWeapon
    {
        private readonly System.Collections.Generic.HashSet<int> HeadshotTargets = new();
        public override string Name { get; set; } = "Sniper";
        public override uint Id { get; set; } = 10;
        public override string Description { get; set; } = "A sniper, all HP if headshot.";
        public SpawnLocationType Location { get; set; } = SpawnLocationType.InsideHidChamber;
        public int Chance { get; set; } = 100;
        public uint Limit { get; set; } = 1;
        public override ItemType Type { get; set; } = ItemType.GunE11SR;
        public override byte ClipSize { get; set; } = 1;
        public override AttachmentName[] Attachments { get; set; } = { AttachmentName.ScopeSight, AttachmentName.Laser, AttachmentName.Flashlight };
        public override float Damage { get; set; } = 75;
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties SpawnProperties { get; set; }
        
        public static void OnRoundStarted()
        {
            
        }

        protected override void OnChangingAttachment(ChangingAttachmentsEventArgs ev)
        {
            ev.IsAllowed = false;
            ev.Player.Broadcast(2,"You cannot change attachments on this weapon.", Broadcast.BroadcastFlags.Normal, true);

        }
        protected override void OnShooting(ShootingEventArgs ev)
        {
            AudioPlayer audioPlayer = AudioPlayer.CreateOrGet($"Player {ev.Player.Nickname}", onIntialCreation: (p) =>
            {        
                p.transform.parent = ev.Player.GameObject.transform;
                Speaker speaker = p.AddSpeaker("Main", isSpatial: true, minDistance: 5f, maxDistance: 15f);
                speaker.transform.parent = ev.Player.GameObject.transform;
                speaker.transform.localPosition = Vector3.zero;
            });
            audioPlayer.AddClip("shot");
            ev.Player.ReloadWeapon();
        }
        protected override void OnHurting(HurtingEventArgs ev)
{
            if (ev.Attacker == null || ev.Player == null)
                return;

            if (HeadshotTargets.Remove(ev.Player.Id))
            {
                ev.Amount = 100f;
            }
        }
        protected override void OnShot(ShotEventArgs ev)
        {
            if (ev.Hitbox != null &&
                ev.Hitbox.HitboxType == HitboxType.Headshot &&
                ev.Target != null)
            {
                HeadshotTargets.Add(ev.Target.Id);
            }
        }

        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            

        }
        private void OnSendingGunSound(SendingGunSoundEventArgs ev)
        {
            ev.IsAllowed = false;
        }
    } }