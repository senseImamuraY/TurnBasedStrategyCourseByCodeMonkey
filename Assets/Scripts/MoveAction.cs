using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }
    //public void Movement(Animator unitAnimator, Vector3 targetPosition)
    private void Update()
    {
        // 単純に数値のみ(マジックナンバー)を使用することは非推奨。何の数値なのかを表した変数を使用するべき
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - this.transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }


    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPositon();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetgridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetgridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    // Same GridPosition where the unit is already at
                    // offsetgridPositionが0,0つまり今いる位置のときは処理を行わない
                    continue;
                }
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
