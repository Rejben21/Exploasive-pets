using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBombsController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float timeToExplode = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if(Input.GetKeyDown(inputKey) && bombsRemaining > 0)
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

        GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(timeToExplode);

        pos = bomb.transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

        Explosion explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(pos, Vector2.up, explosionRadius);
        Explode(pos, Vector2.down, explosionRadius);
        Explode(pos, Vector2.left, explosionRadius);
        Explode(pos, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }

    private void Explode(Vector2 pos, Vector2 direction, int lenght)
    {
        if(lenght <= 0)
        {
            return;
        }

        pos += direction;

        if (Physics2D.OverlapBox(pos, Vector2.one / 2, 0f, explosionLayerMask))
        {
            ClearDestructible(pos);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);
        explosion.SetActiveRenderer(lenght > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(pos, direction, lenght - 1);
    }

    private void ClearDestructible(Vector2 pos)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(pos);
        TileBase tile = destructibleTiles.GetTile(cell);

        if(tile != null)
        {
            Instantiate(destructiblePrefab, pos, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
}
