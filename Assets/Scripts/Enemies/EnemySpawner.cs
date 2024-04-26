using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cyborg.Enemies
{
    public enum EnemyType
    {
        Scout,
        Soldier,
        Tank
    }

    public class EnemySpawner : MonoBehaviour
    {
        const int MAX_ENEMY_COUNT = 15;
        private int _dynamicallySpawnedEnemyCount;
        private Dictionary<EnemyType, float> _spawnChanceMap;
        private PlayerMovement _player;

        [SerializeField] private Enemy _scout;
        [SerializeField] private Enemy _soldier;
        [SerializeField] private Enemy _tank;

        private void Awake()
        {
            _spawnChanceMap = new Dictionary<EnemyType, float>
            {
                { EnemyType.Tank, 0.05f },
                { EnemyType.Scout, 0.30f },
                { EnemyType.Soldier, 0.65f },
            };
            _player = FindObjectOfType<PlayerMovement>();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemies());

        }

        public void CreateEnemy(EnemyType enemyType)
        {
            Enemy enemy = enemyType switch
            {
                EnemyType.Scout => Instantiate(_scout),
                EnemyType.Soldier => Instantiate(_soldier),
                EnemyType.Tank => Instantiate(_tank),
                _ => Instantiate(_scout)
            };

            var randomDistance = Random.Range(15, 20);
            var randomPosition = Random.insideUnitSphere.normalized * randomDistance;
            var spawnPosition = _player.transform.position + new Vector3(randomPosition.x, randomPosition.y, 0);

            enemy.Agent.Warp(spawnPosition);
            enemy.EnemyDied += OnEnemyDied;
            _spawnChanceMap = _spawnChanceMap.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            _dynamicallySpawnedEnemyCount++;
        }

        private IEnumerator SpawnEnemies()
        {
            float timeToWait;
            while (true)
            {
                if (_dynamicallySpawnedEnemyCount < MAX_ENEMY_COUNT)
                {
                    CreateEnemy(GetRandomEnemyType());
                    timeToWait = Random.Range(1, 4);
                    yield return new WaitForSeconds(timeToWait);
                }
                yield return null;
            }
        }

        private EnemyType GetRandomEnemyType()
        {
            float randomValue = Random.Range(0, 1f);
            float currentChance = 0;

            foreach (var kvp in _spawnChanceMap)
            {
                currentChance += kvp.Value;
                if (randomValue <= currentChance)
                {
                    return kvp.Key;
                }
            }
            return EnemyType.Scout;
        }

        private void OnEnemyDied(EnemyType enemyType)
        {
            _dynamicallySpawnedEnemyCount--;
        }
    }
}