using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using MEC;

using ServerEvents = Exiled.Events.Handlers.Server;
using MapEvents = Exiled.Events.Handlers.Map;
using WarheadEvents = Exiled.Events.Handlers.Warhead;
using PlayerEvents = Exiled.Events.Handlers.Player;
using Scp049Events = Exiled.Events.Handlers.Scp049;
using Scp079Events = Exiled.Events.Handlers.Scp079;
using Scp106Events = Exiled.Events.Handlers.Scp106;
using Scp914Events = Exiled.Events.Handlers.Scp914;

namespace GameModes
{
    public class Modes : Plugin<Configs>
    {
        private DieHard diehard;
        private Escape escape;
        public override string Name => nameof(GameModes);
        public override string Author => "Loutroop";
        public override void OnEnabled()
        {
            base.OnEnabled();
            if (!Config.IsEnabled) return;
            ReloadDieHardMode();
            ReloadEspaceMode();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            DisabledDieHardMode();
            DisabledEscapeMode();
        }

        public override void OnReloaded()
        {
            base.OnReloaded();
            Log.Debug("是的，它已经加载完成了");
        }

        private void ReloadDieHardMode()
        {
            if (!Config.DiehardModeEnabled) return;
            ServerEvents.WaitingForPlayers += diehard.WaitingForPlayers;
            ServerEvents.RoundStarted += diehard.RoundStart;
            PlayerEvents.Joined += diehard.OnPlayerJoin;
        }

        private void ReloadEspaceMode()
        {
            if (!Config.EscapeModeEnabled) return;
            ServerEvents.WaitingForPlayers += escape.WaitingForPlayers;
            ServerEvents.RoundStarted += escape.RoundStart;
        }

        private void DisabledDieHardMode()
        {
            ServerEvents.WaitingForPlayers -= diehard.WaitingForPlayers;
            ServerEvents.RoundStarted -= diehard.RoundStart;
            PlayerEvents.Joined -= diehard.OnPlayerJoin;
        }

        private void DisabledEscapeMode()
        {
            ServerEvents.WaitingForPlayers -= escape.WaitingForPlayers;
            ServerEvents.RoundStarted -= escape.RoundStart;
        }

    }
}

