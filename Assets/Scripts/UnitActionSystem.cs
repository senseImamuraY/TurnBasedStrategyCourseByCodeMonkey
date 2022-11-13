using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitActionSystem : MonoBehaviour
{
    // private set で読み込み専用にする　setが使えなくなる
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

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

        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            // (instanceを使う、シングルトンを使うと)クラス名.関数名でアクセスできるらしい
            selectedUnit.Move(MouseWorld.GetPosition());
        }
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
        // EventArgs.Emptyは引数なしを表す
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        Debug.Log(selectedUnit);
    }


    public Unit GetSelectedUnit()
    {
        // 読み込みだけ許可した
        return selectedUnit;
    }
}
