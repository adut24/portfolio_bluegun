using UnityEngine;

public class FirstChest : MonoBehaviour
{
    [SerializeField] private Inventory inv;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inv.OpenFirstChest();
            Destroy(gameObject);
        }
    }
}
