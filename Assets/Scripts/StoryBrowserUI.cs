using UnityEngine;
using UnityEngine.UI;


public class StoryBrowserUI : MonoBehaviour {
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject scrollContent;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private Loader loader;


    private void OnEnable() {
        foreach (string fileName in LoadStory.StoryFiles) {
            Button storyFileButton = Instantiate(buttonPrefab, scrollContent.transform).GetComponent<Button>();
            storyFileButton.onClick.AddListener(delegate {
                var story = LoadStory.Load(fileName);
                loader.StoryLoader(story, 0);
                canvas.SetActive(false);
                buttonPanel.SetActive(true);
            });
            storyFileButton.GetComponentInChildren<Text>().text = fileName;
        }
    }

    private void OnDisable() {
        foreach (Transform child in scrollContent.transform) {
            Destroy(child.gameObject);
        }
    }
}