using UnityEngine;

public class GetChest : MonoBehaviour
{
    [SerializeField] private Inventory inv;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inv.OpenChest();
            Destroy(gameObject);
        }
    }
}
