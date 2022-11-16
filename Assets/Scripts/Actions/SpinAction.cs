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
