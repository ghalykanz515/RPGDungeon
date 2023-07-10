using System;
using System.Collections;
using UnityEngine;

namespace RPGDungeon.DungeonGenerator
{
    public abstract class AbstractDungeonGenerator : MonoBehaviour
    {
        [SerializeField] protected TilemapVisualizer tilemapVisualizer;

        [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

        public void GeneratorDungeon() 
        {
            tilemapVisualizer.Clear();
            RunDungeonGenerator();
        }

        protected abstract void RunDungeonGenerator();
    }
}