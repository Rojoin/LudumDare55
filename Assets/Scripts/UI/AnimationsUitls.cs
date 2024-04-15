using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationsUitls
{
    public static void TimerPassTime(ref float timer, ref float prevTime)
    {
        timer += Time.time - prevTime;
        prevTime = Time.time;
    }
}
