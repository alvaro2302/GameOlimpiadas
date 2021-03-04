using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallexBackground : MonoBehaviour
{

    [SerializeField] private Vector2 parralexEfectMultiplier;
    private Transform cameraTransform;
    private Vector3 LastCameraPosition;
    private float textureUnitSizeX;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        LastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - LastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * parralexEfectMultiplier.x, deltaMovement.y * parralexEfectMultiplier.y);
        LastCameraPosition = cameraTransform.position;
        if((Mathf.Abs(cameraTransform.position.x -  transform.position.x))>=textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            if(cameraTransform.transform.position.x + offsetPositionX>0)
            {
                transform.position = new Vector3(cameraTransform.transform.position.x + offsetPositionX + 0.5f, transform.position.y);
            }
            else
            {
                transform.position = new Vector3(cameraTransform.transform.position.x + offsetPositionX - 0.5f, transform.position.y);
            }

        }

    }
}
