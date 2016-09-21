using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{

    public abstract class Behavior
    {
        protected abstract Steering getSteering();
    }
}
