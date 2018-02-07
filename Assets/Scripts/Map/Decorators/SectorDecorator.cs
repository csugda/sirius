using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map.Decorators
{
    /// <summary>
    /// Adds sectoring to a map
    /// </summary>
    public class SectorDecorator : MapDecorator
    {
        public int[,] SectorMap;
        public SectorDecorator(Map map) : base(map)
        {
            SectorMap = new int[(int)MapParams.MapBounds.x, (int)MapParams.MapBounds.z];
        }

        public override bool Generate()
        {
            return CreateMapSectors();
        }
        /// <summary>
        /// Split the map into MapParams.MapSectors sub-sectors.
        /// Will only split if all new sub-sectors would remain 
        /// at least a size of MapParams.MinimumRoomSize 
        /// </summary>
        private bool CreateMapSectors()
        {
            Vector3 prevRoomStart = Vector3.zero;
            Vector3 prevRoomSize = Vector3.zero;
            Vector3 currRoomStart = Vector3.zero;
            var mapSize = MapParams.MapBounds;
            int sectorID = 1;
            for (sectorID = 1; sectorID <= MapParams.MapSectors; ++sectorID)
            {
                Vector3 currRoomSize = Vector3.zero;

                var randZ = Math.Max(0, prevRoomStart.z + (1 - rand.GetInt(4)));

                currRoomStart = new Vector3(prevRoomStart.x + prevRoomSize.x, 0, randZ);
                Vector3 currBoundsLeft = mapSize - currRoomStart;

                bool placed = false;
                if (MinimumSectorCanFit(currBoundsLeft))
                {
                    int z = (int)MapParams.MinimumSectorSize.z + 1 + rand.GetInt(4);
                    var startX = rand.GetInt((int)MapParams.MinimumSectorSize.x - 1);
                    int x = startX;
                    placed = false;
                    while (placed == false)
                    {
                        while (SectorMap[x, z] != 0)
                        {
                            ++z;
                            if (z >= mapSize.z) return false;
                        }

                        x = startX;
                        while (x <= MapParams.MinimumSectorSize.x)
                        {
                            ++x;
                            if (SectorMap[x, z] != 0) break;
                        }

                        if (SectorMap[x, z] == 0)
                        {
                            currRoomStart = new Vector3(startX, 0, z);
                            placed = true;
                        }
                    }
                    currBoundsLeft = mapSize - currRoomStart;
                }

                currRoomSize = CreateSector(currBoundsLeft);

                PlaceSector(currRoomStart, currRoomSize, prevRoomStart, prevRoomSize, sectorID);
                prevRoomStart = currRoomStart;
                prevRoomSize = currRoomSize;
            }
            return true;
        }

        private bool MinimumSectorCanFit(Vector3 boundsLeft)
        {
            return (boundsLeft.x >= MapParams.MinimumSectorSize.x &&
                    boundsLeft.z >= MapParams.MinimumSectorSize.z);
        }

        private Vector3 CreateSector(Vector3 boundsLeft)
        {
            Vector3 roomBounds = Vector3.zero;
            Vector3 minSize = MapParams.MinimumSectorSize;
            Vector3 maxSize = MapParams.MaximumSectorSize;

            var randX = Math.Min(rand.GetInt((int)(maxSize.x - minSize.x)), boundsLeft.x - minSize.x);
            var randZ = Math.Min(rand.GetInt((int)(maxSize.z - minSize.z)), boundsLeft.z - minSize.z);

            roomBounds.x = minSize.x + randX;
            roomBounds.z = minSize.z + randZ;

            return roomBounds;
        }

        /// <summary>
        /// Place the actual sector that was randomly bounded previously.
        /// </summary>
        /// <param name="startTile"></param>
        /// <param name="roomSize"></param>
        /// <param name="sectorID"></param>
        /// <returns></returns>
        private bool PlaceSector(Vector3 startTile, Vector3 roomSize, Vector3 prevStart, Vector3 prevRoomSize, int sectorID)
        {
            Vector3 endTile = startTile + roomSize;
            //Vector3 mapSize = MapParams.MapBounds;
            Dictionary<int, List<Vector3>> wallDoorwayDict = new Dictionary<int, List<Vector3>>();

            for (int i = (int)startTile.x; i < endTile.x; ++i)
            {
                for (int j = (int)startTile.z; j < endTile.z; ++j)
                {
                    SectorMap[i, j] = sectorID;
                    var tile = new Vector3(i, 0, j);
                    if (i == startTile.x || i == endTile.x - 1 || j == startTile.z || j == endTile.z - 1)
                    {
                        SetMapTileToType(tile, TileType.Moveable);

                        Vector3 diffTile = GetDiffSectorTile(tile);
                        int diffSector = GetSectorIDfromTile(diffTile);

                        if (diffSector == 0 || diffTile == tile || diffTile == Vector3.positiveInfinity) continue;
                        bool tileCorner = TileIsCorner(tile, startTile, endTile);
                        bool difftileCorner = TileIsCorner(diffTile, prevStart, prevStart + prevRoomSize);
                        if (!tileCorner && !difftileCorner)
                        {
                            if (!wallDoorwayDict.ContainsKey(diffSector))
                            {
                                wallDoorwayDict.Add(diffSector, new List<Vector3>());
                            }
                            wallDoorwayDict[diffSector].Add(tile);
                        }
                    }
                    else SetMapTileToType(tile, TileType.Platform);
                }
            }

            //value is list of valid tiles for that sector
            foreach (var tileSector in wallDoorwayDict.Keys)
            {
                int maxRange = wallDoorwayDict[tileSector].Count();
                Vector3 doorwayTile = wallDoorwayDict[tileSector].ElementAt(rand.GetInt(maxRange));
                bool door1 = SetMapTileToType(doorwayTile, TileType.Doorway);
                bool door2 = SetMapTileToType(GetDiffSectorTile(doorwayTile), TileType.Doorway);

                if (door1 && door2)
                    return true;
                else return false;
            }
            return true;
        }

        private Vector3 GetDiffSectorTile(Vector3 tile)
        {
            Vector3 ForwardTile = tile + Vector3.forward;
            Vector3 BackTile = tile + Vector3.back;
            Vector3 RightTile = tile + Vector3.right;
            Vector3 LeftTile = tile + Vector3.left;

            int currID = GetSectorIDfromTile(tile);
            if (currID != GetSectorIDfromTile(LeftTile) && GetSectorIDfromTile(LeftTile) != 0) return LeftTile;
            if (currID != GetSectorIDfromTile(BackTile) && GetSectorIDfromTile(BackTile) != 0) return BackTile;
            if (currID != GetSectorIDfromTile(ForwardTile) && GetSectorIDfromTile(ForwardTile) != 0) return ForwardTile;
            if (currID != GetSectorIDfromTile(RightTile) && GetSectorIDfromTile(RightTile) != 0) return RightTile;

            return Vector3.positiveInfinity;
        }

        private bool SetMapTileToType(Vector3 tile, TileType type)
        {
            if (IsTileOutOfMapBounds(tile))
            {
                Debug.LogWarning("SetMapTileToType trying to set tile out of bounds!");
                return false;
            }
            this.decoratedMap.TileTypeMap[(int)tile.x, (int)tile.z] = type;
            return true;
        }

        private int GetSectorIDfromTile(Vector3 tile)
        {
            if (IsTileOutOfMapBounds(tile)) return 0;

            return SectorMap[(int)tile.x, (int)tile.z];
        }

        private bool TileIsCorner(Vector3 tile, Vector3 startTile, Vector3 endTile)
        {
            return ((tile.x == startTile.x || tile.x == endTile.x - 1) &&
                    (tile.z == startTile.z || tile.z == endTile.z - 1));
        }

        private bool IsTileOutOfMapBounds(Vector3 tile)
        {
            if (tile.x < 0 || tile.z < 0 || tile.x >= MapParams.MapBounds.x || tile.z >= MapParams.MapBounds.z)
            {
                return true;
            }
            return false;
        }
    }
}
