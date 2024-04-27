using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    //Declaracion de los eventos del juego
    public static Action OnRiflePicked;

    public static Action<int> OnModifyHealth;

    public static Action<GameObject, float> OnEnemyHitted;
}
