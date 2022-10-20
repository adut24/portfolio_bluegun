using UnityEngine;

public class GetChest : MonoBehaviour
{
    private Inventory inv;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inv.OpenChest();
            Destroy(gameObject);
        }
    }
}
