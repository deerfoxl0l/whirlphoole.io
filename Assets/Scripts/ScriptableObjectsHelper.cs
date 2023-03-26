using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectsHelper
{
    public static bool VerifyScriptableObject<T>(string filePath) where T : ScriptableObject
    {
        return Resources.Load<T>(filePath) != null ? true : false;
    }

    public static T GetScriptableObject<T>(string filePath) where T : ScriptableObject
    {
        return Resources.Load<T>(filePath);
    }

}
