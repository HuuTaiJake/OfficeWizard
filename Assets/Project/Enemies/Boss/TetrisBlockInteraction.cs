using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockInteraction : CreatureAttribute
{
    private TetrisBlock _tetrisBlock;

    public override void TakeNormalDamage(int dmgToTake)
    {
        _tetrisBlock = GetComponent<TetrisBlock>();
        _tetrisBlock.RotateBlock();
    }
}
