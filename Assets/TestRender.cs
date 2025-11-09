using UnityEngine;

public class TestRender : MonoBehaviour
{
    [SerializeField]
    ComputeShader _shaderTest;

    [SerializeField]
    RenderTexture _ResultRenderTexture;

    [SerializeField]
    RenderTexture _UpdateRenderTexture;

    int _kernel = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _kernel = _shaderTest.FindKernel("CSMain");

        _shaderTest.SetTexture(_kernel, "Result", _ResultRenderTexture);
        _shaderTest.SetTexture(_kernel, "updateTexture", _UpdateRenderTexture);

        ClearOutRenderTexture(_ResultRenderTexture);
    }

    // Update is called once per frame
    void Update()
    {
        _shaderTest.Dispatch(_kernel, 16, 16, 1);
    }

    public void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = rt;
    }
}
