using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract をクラスに付けると、抽象メソッドを1つ以上含むクラスという意味になる
// 正しく記述しないとエラーになるので継承先での実装漏れを防ぐ効果がある
// さらに特定のクラス（ここではBaseAction）のインスタンスを作成できない
public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;
    
    // virtual をつけてオーバライドを許可する
    // virtual　をつけなくても動作するが、若干挙動が変わる
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }
}
