using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLib.Haptics;

public class HapticManager : GameLib.Haptics.HapticsManager
{
    [SerializeField] HapticsPreset presetLow;
    [SerializeField] HapticsPreset presetMed;
    [SerializeField] HapticsPreset presetHi;

    private static HapticsPreset presetLowStatic;
    private static HapticsPreset presetMedStatic;
    private static HapticsPreset presetHiStatic;

    private void Awake()
    {
        presetLowStatic = presetLow;
        presetMedStatic = presetMed;
        presetHiStatic = presetHi;
    }

    void OnEnable()
    {
        /*   Level.onPlay += VibMed;
           Level.onFinished += VibMed;*/
        //Level.onItemsMatch += VibHi;
        /*   Item.onHide += VibLo;
           Item.onHit += VibLo;*/
    }

    void OnDisable()
    {
        /*  Level.onPlay -= VibMed;
          Level.onFinished -= VibMed;*/
        //Level.onItemsMatch -= VibHi;
        /* Item.onHide -= VibLo;
         Item.onHit -= VibLo;*/
    }

    void VibLo0() => VibLo(null);
    void VibMed0() => VibMed(null);
    void VibHi0() => VibHi(null);
    void VibLo2(object obj0, object obj1) => VibLo(null);
    void VibMed2(object obj0, object obj1) => VibMed(null);
    void VibHi2(object obj0, object obj1) => VibHi(null);

    public static void VibLo(object sender)
    {
        HapticsSystem.Vibrate(presetLowStatic);
    }

    public static void VibMed(object sender)
    {
        HapticsSystem.Vibrate(presetMedStatic);
    }

    public static void VibHi(object sender)
    {
        HapticsSystem.Vibrate(presetHiStatic);
    }
}