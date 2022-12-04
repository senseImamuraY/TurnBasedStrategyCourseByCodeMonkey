using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpinAction : BaseAction
{
    //public delegate void SpinCompleteDelegate();


    private float totalSpinAmount;
    // Action �̓f���Q�[�g�̏��������ȈՉ����Ă��邾��
    // �f���Q�[�g�͉����̏������I������Ƃ��ɁA�����ȊO�̃N���X����֐����Ăяo�������Ƃ��ɂ����i��j
    // �܂��A�ˑ��֌W�̋t�]�Ƃ��Ă�����
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
