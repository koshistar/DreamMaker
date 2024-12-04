using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClear : MonoBehaviour
{
    [Header("��������")]
    public float fadeDuration = 2f;
    public string nextSceneName;
    public Canvas fadeCanvas;
    public Image fadeImage;

    private bool isFading = false;

    private void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }

        if (fadeCanvas != null)
        {
            fadeCanvas.enabled = false;
        }
    }

    private void Update()
    {
        if (isFading)
        {
            float alpha = fadeImage.color.a + Time.deltaTime / fadeDuration;
            alpha = Mathf.Clamp01(alpha);

            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;

            if (alpha >= 1f)
            {
                isFading = false;
                AudioManaager.Instance.Play("bubble");
                // AchievementSystem.achievementCount = 0;
                SceneManager.LoadScene(nextSceneName); 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            if (fadeCanvas != null)
            {
                fadeCanvas.enabled = true; 
            }

            isFading = true;
        }
    }
}
