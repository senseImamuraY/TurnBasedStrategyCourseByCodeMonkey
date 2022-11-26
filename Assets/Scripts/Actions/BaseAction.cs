using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract ���N���X�ɕt����ƁA���ۃ��\�b�h��1�ȏ�܂ރN���X�Ƃ����Ӗ��ɂȂ�
// �������L�q���Ȃ��ƃG���[�ɂȂ�̂Ōp����ł̎����R���h�����ʂ�����
// ����ɓ���̃N���X�i�����ł�BaseAction�j�̃C���X�^���X���쐬�ł��Ȃ�
public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;
    
    // virtual �����ăI�[�o���C�h��������
    // virtual�@�����Ȃ��Ă����삷�邪�A�኱�������ς��
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
