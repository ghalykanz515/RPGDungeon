using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGDungeon.DungeonGenerator 
{
    public static class WallGenerator
    {
        public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
        {
            var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
            var cornerWAllPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionList);

            CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
            CreateCornerWalls(tilemapVisualizer, cornerWAllPositions, floorPositions);
        }

        private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
        {
            foreach (var position in cornerWallPositions)
            {
                string neighboursBinaryType = "";
                foreach (var direction in Direction2D.eightDirectionList) 
                {
                    var neighbourPosition = position + direction;
                    if (floorPositions.Contains(neighbourPosition))
                    {
                        neighboursBinaryType += "1";
                    }
                    else
                    {
                        neighboursBinaryType += "0";
                    }
                }
                tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
            }
        }

        private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
        {
            foreach (var position in basicWallPositions)
            {
                string neighboursBinaryType = "";
                foreach (var direction in Direction2D.cardinalDirectionList)
                {
                    var neighbourPosition = position + direction;
                    if (floorPositions.Contains(neighbourPosition))
                    {
                        neighboursBinaryType += "1";
                    }
                    else 
                    {
                        neighboursBinaryType += "0";
                    }
                }
                tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
            }
        }

        private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
        {
            HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

            foreach (var position in floorPositions) 
            {
                foreach (var direction in directionList) 
                {
                    var neighboourPosition = position + direction;
                    if (floorPositions.Contains(neighboourPosition) == false) 
                    {
                        wallPositions.Add(neighboourPosition);
                    }
                }
            }

            return wallPositions;
        }
    }
}
