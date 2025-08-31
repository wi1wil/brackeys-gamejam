using UnityEngine;
using Cinemachine;

public class CameraEdgeZoom : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public CinemachineConfiner2D confiner;
    public float zoomSpeed = 5f;
    public float normalSize = 5f;
    public float smallSpaceSize = 2f;

    void Start()
    {
        confiner = vcam.GetComponent<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SmallSpace"))
            StartCoroutine(ChangeSize(smallSpaceSize));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SmallSpace"))
            StartCoroutine(ChangeSize(normalSize));
    }

    private System.Collections.IEnumerator ChangeSize(float targetSize)
    {
        while (Mathf.Abs(vcam.m_Lens.OrthographicSize - targetSize) > 0.01f)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(
                vcam.m_Lens.OrthographicSize,
                targetSize,
                Time.deltaTime * zoomSpeed
            );
            updateConfiner();
            yield return null;
        }
    }

    private void updateConfiner()
    {
        confiner.m_Damping = 0.10f;
        confiner.InvalidateCache();
    }
}