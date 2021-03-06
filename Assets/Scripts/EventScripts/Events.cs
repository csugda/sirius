﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class HitEnemyEvent : UnityEvent<float> { }
[System.Serializable]
public class HitPlayerEvent : UnityEvent<float> { }
[System.Serializable]
public class PlayerExperienceEvent : UnityEvent<int> { }
public class Events : MonoBehaviour {
    public static HitEnemyEvent HitEnemy = new HitEnemyEvent();
    public static HitPlayerEvent HitPlayer = new HitPlayerEvent();
    public static PlayerExperienceEvent XPDrop = new PlayerExperienceEvent();
}
 