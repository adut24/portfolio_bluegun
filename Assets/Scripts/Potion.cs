using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healPower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().RegenerateLife(healPower);
            Destroy(gameObject);
        }
    }
}
