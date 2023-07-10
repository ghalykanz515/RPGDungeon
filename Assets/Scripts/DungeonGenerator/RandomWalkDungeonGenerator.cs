using RPGDungeon.DungeonGenerator.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPGDungeon.DungeonGenerator
{
    public class RandomWalkDungeonGenerator : AbstractDungeonGenerator
    {
        [SerializeField] protected RandomWalkSO randomWalkParameters;

        protected override void RunDungeonGenerator() 
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPos);
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        protected HashSet<Vector2Int> RunRandomWalk(RandomWalkSO parameters, Vector2Int position)
        {
            var currentPos = position;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < parameters.iterations; i++)
            {
                var path = DungeonGenerator.SimpleRandomWalk(currentPos, parameters.walkLength);
                floorPositions.UnionWith(path);
                if (parameters.startRandomEachIteration)
                    currentPos = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
            }

            return floorPositions;
        }
    }
}