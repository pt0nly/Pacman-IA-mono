using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pacman_IA.GameObjects;

namespace Pacman_IA.Behaviour
{
    public class HuntBehaviour : IHuntBehaviour
    {
        private Pacman pac = null;

        public HuntBehaviour(Character person)
        {
            if (person is Pacman)
                pac = (Pacman)person;
        }

        public void Hunt(Vector2 lastDirection)
        {
            throw new NotImplementedException();
        }
    }
}
