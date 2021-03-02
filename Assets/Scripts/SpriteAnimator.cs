using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] dash;
    // Start is called before the first frame update

  
    void Start()
    {

        StartCoroutine(DashAnimation());


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DashAnimation()
    {
        int i;
        i = 0;
        while (i < dash.Length)
        {
            spriteRenderer.sprite = dash[i];
            i++;
            //yield return new WaitForSeconds(0.05f);
            yield return 0;

        }
      

    }
}
