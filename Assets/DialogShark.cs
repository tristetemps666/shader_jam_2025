using UnityEngine;

public class DialogShark : MonoBehaviour
{
    [SerializeField] Transform SharkTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = new Vector3(SharkTransform.Position.x, SharkTransform.Position.y, 0);
        transform.LookAt(SharkTransform.position);
    }
}
