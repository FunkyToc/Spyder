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
    }

    void OnDestroy()
    {
        _validAction.action.started -= ValidAction;
    }

    void ValidAction(InputAction.CallbackContext cc)
    {
        if (_targetSceneName != null)
        {
            LoadScene(_targetSceneName);
            _OnValidation?.Invoke();
        }
    }

    public void LoadScene(string sceneName)
    {
        GameManager.GM._lm.LoadLevel(sceneName);
    }
}
