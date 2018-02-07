using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map.Decorators
{
    public abstract class MapDecorator
    {
        protected Map decoratedMap;
        protected MapParameters MapParams;
        protected MapRandom rand;

        public MapDecorator(Map map)
        {
            SetMap(map);
        }

        public void SetMap(Map map)
        {
            decoratedMap = map;
            MapParams = map.MapParams;
        }

        public void SetSeed(Int32 seed)
        {
            rand = new MapRandom(seed);
        }

        public abstract bool Generate();
    }
}
