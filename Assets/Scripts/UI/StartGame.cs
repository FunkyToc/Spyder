using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    [SerializeField] string _targetSceneName = null;
    [SerializeField] InputActionReference _validAction;
    [SerializeField] UnityEvent _OnValidation;

    void Start()
    {
        _validAction.action.started += ValidAction;
        _validAction.action.canceled += CancelAction;
    }

    void OnDestroy()
    {
        _validAction.action.started -= ValidAction;
        _validAction.action.canceled -= CancelAction;
    }

    void ValidAction(InputAction.CallbackContext cc)
    {
        if (_targetSceneName != null)
        {
            LoadScene(_targetSceneName);
            _OnValidation?.Invoke();
        }
    }

    void CancelAction(InputAction.CallbackContext cc) {}

    public void LoadScene(string sceneName)
    {
        GameManager.GM._lm.LoadLevel(sceneName);
    }
}
