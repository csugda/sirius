using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map.Decorators
{
    /// <summary>
    /// Adds a randomized pathway from endpoint to another endpoint
    /// </summary>
    public class PathwayDecorator : MapDecorator
    {
        /// <summary>
        /// Percent chance that the path will vary its direction from its current direction.
        /// Higher values will make more chaotic paths. 
        /// 0 will make the shortest path between points.
        /// </summary>
        public int PathVariancePercent = 25;

        public Vector3 StartTile;
        public Vector3 EndTile;

        public PathwayDecorator(Map map) : base(map) { }

        public override bool Generate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Randomly chooses the next direction based on the variance parameter/s.
        /// Defaults to 75% chance to choose shortest path.
        /// </summary>
        /// <returns></returns>
        private Vector3 GetNextTileDirection()
        {
            return Vector3.zero;
        }

        /// <summary>
        /// Checks if it is possible to get from the start to the end (TODO: Given an AI to test parameters?)
        /// </summary>
        /// <returns></returns>
        private bool IsPathPossible()
        {
            return false;
        }
    }
}
