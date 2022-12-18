using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraGameObject;

    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

        HideActionCamera();
    }

    private void ShowActionCamera()
    {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGameObject.SetActive(false);
    }

    private void BaseAction_OnAnyActionStarted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();

                Vector3 cameraCharacterHeight = Vector3.up * 1.7f;

                Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

                //float shoulderOffsetAmount = 50f;
                float shoulderOffsetAmount = 0.5f;
                // shootDir��y�͂O �E������`���Ă���悤�ȃA���O���ɂȂ�@�N�H�[�^�j�I���ƃx�N�g����������ƃx�N�g���ɂȂ�@
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) *shootDir * shoulderOffsetAmount;

                // �ˌ������Ă��郆�j�b�g���݂����̂ōŌ�ɂ�⎋�_������
                Vector3 actionCameraPosition = 
                    shooterUnit.GetWorldPosition() +
                    cameraCharacterHeight +
                    shoulderOffset +
                    (shootDir * -1);

                Debug.Log("shoulderOffset = " + shoulderOffset + " actionCameraPosition = " + actionCameraPosition + " shootDir = " + shootDir);
                actionCameraGameObject.transform.position = actionCameraPosition;
                actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);

                ShowActionCamera();
                break;
        }
    }
    private void BaseAction_OnAnyActionCompleted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }
}
