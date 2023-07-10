using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGDungeon.DungeonGenerator.Data 
{
    [CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "PCG/RandomWalkData")]
    public class RandomWalkSO : ScriptableObject
    {
        public int iterations = 10, walkLength = 10;
        public bool startRandomEachIteration = true;
    }
}
