using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{

    /// <summary>
    /// Informação necessária para representar inequivocamente um personagem no espaço 2 e 1/2D.
    /// </summary>
    public struct MovementInfo
    {
        public Vector3 position; //current character position
        public Vector3 velocity; //current character velocity and direction
        public float orientation; //current character orientation
        public float rotation; //current character rotation speed
    }
}
