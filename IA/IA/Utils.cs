/*
 * Utilidades para a IA de cálculo de movimentos
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{

    /// <summary>
    /// Classe estática que disponibiliza métodos utilitários
    /// </summary>
    public static class Utils
    {

        public static Vector3 orientationToVector(float orientation)
        {
            return new Vector3((float)Math.Sin(orientation), 0f, (float)Math.Cos(orientation));
        }

    }
}
