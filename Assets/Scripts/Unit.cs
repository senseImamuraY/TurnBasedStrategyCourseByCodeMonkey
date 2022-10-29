using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
  private Vector3 targetPosition;

  private void Update()
  {
    // 単純に数値のみを使用することは非推奨。何の数値なのかを表した変数を使用するべき
    float stoppingDistance = 0.1f;
    if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
    {
      Vector3 moveDirection = (targetPosition - this.transform.position).normalized;
      float moveSpeed = 4f;
      transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    if (Input.GetKeyDown(KeyCode.T))
    {
      Move(new Vector3(4, 0, 4));
    }
    
  }
  private void Move(Vector3 targetPosition)
  {
    //Debug.Log(Vector3.Distance(targetPosition, this.transform.position));
    //if (Vector3.Distance(targetPosition, this.transform.position) > 0.1f)
    //{
      
    //}
      this.targetPosition = targetPosition;
  }
}
