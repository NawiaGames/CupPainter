using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action<int> OnLevelStart;
    public static Action<int> OnLevelSuccessed;
    public static Action<int> OnLevelFail;
    public static Action<int> OnLevelRestert;
}
