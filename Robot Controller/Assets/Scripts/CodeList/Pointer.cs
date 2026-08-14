using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, -50, 0);

    public void SetPointer(MonoBehaviour block)
    {
        transform.SetParent(block.transform, false);
        transform.localPosition = offset;
    }
}
