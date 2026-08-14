using System.Collections;
using UnityEngine;

public class AddBlockButton : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform list;

    public void AddItem()
    {
        CodeBlock newBlock = Instantiate(prefab).GetComponent<CodeBlock>();

        StartCoroutine(SetFirstNextFrame(newBlock.transform));

        newBlock.GetComponent<CodeBlockDragHandler>().list = list.GetComponent<RectTransform>();
        newBlock.SetQuantifier(1);
        newBlock.transform.SetParent(list);

        CodeListEvents.OnAddBlock?.Invoke();
    }
    

    IEnumerator SetFirstNextFrame(Transform t)
    {
        yield return null; // wait 1 frame
        t.SetSiblingIndex(list.childCount - 2);
    }
}
