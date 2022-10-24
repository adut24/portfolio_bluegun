using UnityEngine;
using UnityEngine.UI;

public class ArtifactCooldown : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void Awake()
    {
        slider.value = 0;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
