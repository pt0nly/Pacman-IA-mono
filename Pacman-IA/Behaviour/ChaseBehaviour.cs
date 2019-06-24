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
    public abstract class ChaseBehaviour : IChaseBehaviour
    {
        protected Character ghost = null;

        public ChaseBehaviour(Character person)
        {
            ghost = person;
        }

        public virtual Dictionary<string, int> Chase(Vector2 lastDirection)
        {
            Dictionary<string, int> dirWeights = new Dictionary<string, int>();
            dirWeights["left"] = checkChase(GameVars.DIR.LEFT.ToPoint());
            dirWeights["right"] = checkChase(GameVars.DIR.RIGHT.ToPoint());
            dirWeights["down"] = checkChase(GameVars.DIR.DOWN.ToPoint());
            dirWeights["up"] = checkChase(GameVars.DIR.UP.ToPoint());

            return dirWeights;
        }

        protected virtual int checkChase(Point direction)
        {
            int retval = 0;

            return retval;
        }
    }
}
