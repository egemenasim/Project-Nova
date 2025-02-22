using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpotlightDecay : MonoBehaviour
{
    private Light2D spotlight;
    public float decayRate = 0.5f; // Işığın küçülme hızı
    public float minInnerRadius = 0.1f;
    public float minOuterRadius = 0.2f;
    public float increaseAmount = 0.5f; // Işığın artış miktarı

    void Start()
    {
        spotlight = GetComponent<Light2D>();
        if (spotlight == null || spotlight.lightType != Light2D.LightType.Point)
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

  private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Bir objeye çarptım: " + other.gameObject.name); // Çarpışma kontrolü
    if (other.CompareTag("LightSphere"))
    {
        Debug.Log("LightSphere bulundu, ışık büyütülüyor."); // Doğru objeyi buldu mu?
        
        // Işığın büyümesini sağla
        spotlight.pointLightInnerRadius += increaseAmount;
        spotlight.pointLightOuterRadius += increaseAmount;
        
        // Işık küresini yok et
        Destroy(other.gameObject);
    }
}

}
