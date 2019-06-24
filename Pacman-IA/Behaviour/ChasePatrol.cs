using Microsoft.Xna.Framework;
using Pacman_IA.Classes;
using Pacman_IA.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_IA.Behaviour
{
    public class ChasePatrol : ChaseBehaviour
    {
        private Vector2 pacmanLastPosition;
        private bool pacmanLocated;

        public ChasePatrol(Character person) : base(person)
        {
            pacmanLastPosition = GameVars.DIR.INVALID;
            pacmanLocated = false;
        }

        protected override int checkChase(Point direction)
        {
            return base.checkChase(direction);
        }
    }
}
