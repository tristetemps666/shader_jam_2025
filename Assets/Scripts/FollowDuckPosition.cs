using UnityEngine;

public class FollowDuckPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    Transform _transformToFollow;

    [SerializeField]
    Vector2 _offset;

    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPositionToFollow = Camera.main.WorldToScreenPoint(_transformToFollow.position);
        Debug.Log("PosToFollow : " + screenPositionToFollow);
        _rectTransform.localPosition = new Vector3(screenPositionToFollow.x-Camera.main.pixelWidth/2f+_offset.x,screenPositionToFollow.y-Camera.main.pixelHeight/2f+_offset.y,0f);
    }
}
