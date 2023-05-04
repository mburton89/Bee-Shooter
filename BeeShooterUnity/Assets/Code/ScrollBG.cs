using UnityEngine;
using UnityEngine.UI;
public class ScrollBG : MonoBehaviour
{
    public float scrollSpeed;

    private RawImage renderer;
    private Vector2 savedOffset;

    void Start()
    {
        renderer = GetComponent<RawImage>();
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        //renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}