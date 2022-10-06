using UnityEngine;

public class LightTorch : MonoBehaviour
{
    [SerializeField]
    private GameObject offTorch, onTorch;

    void Start()
    {
        onTorch.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(offTorch);
        onTorch.SetActive(true);
    }
}
