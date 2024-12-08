using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public abstract class RelativeTemporalAction : TemporalAction
    {
        private float lastPercent;

        protected RelativeTemporalAction(float duration, Interpolation interpolation = null) 
            : base(duration, interpolation)
        {
        }

        protected override void Begin()
        {
            lastPercent = 0;
        }

        protected override void Update(float percent)
        {
            UpdateRelative(percent - lastPercent);
            lastPercent = percent;
        }

        abstract protected void UpdateRelative(float percentDelta);
    }
}
