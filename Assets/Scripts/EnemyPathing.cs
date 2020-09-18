using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0; // определяем с какой точки начинаем движение
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        // определяем начальное положение врагов
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        Move(); // вызываем функцию движения
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {
        // начинаем движение по точкам маршрута
        if (waypointIndex <= waypoints.Count - 1)
        {
            // координаты следующей точки
            var targetPosition = waypoints[waypointIndex].transform.position;
            // определяем скорость движения
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            // определяем куда двигаться дальше и с какой скоростью
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            // проверяем долетел ли корабль до точки,
            // если долетел, то двигаемся к следующей
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            // если точки кончились, то уничтожаем вражеский корабль
            Destroy(gameObject);
        }
    }
}
