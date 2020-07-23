using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMessagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 2f;
    private static int _sortingOrder;

    private TextMeshPro _textMesh;
    private float _disappearTimer;
    private Color _textColor;
    private Vector3 _moveVector;

    static GameMessagePopup _gameMessagePopup;

    private static Transform _player;

    public static GameMessagePopup Create(Transform player, string message)
    {
        Transform gameMessagePopupTransform;
        GameObject gameMessagePopupObject = Managers.Resource.Instantiate("UI/@GameMessagePopup");
        if (gameMessagePopupObject != null)
        {
            _player = player;

            gameMessagePopupObject.transform.position = _player.position + Vector3.up * 2;
            gameMessagePopupObject.transform.rotation = Managers.Camera.Main.transform.rotation;

            gameMessagePopupTransform = gameMessagePopupObject.transform;
            GameMessagePopup gameMessagePopup = gameMessagePopupTransform.GetComponent<GameMessagePopup>();
            gameMessagePopup.Setup(message);

            if (_gameMessagePopup != null)
            {
                Destroy(_gameMessagePopup.gameObject);
                _gameMessagePopup = null;
            }

            _gameMessagePopup = gameMessagePopup;
            return gameMessagePopup;
        }

        return null;
    }

    private void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(string message)
    {
        _textMesh.SetText(message);
        _textColor = Color.red;

        _textMesh.color = _textColor;
        _disappearTimer = DISAPPEAR_TIMER_MAX;

        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;
        _textMesh.fontSize = 1;
        _moveVector = new Vector3(0, 0.1f);
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;

        transform.rotation = Managers.Camera.Main.transform.rotation;

        transform.position = _player.position + Vector3.up * 2;
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * Time.deltaTime;

        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            float disappearSpeed = 1f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
