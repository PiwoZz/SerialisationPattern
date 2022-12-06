using Script.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {
    [Header("UI")] [SerializeField] private TextMeshProUGUI thumbnailName;
    [SerializeField] private TextMeshProUGUI thumbnailDesc;
    [SerializeField] private GameObject layoutChoiceParent;
    [SerializeField] private GameObject choicePrefab;

    public void StoryLoader(Story story, int index) {
        foreach (Transform child in layoutChoiceParent.transform) {
            Destroy(child.gameObject);
        }

        thumbnailName.text = story.Thumbnails[index].Title;
        thumbnailDesc.text = story.Thumbnails[index].Description;
        foreach (var variable in story.Thumbnails[index].Choices) {
            var currentChoice = Instantiate(choicePrefab, layoutChoiceParent.transform);
            currentChoice.GetComponentInChildren<Text>().text = variable.Description;
            currentChoice.GetComponent<Button>().onClick.AddListener(delegate {
                for (var i = 0; i < story.Thumbnails.Count; i++) {
                    if (story.Thumbnails[i].Guid == variable.ThumbnailGuid) {
                        StoryLoader(story, i);
                        return;
                    }

                    if (variable.ThumbnailGuid == null) {
                        Debug.LogWarning("Thumbnail is null");
                    }
                }
            });
        }
    }
}