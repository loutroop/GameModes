using Exiled.API.Features;
using Exiled.Events.EventArgs;
using GameCore;
using GameModes;
using MEC;
using Respawning;
using System.Collections.Generic;

namespace GameModes
{
    public class Escape
    {
        private Modes plugin;
        public Escape(Modes plugin) => this.plugin = plugin;
        public void WaitingForPlayers()
        {
            foreach (var player in Player.List)
            {
                player.SendConsoleMessage("当前模式：逃生模式 Current Mode: Escape", "yellow");
            }
        }

        public void RoundStart()
        {
            Timing.RunCoroutine(SetRole());
            Timing.RunCoroutine(offlight());
            Door[] d = UnityEngine.Object.FindObjectsOfType<Door>();
            foreach (Door door in d)
            {
                bool flag = door.DoorName == "914";
                if (flag)
                {
                    door.isOpen = true;
                }
            }
        }

        private IEnumerator<float> SetRole()
        {
            yield return Timing.WaitForSeconds(0.5f);
            foreach (var player in Player.List)
            {
                if (player.Team != Team.SCP)
                {
                    player.SetRole(RoleType.ClassD);
                }
                if (player.Role == RoleType.ClassD)
                {
                    player.Inventory.AddNewItem(ItemType.KeycardJanitor);
                    player.Inventory.AddNewItem(ItemType.Radio);
                    player.Inventory.AddNewItem(ItemType.Flashlight);
                    player.Inventory.AddNewItem(ItemType.Medkit);
                }
                
            }
        }

        private IEnumerator<float> offlight()
        {
            for (; ; )
            {
                Map.TurnOffAllLights(10f, true);
            }
        }
       

    }
}
