using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hang.Models
{
    public class Player
    {
        public string Name { get; }
        public PlayerType Type { get; }

        public Player(string name, PlayerType type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}