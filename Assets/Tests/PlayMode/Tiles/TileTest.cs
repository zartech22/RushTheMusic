using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections.Generic;

//TODO: explain your FUNCKING TEST (like youyou in Tests/PlayMode/Enemy/ActionZoneTest)

namespace Aloha.Test
{
    /// <summary>
    /// TODO
    /// </summary>
    public class TileTest
    {
        /// <summary>
        /// This Test checks if tiles are moving on Z- axis
        /// </summary>
        [UnityTest]
        public IEnumerator TileMoveForward()
        {
            GameObject tile = new GameObject();
            tile.AddComponent<BasicTile>();
            float initialZPos = tile.transform.position.z;
            yield return null;

            Assert.Less(tile.transform.position.z, initialZPos, "Does the tile move towards the player ?");

            Aloha.Utils.ClearCurrentScene();
            yield return null;
        }

        /// <summary>
        /// This test checks that a new tile appears and the first one is destroy when tiles move
        /// </summary>
        [UnityTest]
        public IEnumerator TileAutomaticallyAppearsAndDestroyed()
        {
            GameObject manager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            TilesManager tilesManager = TilesManager.Instance;
            LevelManager levelManager = LevelManager.Instance;
            levelManager.LevelMapping = new LevelMapping(new SerializeDictionary<int, List<EnemyMapping>>(), 50);
            yield return null;

            tilesManager.StartGame();
            yield return null;

            GameObject firstTile = tilesManager.GetActiveTileById(0);
            GameObject lastTile = tilesManager.GetActiveTileById(tilesManager.NumberOfTiles - 1);

            float timeOfOneTile = (tilesManager.TileSize / tilesManager.TileSpeed);
            yield return new WaitForSeconds(timeOfOneTile * 2);

            Assert.IsTrue(firstTile == null, "Is the first tile deleted when it's behind the player ?");
            Assert.AreNotSame(lastTile, tilesManager.GetActiveTileById(tilesManager.NumberOfTiles - 1), "Does a new tile appear ?");

            tilesManager.StopGame();
            yield return null;

            Aloha.Utils.ClearCurrentScene();
            yield return null;
        }

        /// <summary>
        /// This Test checks if the game is correctly instanced and destroyed when it's start and stop
        /// </summary>
        [UnityTest]
        public IEnumerator GameStartAndStop()
        {
            GameObject manager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            TilesManager tilesManager = TilesManager.Instance;
            LevelManager levelManager = LevelManager.Instance;
            levelManager.LevelMapping = new LevelMapping();
            yield return null;

            tilesManager.StartGame();
            yield return null;
            Assert.IsTrue(tilesManager.GameIsStarted, "Is the game started ?");

            tilesManager.StartGame();
            yield return null;
            Assert.IsTrue(tilesManager.GameIsStarted, "Is the game started ?");

            tilesManager.StopGame();
            yield return null;
            Assert.IsFalse(tilesManager.GameIsStarted, "Is the game stopped ?");

            tilesManager.StopGame();
            yield return null;
            Assert.IsFalse(tilesManager.GameIsStarted, "Is the game stopped ?");

            Aloha.Utils.ClearCurrentScene();
            yield return null;
        }
    }
}