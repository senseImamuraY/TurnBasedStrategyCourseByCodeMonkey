using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpinAction : BaseAction
{
    //public delegate void SpinCompleteDelegate();


    private float totalSpinAmount;
    // Action はデリゲートの書き方を簡易化しているだけ
    // デリゲートは何かの処理が終わったときに、自分以外のクラスから関数を呼び出したいときにつかう（例）
    // また、依存関係の逆転としてもつかう
    //private Action onSpinComplete;

    //private SpinCompleteDelegate onSpinComplete;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360f)
        {
            ActionComplete();
        }
    }


    public override void TakeAction(GridPosition gridPositon, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        totalSpinAmount = 0f;
    }



    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        //List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }
}
