using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// добавляем новый пункт в меню создания объектов
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    // конфигурация волны
    [SerializeField] GameObject enemyPrefab; // определяем какой префаб корабля использовать
    [SerializeField] GameObject pathPrefab; // опредеояем какой path префаб использовать
    [SerializeField] float timeBetweenSpawns = 1f; // время между спаун врагов
    [SerializeField] float spawnRandomFactor = 0.3f; // значение для определения случайной задержки
    [SerializeField] int numberOfEnemies = 5; // количество врагов в волне
    [SerializeField] float moveSpeed = 2f; // скорость движения врагов в волне


    // функции для доступа к характеристикам (переменным) волны
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public GameObject GetPathPrefab() { return pathPrefab; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
}
