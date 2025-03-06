using UnityEngine;

// thanks to https://nielson.dev/2016/04/2d-sprite-outlines-in-unity
[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour
{
    private static readonly int Outline = Shader.PropertyToID("_Outline");
    private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
    public Color color = Color.white;
    public Material outlineMaterial;
    public bool isTile;

    private SpriteRenderer spriteRenderer;
    private GameObject tile;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateOutline(true);
    }

    void OnDisable()
    {
        UpdateOutline(false);
    }

    void Update()
    {
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline)
    {
        if (isTile)
        {
            if (outline && tile == null)
            {
                this.GetComponent<SpriteOutline>().enabled = false;
                tile = Instantiate(gameObject, transform.position, transform.rotation);
                this.GetComponent<SpriteOutline>().enabled = true;
                tile.transform.localScale = new Vector3(1.02f, 1.02f, 1.02f);
                SpriteRenderer render = tile.GetComponent<SpriteRenderer>();
                render.material = outlineMaterial;
                tile.transform.Translate(0, 0, transform.position.z+0.0001f);
                render.color = color;
                tile.transform.SetParent(transform);
            }
            else if (!outline && tile != null)
            {
                Destroy(tile);
                tile = null;
            }
            
        }
        else
        {
            MaterialPropertyBlock mpb = new();
            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat(Outline, outline ? 1f : 0);
            mpb.SetColor(OutlineColor, color);
            spriteRenderer.SetPropertyBlock(mpb);
        }
    }
}