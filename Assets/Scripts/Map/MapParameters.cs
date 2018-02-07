using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map
{
    [Serializable]
    public class MapParameters
    {
        /// <summary>
        /// Total size of map to be split into sectors/halls
        /// </summary>
        public Vector3 MapBounds;

        public bool GenerateRandomMap;
        public int Seed;

        public int MapSectors;
        public Vector3 MinimumSectorSize;
        public Vector3 MaximumSectorSize;

        /// <summary>
        /// The minimum area created as walkable/moveable area when creating hallways
        /// or paths through blocks
        /// </summary>
        public Vector3 MinimumMovingAreaSize;

        /// <summary>
        /// Determines the map or sub-map's entrances to ADD to this current map.
        /// on default map (to be further split) this will only add the number and then
        /// delegate further to sub-maps/sector generators
        /// </summary>
        public int NumberNorthEntrances;
        public int NumberEastEntrances;
        public int NumberSouthEntrances;
        public int NumberWestEntrances;

        //TODO: THREADING/SUBMAP?
    }
}


