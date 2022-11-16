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
            isActive = false;
            onActionComplete();
        }
    }
    public void Spin(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        totalSpinAmount = 0f;
    }
}
