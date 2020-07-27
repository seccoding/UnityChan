using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetPointerPopup : MonoBehaviour
{
    private static int _sortingOrder;

    private Transform _targetTransform;
    private TextMeshPro _textMesh;

    public static TargetPointerPopup Create(Transform targetTransform)
    {
        Transform targetPointerPopupTransform;
        GameObject targetPointerPopupObject = Managers.Resource.Instantiate("UI/@TargetPointer");
        if (targetPointerPopupObject != null)
        {
            targetPointerPopupObject.transform.position = targetTransform.position + Vector3.up * (targetTransform.GetComponent<Collider>().bounds.size.y);
            targetPointerPopupObject.transform.rotation = Managers.Camera.Main.transform.rotation;

            targetPointerPopupTransform = targetPointerPopupObject.transform;
            TargetPointerPopup targetPointerPopup = targetPointerPopupTransform.GetComponent<TargetPointerPopup>();
            targetPointerPopup.Setup(targetTransform);

            return targetPointerPopup;
        }

        return null;
    }

    private void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(Transform targetTransform)
    {
        _targetTransform = targetTransform;

        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;
    }

    public static void Destroy(TargetPointerPopup targetPointerPopup)
    {
        Destroy(targetPointerPopup.gameObject);
    }

    private void LateUpdate()
    {
        transform.position = _targetTransform.position + Vector3.up * (_targetTransform.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Managers.Camera.Main.transform.rotation;
    }

}
