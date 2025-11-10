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

    uint _kernelSizeX,
        _kernelSizeY,
        _kernelSizeZ;

    int groupsX,
        groupsY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _kernel = _shaderTest.FindKernel("CSMain");
        _shaderTest.GetKernelThreadGroupSizes(
            _kernel,
            out _kernelSizeX,
            out _kernelSizeY,
            out _kernelSizeZ
        );

        groupsX = Mathf.CeilToInt((float)_ResultRenderTexture.width / _kernelSizeX);
        groupsY = Mathf.CeilToInt((float)_ResultRenderTexture.height / _kernelSizeY);

        _shaderTest.SetTexture(_kernel, "Result", _ResultRenderTexture);
        _shaderTest.SetTexture(_kernel, "updateTexture", _UpdateRenderTexture);

        ClearOutRenderTexture(_ResultRenderTexture);
    }

    // Update is called once per frame
    void Update()
    {
        _shaderTest.Dispatch(_kernel, groupsX, groupsY, (int)_kernelSizeZ);
    }

    public void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = rt;
    }
}
