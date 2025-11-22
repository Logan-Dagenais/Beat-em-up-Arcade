using UnityEngine;
using UnityEngine.UI;

public class SelectionAnim : MonoBehaviour
{
    public Image image;
    public Sprite[] frames;
    [SerializeField] private float duration = 0.75f; // 45 frames at 60fps = 0.75 seconds

    [SerializeField] private float timer;
    [SerializeField] private bool isPlaying;
    [SerializeField] private int currentFrame;

    public void MenuSwitch()
    {
        print("menuswitch");
        if (frames.Length == 0 || image == null) return;
        timer = 0f;
        currentFrame = 0;
        image.sprite = frames[currentFrame];
        isPlaying = true;
        print("menuswitchdone");
    }

    public void StopAnimation()
    {
        print("stopping");
        isPlaying = false;
    }

    public void MenuClosed()
    {
        print("menuclosed");
        isPlaying = false;
        currentFrame = 0;
        if (frames.Length > 0 && image != null)
            image.sprite = frames[0];
    }

    void Update()
    {
        if (!isPlaying || frames.Length == 0 || image == null) return;

        // Use unscaledDeltaTime so animation continues while game is paused
        timer += Time.unscaledDeltaTime;
        float normalized = (timer % duration) / duration;

        int frameIndex = Mathf.FloorToInt(normalized * frames.Length);

        if (frameIndex != currentFrame)
        {
            currentFrame = frameIndex;
            image.sprite = frames[currentFrame];
        }
    }
}