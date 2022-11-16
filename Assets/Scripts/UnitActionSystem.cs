using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitActionSystem : MonoBehaviour
{
    // private set �œǂݍ��ݐ�p�ɂ���@set���g���Ȃ��Ȃ�
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There`s more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {

        if (isBusy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                // (instance���g���A�V���O���g�����g����)�N���X��.�֐����ŃA�N�Z�X�ł���炵��
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    private void SetBusy()
    {
        isBusy = true;
    }
    private void ClearBusy()
    {
        isBusy = false;
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, this.unitLayerMask))
        {
            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        // EventArgs.Empty�͈����Ȃ���\��
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        Debug.Log(selectedUnit);
    }


    public Unit GetSelectedUnit()
    {
        // �ǂݍ��݂���������
        return selectedUnit;
    }
}
