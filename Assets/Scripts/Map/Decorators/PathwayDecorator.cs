using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map.Decorators
{
    /// <summary>
    /// Adds a randomized pathway from endpoint to another endpoint
    /// </summary>
    public class PathwayDecorator : MapDecorator
    {
        public PathwayDecorator(Map map) : base(map)
        {
        }

        public override bool Generate()
        {
            throw new NotImplementedException();
        }
    }
}
