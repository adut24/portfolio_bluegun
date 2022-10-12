using UnityEngine;

public class LightTorch : MonoBehaviour
{
    public GameObject offTorch, onTorch;

    private void Start()
    {
        onTorch.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(offTorch);
            onTorch.SetActive(true);
        }
    }
}
