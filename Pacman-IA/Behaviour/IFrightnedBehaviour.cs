using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_IA.Behaviour
{
    interface IFrightnedBehaviour
    {
        Dictionary<string, int> Wander(Vector2 lastDirection);
    }
}
