using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Start()
    {
    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            GridPosition startGridPosition = new GridPosition(0, 0);

            List<GridPosition> gridPositionList = Pathfinding.Instance.FindPath(startGridPosition, mouseGridPosition);

            for (int i = 0; i < gridPositionList.Count; i++)
            {
                Debug.DrawLine(
                        LevelGrid.Instance.GetWorldPosition(gridPositionList[i]),
                        LevelGrid.Instance.GetWorldPosition(gridPositionList[i + 1]),
                        UnityEngine.Color.white,
                        10f
                        );
            }
        }

    }

    private void FixedUpdate()
    {
        
    }
}
