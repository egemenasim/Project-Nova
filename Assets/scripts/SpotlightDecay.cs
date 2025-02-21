using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpotlightDecay : MonoBehaviour
{
    private Light2D spotlight;
    public float decayRate = 0.5f; // Işığın küçülme hızı
    public float minInnerRadius = 0.1f;
    public float minOuterRadius = 0.2f;

    void Start()
    {
        spotlight = GetComponent<Light2D>();
        if (spotlight == null || spotlight.lightType != Light2D.LightType.Point) // Spot yerine Point kullanıyoruz
        {
            Debug.LogError("Bu script yalnızca Point Light 2D objelerine eklenmelidir!");
            enabled = false;
        }
    }

    void Update()
    {
        if (spotlight != null)
        {
            // Işığın zamanla azalmasını sağla
            spotlight.pointLightInnerRadius = Mathf.Max(minInnerRadius, spotlight.pointLightInnerRadius - decayRate * Time.deltaTime);
            spotlight.pointLightOuterRadius = Mathf.Max(minOuterRadius, spotlight.pointLightOuterRadius - decayRate * Time.deltaTime);
        }
    }
}
