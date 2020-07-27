using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModes
{
    public class Configs : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool EscapeModeEnabled { get; set; } = false;
        public bool DiehardModeEnabled { get; set; } = false;
    }
}
