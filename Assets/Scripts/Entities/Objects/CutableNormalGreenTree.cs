using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableNormalGreenTree : ToolHit
{
    public override void Hit()
    {
        Destroy(gameObject);
    }
}
