using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
  
  private GridSystem gridSystem;
  private GridPosition gridPosition;
  private Unit unit;

  public GridObject(GridSystem gridSystem, GridPosition gridPosition)
  {
    
    this.gridSystem = gridSystem;
    this.gridPosition = gridPosition;
  }

  //public string MatrixPosition()
  //{
  //  return gridPosition.MatrixPosition();
  //}
  public override string ToString()
  {
    return gridPosition.ToString() + "\n" + unit;
  }

  public void SetUnit(Unit unit)
  {
    this.unit = unit;
  }

  public Unit GetUnit()
  {
    return unit;
  }
}
