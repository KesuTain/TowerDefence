using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronEntity : MonoBehaviour
{
    private bool DeleteObj;
    void Start()
    {
        DeleteObj = true;
    }

    void Update()
    {
        transform.Translate(new Vector3(50f * Time.deltaTime, 0f, 0f));
        if (DeleteObj)
            StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        DeleteObj = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
