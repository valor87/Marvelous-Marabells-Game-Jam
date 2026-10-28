using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript
{
    public IEnumerator fadeFunctionUI(Image _UiImage, Color _imageColor, float _fadeAmount = 0.005f, float waitTime = 0.5f)
    {
        float fadeAmount = 0;
        while (fadeAmount < 1)
        {
            _UiImage.color = new Color(_imageColor.r, _imageColor.g, _imageColor.b, fadeAmount);
            fadeAmount += _fadeAmount;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        while (fadeAmount > 0)
        {
            _UiImage.color = new Color(_imageColor.r, _imageColor.g, _imageColor.b, fadeAmount);
            fadeAmount -= _fadeAmount;
            yield return null;
        }
    }

    public IEnumerator fadeFunctionSpryte(SpriteRenderer _sprite, Color _imageColor, float _fadeAmount = 0.005f, float waitTime = 0.5f)
    {
        float fadeAmount = 0;
        while (fadeAmount < 1)
        {
            _sprite.color = new Color(_imageColor.r, _imageColor.g, _imageColor.b, fadeAmount);
            fadeAmount += _fadeAmount;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        while (fadeAmount > 0)
        {
            _sprite.color = new Color(_imageColor.r, _imageColor.g, _imageColor.b, fadeAmount);
            fadeAmount -= _fadeAmount;
            yield return null;
        }
    }
}
