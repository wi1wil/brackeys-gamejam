using UnityEngine;
using UnityEditor;
using NavMeshPlus.Components;
using UnityEngine.AI;
using NavMeshPlus.Extensions;

public class BuildNavMeshScript : MonoBehaviour
{
    [MenuItem("Tools/Add Build NavMesh")]
    public static void BuildNavMesh()
    {
        var surface = GameObject.FindObjectOfType<NavMeshSurface>();
        surface.BuildNavMesh();
    }
}
