using System;
using UnityEngine;

namespace ESDM.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ESDM/GameData", order = 0)]
    public class Game : ScriptableObject
    {
        public bool tutorialPlayed = false;
        public string savedDate;
        public string savedName;
    }
}