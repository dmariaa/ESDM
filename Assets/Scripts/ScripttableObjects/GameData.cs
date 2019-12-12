using System;
using InventorySystem;
using UnityEngine;

namespace ESDM.ScriptableObjects
{
    [Serializable]
    public class GameData
    {
        public string savedDate;
        public string savedName;

        public bool gameStarted = false;
        public bool tutorialPlayed = false;
        public bool keyPicked = false;
        public bool chestOpened = false;
        public bool paperPicked = false;
        public Vector3 lastPosition;
        public Inventory inventory;
        public int[] mauaPetals = new int[6];

        public void Reset()
        {
            Debug.Log("Resetting current game state");
            gameStarted = false;
            tutorialPlayed = false;
            keyPicked = false;
            chestOpened = false;
            paperPicked = false;
            lastPosition = Vector3.zero;
            inventory = new Inventory();
            mauaPetals = new int[6];
        }
    }
}