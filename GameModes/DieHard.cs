using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameModes
{
    public class DieHard
    {
        private Modes plugin;
        public DieHard(Modes plugin) => this.plugin = plugin;
        private int seconds;
        private int ntf;
        private int chaos;
        private bool spawnchoice;
        public void WaitingForPlayers()
        {
            foreach (var player in Player.List)
            {
                player.SendConsoleMessage("当前模式：死斗模式 Current Mode: Die hard mode", "yellow");
            }

        }

        public void RoundStart()
        {
            Timing.RunCoroutine(Spawning());
            Door[] d = UnityEngine.Object.FindObjectsOfType<Door>();
            foreach (Door door in d)
            {
                bool flag = door.DoorName == "SURFACE_GATE";
                if (flag)
                {
                    door.lockdown = true;
                    Timing.WaitForSeconds(30f);
                }
                bool flag2 = door.DoorName == "GATE_A";
                if (flag2)
                {
                    door.lockdown = true;
                }

            }
            seconds = 30;
            Map.Broadcast(30, $"距离大门解锁时间剩余{seconds--}");
            
            
        }

        private IEnumerator<float> Spawning()
        {
            yield return Timing.WaitForSeconds(0.5f);
            foreach (var player in Player.List)
            {
                if (player.Role == RoleType.Spectator || player.Role == RoleType.ClassD || player.Role == RoleType.FacilityGuard || player.Role == RoleType.NtfScientist || player.Role == RoleType.Scientist || player.Team == Team.SCP)
                {
                    RoleType randomType = UnityEngine.Random.Range(0, 2) == 1 ? RoleType.ChaosInsurgency : RoleType.NtfLieutenant;
                    player.SetRole(randomType);
                }
                bool flag = player.Role == RoleType.ChaosInsurgency;
                if (flag)
                {
                    chaos++;
                    this.spawnchoice = true;
                }
                bool flag2 = player.Role == RoleType.NtfLieutenant;
                if (flag2)
                {
                    ntf++;
                    this.spawnchoice = false;
                }
            }
            
            foreach (var player in Player.List)
            {
                player.RemoveItem();
                player.Inventory.AddNewItem(ItemType.GunProject90);
                player.Inventory.AddNewItem(ItemType.GunUSP);
                player.Inventory.AddNewItem(ItemType.GunMP7);
                player.Inventory.AddNewItem(ItemType.GunLogicer);
                player.Inventory.AddNewItem(ItemType.GunE11SR);
                player.Inventory.AddNewItem(ItemType.GunCOM15);
                player.SetAmmo(Exiled.API.Enums.AmmoType.Nato556, 10000);
                player.SetAmmo(Exiled.API.Enums.AmmoType.Nato762, 10000);
                player.SetAmmo(Exiled.API.Enums.AmmoType.Nato9, 10000);
            }
            Map.Broadcast(1, $"九尾狐人数：" + this.ntf.ToString() + "混沌人数" + this.chaos.ToString());
            yield break;
        }

        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (this.spawnchoice)
            {
                ev.Player.SetRole(RoleType.NtfLieutenant);
            }
            else if (!this.spawnchoice)
            {
                ev.Player.SetRole(RoleType.ChaosInsurgency);
            }
        }

        private string[] unbreakableDoorNames = new string[]
        {
            "012",
            "012_BOTTOM",
            "049_ARMORY",
            "079_FIRST",
            "079_SECOND",
            "096",
            "106_BOTTOM",
            "106_PRIMARY",
            "106_SECONDARY",
            "173",
            "173_ARMORY",
            "372",
            "914",
            "CHECKPOINT_ENT",
            "CHECKPOINT_LCZ_A",
            "CHECKPOINT_LCZ_B",
            "ESCAPE",
            "ESCAPE_INNER",
            "GATE_A",
            "GATE_B",
            "HCZ_ARMORY",
            "HID",
            "HID_LEFT",
            "HID_RIGHT",
            "INTERCOM",
            "LCZ_ARMORY",
            "NUKE_ARMORY",
            "NUKE_SURFACE",
            "SURFACE_GATE",
        };

    }
}
