using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
  void Update()
  {

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Debug.Log(Physics.Raycast(ray, out RaycastHit raycastHit));

  }
}
