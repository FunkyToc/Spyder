using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Scene Transition")]
    [SerializeField] Image _bgFade;
    [Range(0f, 3f),SerializeField] float _fadeSpeed;

    Coroutine _ccoroutine;

    public void GoLastCheckpoint()
    {

    }

    public void ReloadLevel()
    {

    }

    public void LoadLevel(string levelName)
    {
        if (_ccoroutine == null)
        {
            _ccoroutine = StartCoroutine(LoadLevelTransition(levelName));
        }
    }

    IEnumerator LoadLevelTransition(string levelname)
    {
        float alpha;

        // fade in
        while (_bgFade.color.a < 1)
        {
            alpha = _bgFade.color.a + (_fadeSpeed * Time.deltaTime);
            _bgFade.color = new Color(_bgFade.color.r, _bgFade.color.g, _bgFade.color.b, alpha);
            yield return null;
        }

        // load
        SceneManager.LoadScene(levelname);
        yield return new WaitForSeconds(0.2f);

        // fade out
        while (_bgFade.color.a > 0)
        {
            alpha = _bgFade.color.a - (_fadeSpeed * Time.deltaTime);
            _bgFade.color = new Color(_bgFade.color.r, _bgFade.color.g, _bgFade.color.b, alpha);
            yield return null;
        }

        _ccoroutine = null;
    }
}
