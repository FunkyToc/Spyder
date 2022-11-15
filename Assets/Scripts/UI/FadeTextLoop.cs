using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTextLoop : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float _fadeAmplitude = 1.0f;
    [Range(1f, 10f)]
    [SerializeField] float _fadeFrequency = 5.0f;

    private SpriteRenderer spr = null;
    private Image img = null;

    void Start()
    {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer comp))
        {
            spr = comp;
        }
        
        if (TryGetComponent<Image>(out Image comp2))
        {
            img = comp2;
        }
    }

    void Update()
    {
        float alpha = 1 - (Mathf.Cos(Time.time * _fadeFrequency) * _fadeAmplitude);

        // sprite
        if (spr != null)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, alpha);
        }
        
        // img
        if (img != null)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }
    }
}