using UnityEngine;
using UnityEngine.UI;

public class RawImageScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;    // The speed at which the image should scroll
    public float scrollDuration = 5f;   // The duration in seconds of one complete scroll cycle

    private RawImage rawImage;
    private float scrollOffset = 0f;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        float cycleOffset = Mathf.Repeat(Time.time / scrollDuration, 1f);
        scrollOffset = cycleOffset;
        rawImage.uvRect = new Rect(scrollOffset, scrollOffset, 1f, 1f);
    }
}
