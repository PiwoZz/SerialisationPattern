using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FullSerializer;
using Script.Data;
using Script.Utils;
using UnityEngine;

public static class LoadStory {
    private static readonly fsSerializer Serializer = new fsSerializer();
    private static readonly string StoryPath = Application.streamingAssetsPath;

    public static List<string> StoryFiles => Directory.GetFiles(StoryPath, "*" + Properties.File.StoryExt)
        .Select(Path.GetFileName).ToList();


    public static Story Load(string storyNameWithExtension) {
        string path = StoryPath + Path.DirectorySeparatorChar + storyNameWithExtension;
        if (File.Exists(path)) File.OpenRead(path);
        else Debug.LogError("File not found");
        string fileJson = File.ReadAllText(path);
        return Deserialize(typeof(Story), fileJson) as Story;
    }

    private static object Deserialize(Type type, string serializedState) {
        fsData data = fsJsonParser.Parse(serializedState);
        object deserialized = null;
        Serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

        return deserialized;
    }
}