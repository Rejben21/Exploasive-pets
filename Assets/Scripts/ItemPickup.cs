using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        RadiusIncrease,
        SpeedBoost,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch(type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<PlayerBombsController>().AddBomb();
                break;

            case ItemType.RadiusIncrease:
                if (player.GetComponent<PlayerBombsController>().explosionRadius < 6)
                {
                    player.GetComponent<PlayerBombsController>().explosionRadius++;
                }
                break;

            case ItemType.SpeedBoost:
                if(player.GetComponent<PlayerController>().moveSpeed < 8)
                {
                    player.GetComponent<PlayerController>().moveSpeed++;
                }
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }
}
