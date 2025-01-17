using System.Collections.Generic;
using Aloha.Events;
using UnityEngine;

namespace Aloha
{
    /// <summary>
    /// Singleton that manage the tiles
    /// </summary>
    public class TilesManager : Singleton<TilesManager>
    {
        [HideInInspector] public bool gameIsStarted;
        private List<GameObject> activeTiles = new List<GameObject>();
        private GameObject[] tilePrefabs;
        public int NumberOfTiles = 20;
        public float TileSpeed = 10;
        public float TileSize = 5;
        public bool Running = false;

        /// <summary>
        /// Is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            GlobalEvent.LevelStart.AddListener(OnLevelStart);
            GlobalEvent.LevelStop.AddListener(Reset);
        }

        /// <summary>
        /// Is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            // Delete a tile and replace it by a new one
            if (Running && activeTiles.Count != 0 && activeTiles[0].transform.position.z + TileSize < 0)
            {
                DeleteFirstTile();
                SpawnTileToQueue(GetNextTileToSpawn());
            }
        }

        /// <summary>
        /// Start the game
        /// <example> Example(s):
        /// <code>
        ///     GlobalEvent.LevelStart.AddListener(OnLevelStart);
        /// </code>
        /// </example>
        /// </summary>
        public void OnLevelStart()
        {

            this.Running = true;

            this.tilePrefabs = SideEnvironmentManager.Instance.GetCurrentBiome().TilePrefabs;

            for (int position = 0; position < NumberOfTiles; position++)
            {
                SpawnTileToQueue(Random.Range(0, tilePrefabs.Length));
            }
        }

        /// <summary>
        /// Return background position based on last tile
        /// </summary>
        public Vector3 getEndTilesPosition()
        {
            return new Vector3(0, 0, TileSize * NumberOfTiles);
        }

        /// <summary>
        /// Create a tile at the end of the tile list
        /// <example> Example(s):
        /// <code>
        ///     SpawnTileToQueue(Random.Range(0, tilePrefabs.Length));
        /// </code>
        /// </example>
        /// </summary>
        public void SpawnTileToQueue(int tileIndex)
        {
            if (activeTiles.Count == 0)
            {
                SpawnTileAt(tileIndex, 0);
                return;
            }
            GameObject tile = Instantiate(tilePrefabs[tileIndex], transform.forward * (activeTiles[activeTiles.Count - 1].transform.position.z + TileSize), transform.rotation);
            ContainerManager.Instance.AddToContainer(ContainerTypes.Tile, tile);
            activeTiles.Add(tile);
            GlobalEvent.TileCount.Invoke(tile);
            GlobalEvent.OnProgressionUpdate.Invoke(EnemySpawner.Instance.TilesCounter - NumberOfTiles, LevelManager.Instance.LevelMapping.TileCount);

            if (EnemySpawner.Instance.TilesCounter - NumberOfTiles >= LevelManager.Instance.LevelMapping.TileCount)
            {
                GlobalEvent.Victory.Invoke();
            }
        }

        /// <summary>
        /// Spawn a tile at a specific position
        /// <example> Example(s):
        /// <code>
        ///     SpawnTileAt(tileIndex, 0);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="tileIndex"></param>
        /// <param name="position"></param>
        public void SpawnTileAt(int tileIndex, int position)
        {
            GameObject tile = Instantiate(tilePrefabs[tileIndex], transform.forward * (TileSize * position), transform.rotation);
            ContainerManager.Instance.AddToContainer(ContainerTypes.Tile, tile);
            activeTiles.Add(tile);
        }

        /// <summary>
        /// Change speed of tiles
        /// <example> Example(s):
        /// <code>
        ///     TilesManager.Instance.ChangeTileSpeed(0);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="tileSpeed"></param>
        public void ChangeTileSpeed(float tileSpeed)
        {
            this.TileSpeed = tileSpeed;
        }

        /// <summary>
        /// Returns a random tile between the list of prefabs
        /// <example> Example(s):
        /// <code>
        ///     SpawnTileToQueue(GetNextTileToSpawn());
        /// </code>
        /// </example>
        /// </summary>
        /// <returns>
        ///     Returns a random tile between the list of prefabs
        /// </returns>
        int GetNextTileToSpawn()
        {
            return Random.Range(0, tilePrefabs.Length);
        }

        /// <summary>
        ///     Delete the first tile of the list
        /// <example> Example(s):
        /// <code>
        ///     DeleteFirstTile();
        /// </code>
        /// </example>
        /// </summary>
        void DeleteFirstTile()
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }

        /// <summary>
        /// Get specific tile with an id
        /// <example> Example(s):
        /// <code>
        ///     GameObject lastTile = tilesManager.GetActiveTileById(tilesManager.NumberOfTiles - 1);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// The choosen tile
        /// </returns>
        public GameObject GetActiveTileById(int index)
        {
            return activeTiles[index];
        }

        /// <summary>
        /// Reset the tiles
        /// </summary>
        public void Reset()
        {
            Running = false;
            activeTiles.Clear();
            ContainerManager.Instance.ClearContainer(ContainerTypes.Tile);
        }

        /// <summary>
        /// Is called when a Scene or game ends.
        /// </summary>
        void OnDestroy()
        {
            ContainerManager.Instance.ClearContainer(ContainerTypes.Tile);
            GlobalEvent.LevelStart.RemoveListener(OnLevelStart);
            GlobalEvent.LevelStop.RemoveListener(Reset);
        }
    }
}
