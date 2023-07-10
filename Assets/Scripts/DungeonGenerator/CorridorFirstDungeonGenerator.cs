﻿using RPGDungeon.DungeonGenerator.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPGDungeon.DungeonGenerator
{
    public class CorridorFirstDungeonGenerator : RandomWalkDungeonGenerator
    {
        [SerializeField] private int corridorLength = 14, corridorCount = 5;

        [Range(0.1f, 1)]
        [SerializeField] private float roomPercent = 0.8f;

        protected override void RunDungeonGenerator()
        {
            CorridorFirstGeneration();
        }

        private void CorridorFirstGeneration()
        {
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

            List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

            //CreateCorridors(floorPositions, potentialRoomPositions);

            HashSet<Vector2Int> roomPostions = CreateRooms(potentialRoomPositions);

            List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

            CreateRoomsAtDeadEnd(deadEnds, roomPostions);

            floorPositions.UnionWith(roomPostions);

            for (int i = 0; i < corridors.Count; i++)
            {
                //corridors[i] = IncreaseCorridorSizeOne(corridors[i]);
                corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
                floorPositions.UnionWith(corridors[i]);
            }

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
        {
            List<Vector2Int> newCorridor = new List<Vector2Int>();
            for (int i = 1; i < corridor.Count; i++)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++) 
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
            }

            return newCorridor;
        }

        private List<Vector2Int> IncreaseCorridorSizeOne(List<Vector2Int> corridor)
        {
            List<Vector2Int> newCorridor = new List<Vector2Int>();
            Vector2Int prevDirection = Vector2Int.zero;

            for (int i = 1; i < corridor.Count; i++)
            {
                Vector2Int directionFromCell = corridor[i] - corridor[i - 1];

                if (prevDirection != Vector2Int.zero && directionFromCell != prevDirection)
                {
                    // handle corner
                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                        }
                    }

                    prevDirection = directionFromCell;
                }
                else 
                {
                    // Add a single cell in the direction + 90 degrees
                    Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                    newCorridor.Add(corridor[i - 1]);
                    newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
                }
            }

            return newCorridor;
        }

        private Vector2Int GetDirection90From(Vector2Int direction)
        {
            if (direction == Vector2Int.up)
                return Vector2Int.right;
            if (direction == Vector2Int.right)
                return Vector2Int.down;
            if (direction == Vector2Int.down)
                return Vector2Int.left;
            if (direction == Vector2Int.left)
                return Vector2Int.up;

            return Vector2Int.zero;
        }

        private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
        {
            foreach (var position in deadEnds)
            {
                if (roomFloors.Contains(position) == false) 
                {
                    var room = RunRandomWalk(randomWalkParameters, position);
                    roomFloors.UnionWith(room);
                }
            }
        }

        private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
        {
            List<Vector2Int> deadEnds = new List<Vector2Int>();

            foreach (var position in floorPositions) 
            {
                int neighboursCount = 0;

                foreach (var direction in Direction2D.cardinalDirectionList)
                {
                    if (floorPositions.Contains(position + direction)) 
                    {
                        neighboursCount++;
                    }
                }

                if (neighboursCount == 1) 
                {
                    deadEnds.Add(position);
                }
            }

            return deadEnds;
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
        {
            HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
            int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

            List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

            foreach (var roomPosition in roomsToCreate)
            {
                var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
                roomPositions.UnionWith(roomFloor);
            }

            return roomPositions;
        }

        private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions) 
        {
            var currentPosition = startPos;
            potentialRoomPositions.Add(currentPosition);
            List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

            for (int i = 0; i < corridorCount; i++)
            {
                //var corridor = DungeonGenerator.RandomWalkCorridor(currentPosition, corridorLength);
                var corridor = DungeonGenerator.RandomWalkCorridor(currentPosition, corridorLength);
                corridors.Add(corridor);
                currentPosition = corridor[corridor.Count - 1];
                potentialRoomPositions.Add(currentPosition);
                floorPositions.UnionWith(corridor);
            }

            return corridors;
        }
    }
}