using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private static int _sortingOrder;

    private TextMeshPro _textMesh;
    private float _disappearTimer;
    private Color _textColor;
    private Vector3 _moveVector;

    public static DamagePopup Create(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform;
        GameObject damagePopupObject = Managers.Resource.Instantiate("UI/@DamagePopup");
        if (damagePopupObject != null)
        {
            damagePopupObject.transform.position = position;
            damagePopupObject.transform.rotation = Managers.Camera.Main.transform.rotation;

            damagePopupTransform = damagePopupObject.transform;
            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.Setup(damageAmount, isCriticalHit);

            return damagePopup;
        }

        return null;
    }

    public void Setup(float damageAmount, bool isCriticalHit)
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
        _textMesh.SetText(damageAmount.ToString());
        if (isCriticalHit)
        {
            _textMesh.fontSize = 2;
            _textColor = Color.red;
        }
        else
        {
            _textMesh.fontSize = 1;
            _textColor = Color.yellow;
        }

        _textMesh.color = _textColor;
        _disappearTimer = DISAPPEAR_TIMER_MAX;

        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;

        _moveVector = new Vector3(0, 0.3f) * 3f;
    }

    private void LateUpdate()
    {
        transform.rotation = Managers.Camera.Main.transform.rotation;

        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * 3f * Time.deltaTime;

        if (_disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if (_textColor.a < 0)
                Destroy(gameObject);
        }

    }

}
