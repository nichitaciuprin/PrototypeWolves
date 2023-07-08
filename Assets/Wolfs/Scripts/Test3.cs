using System;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public GameObject target;
    public float targetSpeed;
    public float velocity;
    public float acc;
    private void FixedUpdate()
    {
        var deltaTime = Time.fixedDeltaTime;
        // deltaTime = 1f;

        var position = transform.position;
        Catchup(ref position.x, ref velocity, acc, target.transform.position.x, targetSpeed, deltaTime);
        transform.position = position;

        target.transform.position = target.transform.position + (new Vector3(targetSpeed,0,0))*deltaTime;
    }
    private static void Catchup(ref float Apos, ref float Avel, float acc, float Bpos, float Bvel, float deltaTime)
    {
        var catchupInfo = CatchupInfo(Apos,Avel,acc,Bpos,Bvel);

        if (catchupInfo.Item2 == 0f && catchupInfo.Item3 == 0f) return;

        {
        var direction = catchupInfo.Item1;
        var time = catchupInfo.Item2;
        time = Math.Clamp(deltaTime,0,time);
        deltaTime -= time;
        var cachedValue = direction*acc*time;
        Apos += Avel*time;
        Apos += cachedValue*time/2;
        Avel += cachedValue;
        if (deltaTime == 0) return;
        }

        {
        var direction = -catchupInfo.Item1;
        var time = catchupInfo.Item3;
        time = Math.Clamp(deltaTime,0,time);
        deltaTime -= time;
        var cachedValue = direction*acc*time;
        Apos += Avel*time;
        Apos += cachedValue*time/2;
        Avel += cachedValue;
        if (deltaTime == 0) return;
        }

        Apos += Avel*deltaTime;
    }
    private static (int,float,float) CatchupInfo(float Apos, float Avel, float acc, float Bpos, float Bvel)
    {
        var pos = Apos - Bpos;
        var vel = Avel - Bvel;
        return CatchupInfo(pos,vel,acc);
    }
    private static (int,float,float) CatchupInfo(float pos, float vel, float acc)
    {
        if (acc == 0f) return (1,0,0);
        if (pos == 0 && vel == 0) return (1,0,0);
        var velAbs = MathF.Abs(vel);
        var haltTime = velAbs/acc;
        var haltOffset = vel*haltTime;
        var haltOffsetHalf = haltOffset/2;
        var pos2 = pos+haltOffsetHalf;
        if (pos2 == 0f) return (-MathF.Sign(vel),haltTime,0);
        var isOvershoot = MathF.Sign(pos) != MathF.Sign(pos2);
        var s = isOvershoot ? 1 : -1;
        var val2 = -pos-haltOffsetHalf*s;
        var t2 = MathF.Sqrt(MathF.Abs(val2)/acc);
        var t1 = t2+haltTime*s;
        t1 = MathF.Abs(t1); // Can be minus on edge case. Not sure why. Maybe because float calc error. Maybe s value wrong.
        return (s*MathF.Sign(pos),t1,t2);
    }
    private static (int,float,float) CatchupInfo2(float pos, float vel, float acc)
    {
        if (acc == 0f) return (1,0,0);
        if (pos == 0 && vel == 0) return (1,0,0);
        var haltTime = MathF.Abs(vel)/acc;
        var pos2 = pos + vel*haltTime/2;
        if (pos2 == 0f) return (-MathF.Sign(vel),haltTime,0);
        var isOvershoot = MathF.Sign(pos) != MathF.Sign(pos2);
        var velAbs = MathF.Abs(vel);
        var duno1 = velAbs/acc;
        var duno2 = vel*duno1/2;
        if (isOvershoot)
        {
            Debug.LogError("DUNO");
            var duno3 = -duno2-pos;
            var t2 = MathF.Sqrt(MathF.Abs(duno3)/acc);
            var t1 = t2+duno1;
            return (MathF.Sign(pos),t1,t2);
        }
        else
        {
            var duno3 = duno2-pos;
            var t2 = MathF.Sqrt(MathF.Abs(duno3)/acc);
            var t1 = t2-duno1;
            // Debug.Log($"var t1 = {t2}-{velAbs}/{acc}");
            // t1 = MathF.Abs(t1); // Can be minus. Not sure why. Maybe because float calc error.
            return (-MathF.Sign(pos),t1,t2);
        }
    }
    // private static (int,float,float) CatchupInfoOld(float Apos, float Avel, float acc, float Bpos, float Bvel)
    // {
    //     if (acc == 0f) return (1,0,0);
    //     var vellDiff = Bvel - Avel;
    //     var posDiff = Bpos - Apos;
    //     if (posDiff == 0 && vellDiff == 0) return (1,0,0);
    //     var vellDiffAbs = MathF.Abs(vellDiff);
    //     var t1 = vellDiffAbs/acc;
    //     var p1 = posDiff + t1/2*vellDiff;
    //     var s = MathF.Sign(p1);
    //     if (s == 0) return (MathF.Sign(vellDiff),t1,0);
    //     var a = -s*acc/4;
    //     // var b = Bvel - V0;
    //     var b = (vellDiff - s*vellDiffAbs) / 2;
    //     var c = p1;
    //     var t2 = (-b - s*MathF.Sqrt(b*b - 4*a*c)) / (2*a);
    //     // t2 = Math.Abs(t2);
    //     var t2Half = t2/2;
    //     var s1 = s*MathF.Sign(p1 - posDiff) == -1 ? -1 : 1;
    //     var result = s1 == 1 ?
    //         (s,t1+t2Half,t2Half):
    //         (s,t2Half,t1+t2Half);
    //     return result;
    // }
}