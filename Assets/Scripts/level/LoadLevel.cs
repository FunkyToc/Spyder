using System.Collections;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    [SerializeField] Collider _coll;
    [SerializeField] float _loadDelay;
    [SerializeField] string _sceneName;

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("trig");
        if (obj.gameObject.GetComponentInParent<PlayerTag>())
        {
        Debug.Log("load");
            StartCoroutine(LoadDelay());
        }
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(_loadDelay);
        GameManager.GM._lm.LoadLevel(_sceneName);
    }

}
