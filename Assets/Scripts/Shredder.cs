using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    // проверяем столкновения с объектом Shredder
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision - передается, когда другой обьект входит в триггер присоединенный к обьекту Shredder
        Destroy(collision.gameObject); // уничтожаем игровые объекты, которые столкнулись с Shredder
    }
}
