using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BodyPart { Head, Torso, Legs, Shoes }

public class ModelCreator : MonoBehaviour
{
    [SerializeField]
    private int textureSizeX, textureSizeY, rotationSpeed;
    [SerializeField]
    private SkinnedMeshRenderer rend;
    [SerializeField]
    private CreatorUIController uiController;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject characterObject;

    [SerializeField]
    private Rect headTexData,
        torsoTexData,
        legsTexData,
        handsTexData,
        shoesTexData;


    [SerializeField]
    private List<Texture2D> skins;

    private Texture2D mainTex;
    private Material mainMat;

    private int currentHeadID = 0,
        currentTorsoID = 0,
        currentLegsID = 0,
        currentShoesID = 0;

    void Start()
    {
        mainMat = new Material(rend.material);
        mainTex = new Texture2D(textureSizeX, textureSizeY, TextureFormat.RGBA64, false);
        mainTex.SetPixels((rend.material.mainTexture as Texture2D).GetPixels(0, 0, textureSizeX, textureSizeY));

        currentHeadID = Random.Range(0, skins.Count);
        currentTorsoID = Random.Range(0, skins.Count);
        currentLegsID = Random.Range(0, skins.Count);
        currentShoesID = Random.Range(0, skins.Count);

        ModifyTexture(BodyPart.Head,0);
        ModifyTexture(BodyPart.Torso, 0);
        ModifyTexture(BodyPart.Legs, 0);
        ModifyTexture(BodyPart.Shoes, 0);

        gameObject.SetActive(false);
        //function that takes in one of each section (head/arms/body/legs) from the current list of whole textures.
        //function stitches them together for new texture
        //texture is then applied
    }

    void ModifyTexture(BodyPart _part, int _increment)
    {
        float x=0, y=0, width=0, height=0;
        int id = 0;
        switch (_part)
        {
            case BodyPart.Head:
                x = headTexData.position.x;
                y = headTexData.y;
                width = headTexData.width;
                height = headTexData.height;
                currentHeadID += _increment;
                if (currentHeadID >= skins.Count)
                    currentHeadID = 0;
                if (currentHeadID < 0)
                    currentHeadID = skins.Count - 1;
                id = currentHeadID;
                break;
            case BodyPart.Torso:
                x = torsoTexData.x;
                y = torsoTexData.y;
                width = torsoTexData.width;
                height = torsoTexData.height;
                currentTorsoID += _increment;
                if (currentTorsoID >= skins.Count)
                    currentTorsoID = 0;
                if (currentTorsoID < 0)
                    currentTorsoID = skins.Count - 1;
                id = currentTorsoID;
                ModifyHands(id);
                break;
            case BodyPart.Legs:
                x = legsTexData.x;
                y = legsTexData.y;
                width = legsTexData.width;
                height = legsTexData.height;
                currentLegsID += _increment;
                if (currentLegsID >= skins.Count)
                    currentLegsID = 0;
                if (currentLegsID < 0)
                    currentLegsID = skins.Count - 1;
                id = currentLegsID;
                break;
            case BodyPart.Shoes:
                x = shoesTexData.x;
                y = shoesTexData.y;
                width = shoesTexData.width;
                height = shoesTexData.height;
                currentShoesID += _increment;
                if (currentShoesID >= skins.Count)
                    currentShoesID = 0;
                if (currentShoesID < 0)
                    currentShoesID = skins.Count - 1;
                id = currentShoesID;
                break;
        }

        Color[] pixels = skins[id].GetPixels((int)x,textureSizeY - (int)height - (int)y, (int)width, (int)height);
        mainTex.SetPixels((int)x, textureSizeY - (int)height - (int)y, (int)width, (int)height, pixels, 0);
        mainTex.Apply();
        mainMat.mainTexture = mainTex;
        rend.material = mainMat;
    }

    void ModifyHands(int id)
    {
        float x = handsTexData.x,
        y = handsTexData.y,
        width = handsTexData.width,
        height = handsTexData.height;

        Color[] handPixels = skins[id].GetPixels((int)x, textureSizeY - (int)height - (int)y, (int)width, (int)height);
        mainTex.SetPixels((int)x, textureSizeY - (int)height - (int)y, (int)width, (int)height, handPixels, 0);
        mainTex.Apply();
        mainMat.mainTexture = mainTex;
        rend.material = mainMat;
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
    }
    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ModifyTexture((BodyPart)uiController.currentID, 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ModifyTexture((BodyPart)uiController.currentID, -1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            uiController.VerticalChange(-1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            uiController.VerticalChange(1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            characterObject.transform.Rotate(new Vector3(0, -Time.deltaTime*rotationSpeed, 0));
        }
    }
}
