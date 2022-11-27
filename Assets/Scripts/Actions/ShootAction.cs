using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAction : BaseAction
{

    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }

    private State state;
    private int maxShootDistance = 7;
    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;

    //private bool isRotateFinished;
    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        stateTimer -= Time.deltaTime;


        switch (state)
        {
            case State.Aiming:
                Vector3 aimDir = targetUnit.GetWorldPosition() - unit.GetWorldPosition().normalized;
                //if(!isRotateFinished)
                //{
                //    RotateAction(targetUnit);
                //}
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);

                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:
                break;
        }
        if (stateTimer <= 0f)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                float CooloffStateTime = 0.5f;
                stateTimer = CooloffStateTime;
                break;
            case State.Cooloff:
                isActive = false;
                onActionComplete();
                break;
        }

    }

    private void Shoot()
    {
        targetUnit.Damage();
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetgridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetgridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }


                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid position is empty, no unit
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    // Both Units on same "team"
                    continue;
                }
                //Debug.Log(testGridPosition);
                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);


        state = State.Aiming;
        float amingStateTime = 1f;
        stateTimer = amingStateTime;

        canShootBullet = true;
    }

    //public void RotateAction(Unit targetUnit)
    //{
    //    Vector3 moveDirection = (targetUnit.transform.position - this.transform.position).normalized;

    //    float rotateSpeed = 10f;
    //    transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    //    if (stateTimer <= 0f)
    //    {
    //        isRotateFinished = true;
    //    }
    //    else
    //    {
    //        isRotateFinished = false;
    //    }
    //}

}
