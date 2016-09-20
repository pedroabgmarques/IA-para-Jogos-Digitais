﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{

    /// <summary>
    /// Para permitir a junção dos vários comportamentos calculados,cada algoritmo irá indicar como
    /// o personagem deve alterar o seu movimento de modo a atingir o comportamento desejado. Essa 
    /// informação será dada por base em dois valores de aceleração: um linear, que indica a direção
    /// e velocidade a que o personagem se deve movimentar; e um angular, que indica a direção e 
    /// velocidade a que o personagem deve rodar sobre si mesmo
    /// </summary>
    public class Steering
    {
        Vector3 linear; //linear steering
        float angular; //angular steering
    }
}
