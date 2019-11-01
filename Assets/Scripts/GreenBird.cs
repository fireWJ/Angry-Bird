using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird :  Bird{

    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = base.rbody.velocity;
        speed.x *= -1;
        speed.y *= -1;
        base.rbody.velocity = speed;
    }
}
