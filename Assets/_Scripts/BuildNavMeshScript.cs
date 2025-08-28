using UnityEngine;
using UnityEditor;
using NavMeshPlus.Components;

public class BuildNavMeshScript : MonoBehaviour
{
    [MenuItem("Tools/Add Build NavMesh")]
    public static void BuildNavMesh()
    {
        var surface = GameObject.FindObjectOfType<NavMeshSurface>();
        surface.BuildNavMesh();
    }
}
