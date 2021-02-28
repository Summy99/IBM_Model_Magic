using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackball : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("TiltAnim");
    }

    private IEnumerator TiltAnim()
    {
        while (true)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -135));
            yield return new WaitForSeconds(0.3f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -155));
            yield return new WaitForSeconds(0.3f);
        }
    }
}
