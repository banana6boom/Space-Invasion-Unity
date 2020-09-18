using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 10f; // скорость космического корабля
    [SerializeField] float padding = 0.5f; // отступ от границ экрана
    [SerializeField] GameObject laserPrefab; // переменная для хранения префаба лазера
    [SerializeField] float laserSpeed = 10f; // скорость движения (полета) лазера
    [SerializeField] float laserFiringPeriod = 0.1f; // задержка между выстрелами
    Coroutine firingCoroutine; 

    float xMin; // минимальное значение по горизионтальной оси (X)
    float xMax; // максимальное значение по горизионтальной оси (X)
    float yMin; // минимальное значение по вертикальной оси (Y)
    float yMax; // максимальное значение по вертикальной оси (Y)

    // функция Start() запускается один раз при запуске игры
    void Start()
    {
        SetUpMoveBoundaries(); // вызов функции для определения границ передвижения космического корабля
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main; // переменная для хранения объекта камеры
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // функция Update() вызывается каждый игровой кадр
    void Update()
    {
        Move(); // вызов функции движения
        Fire(); // вызов функции выстрела
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed; // определяем смещение корабля по горизонтали
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed; // определяем смещение корабля по вертикали

        // Input.GetAxis("Horizontal") - отслеживание нажатий от игрока по горизонтали (стрелка влево / стрелка вправо)
        // Input.GetAxis("Vertical") - отслеждивание нажатий от игрока по вертикали (стрелка вверх / стрелка вниз)
        // Time.deltaTime - время в секундах с момента последнего кадра

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); // задаем кораблю новое положение по горизонтали
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax); // задаем кораблю новое полодение по вертикали

        transform.position = new Vector2(newXPos, newYPos); // передаем кораблю новые координаты
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) // проверяем нажата ли левая кнопка мыши
        {
            firingCoroutine = StartCoroutine(FireContinuously()); // запускаем функцию выстрела
        }
        if (Input.GetButtonUp("Fire1")) // проверяем отжата ли левая кнопка мыши
        {
            StopCoroutine(firingCoroutine); // останавливаем выстрел
        }
    }

    IEnumerator FireContinuously()
    {
        while (true) // запускаем бесконечный цикл, пока нажата кнопка выстрела
        {
            // создаем лазер
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            // задаем скорость лазеру
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            // задаем задержку между выстрелами
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }
}
