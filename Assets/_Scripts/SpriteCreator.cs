using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Part { Hair, Eyebrow, Eye, Nose, Skin, Mouth, Shirt, Sleeve, Pants, Shoe }
public class SpriteCreator : MonoBehaviour
{   
    //Could load this in on start so it doesn't have to be set in inspector if multiple instances.
    [SerializeField]
    private SpriteRenderer lEyebrow,rEyebrow,lEye,rEye,mouth, nose, hair,waist,lPant,rPant,shirt,lShoe,rShoe,head, neck,lSleeve,rSleeve, lArm,rArm,lHand,rHand,lLeg,rLeg;

    [SerializeField]
    private CreatorUIController uiController;

    private Object[] eyebrowSprites, eyeSprites, mouthSprites, noseSprites, hairSprites, pantSprites, sleeveSprites, torsoSprites, shoeSprites, skinSprites;

    private int eyebrowID, eyeID, mouthID, noseID, hairID, pantsID=1, shirtID,sleeveID, shoeID, skinID;

    int skinParts = 0, noseStyles = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        eyebrowSprites = Resources.LoadAll("BodyParts/Face/Eyebrows", typeof(Sprite));
        eyeSprites = Resources.LoadAll("BodyParts/Face/Eyes", typeof(Sprite));
        mouthSprites = Resources.LoadAll("BodyParts/Face/Mouth", typeof(Sprite));
        noseSprites = Resources.LoadAll("BodyParts/Face/Nose", typeof(Sprite));
        hairSprites = Resources.LoadAll("BodyParts/Hair", typeof(Sprite));
        pantSprites = Resources.LoadAll("BodyParts/Pants", typeof(Sprite));
        sleeveSprites = Resources.LoadAll("BodyParts/Shirts/Sleeves", typeof(Sprite));
        torsoSprites = Resources.LoadAll("BodyParts/Shirts/Torso", typeof(Sprite));
        shoeSprites = Resources.LoadAll("BodyParts/Shoes", typeof(Sprite));
        skinSprites = Resources.LoadAll("BodyParts/Skin", typeof(Sprite));

        skinParts = Resources.LoadAll("BodyParts/Skin/tint1", typeof(Sprite)).Length;
        noseStyles = Resources.LoadAll("BodyParts/Face/Nose/tint1", typeof(Sprite)).Length;

        ChangeEyebrows(uiController.colourList[uiController.colourID[1]]);
        ChangeEyes(uiController.colourList[uiController.colourID[2]]);
        ChangeHair(uiController.colourList[uiController.colourID[0]]);
        ChangePants(uiController.colourList[uiController.colourID[8]]);
        ChangeShirt(uiController.colourList[uiController.colourID[6]]);
        ChangeShoes(uiController.colourList[uiController.colourID[9]]);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
    }
    void ChangeEyebrows(Color c)
    {
        lEyebrow.sprite = eyebrowSprites[eyebrowID] as Sprite;
        lEyebrow.color = c;
        rEyebrow.sprite = eyebrowSprites[eyebrowID] as Sprite;
        rEyebrow.color = c;
    }
    void ChangeEyes(Color c)
    {
        lEye.sprite = eyeSprites[eyeID] as Sprite;
        lEye.color = c;
        rEye.sprite = eyeSprites[eyeID] as Sprite;
        rEye.color = c;
    }
    void ChangeMouth()
    {
        mouth.sprite = mouthSprites[mouthID] as Sprite;
    }
    void ChangeNose()
    {
        nose.sprite = noseSprites[skinID * noseStyles + noseID] as Sprite;
    }
    void ChangeHair(Color c)
    {
        hair.sprite = hairSprites[hairID] as Sprite;
        hair.color = c;
    }
    void ChangePants(Color c)
    {
        lPant.sprite = pantSprites[pantsID] as Sprite;
        lPant.color = c;
        rPant.sprite = pantSprites[pantsID] as Sprite;
        rPant.color = c;
        waist.sprite = pantSprites[0] as Sprite;
        waist.color = c;
    }
    void ChangeShirt(Color c)
    {
        shirt.sprite = torsoSprites[shirtID] as Sprite;
        shirt.color = c;
        rSleeve.sprite = sleeveSprites[sleeveID] as Sprite;
        rSleeve.color = c;
        lSleeve.sprite = sleeveSprites[sleeveID] as Sprite;
        lSleeve.color = c;
    }
    void ChangeShoes(Color c)
    {
        lShoe.sprite = shoeSprites[shoeID] as Sprite;
        lShoe.color = c;
        rShoe.sprite = shoeSprites[shoeID] as Sprite;
        rShoe.color = c;
    }
    void ChangeSkinColour() {
        int id = skinID * skinParts;
        lArm.sprite = skinSprites[id] as Sprite;
        rArm.sprite = skinSprites[id] as Sprite;
        lHand.sprite = skinSprites[id+1] as Sprite;
        rHand.sprite = skinSprites[id+1] as Sprite;
        head.sprite = skinSprites[id+2] as Sprite;
        lLeg.sprite = skinSprites[id + 3] as Sprite;
        rLeg.sprite = skinSprites[id + 3] as Sprite;
        neck.sprite = skinSprites[id + 4] as Sprite;
        nose.sprite = noseSprites[skinID * noseStyles + noseID] as Sprite;
    }
    void ChangePart(Part _part, int _increment)
    {
        Color c = uiController.colourList[uiController.colourID[uiController.currentID]];
        switch (_part)
        {
            case Part.Eye:
                eyeID += _increment;
                if (eyeID >= eyeSprites.Length) eyeID = 0;
                if (eyeID < 0) eyeID = eyeSprites.Length - 1;
                ChangeEyes(c);
                break;
            case Part.Eyebrow:
                eyebrowID += _increment;
                if (eyebrowID >= eyebrowSprites.Length) eyebrowID = 0;
                if (eyebrowID < 0) eyebrowID = eyebrowSprites.Length - 1;
                ChangeEyebrows(c);
                break;
            case Part.Hair:
                hairID += _increment;
                if (hairID >= hairSprites.Length) hairID = 0;
                if (hairID < 0) hairID = hairSprites.Length - 1;
                ChangeHair(c);
                break;
            case Part.Mouth:
                mouthID += _increment;
                if (mouthID >= mouthSprites.Length) mouthID = 0;
                if (mouthID < 0) mouthID = mouthSprites.Length - 1;
                ChangeMouth();
                break;
            case Part.Nose:
                noseID += _increment;
                if (noseID >= noseStyles) noseID = 0;
                if (noseID < 0) noseID = noseStyles;
                ChangeNose();
                break;
            case Part.Pants:
                pantsID += _increment;
                if (pantsID >= pantSprites.Length) pantsID = 1;
                if (pantsID < 1) pantsID = pantSprites.Length - 1;
                ChangePants(c);
                break;
            case Part.Shirt:
                shirtID += _increment;
                if (shirtID >= torsoSprites.Length) shirtID = 0;
                if (shirtID < 0) shirtID = torsoSprites.Length - 1;
                ChangeShirt(c);
                break;
            case Part.Shoe:
                shoeID += _increment;
                if (shoeID >= shoeSprites.Length) shoeID = 0;
                if (shoeID < 0) shoeID = shoeSprites.Length - 1;
                ChangeShoes(c);
                break;
            case Part.Sleeve:
                sleeveID += _increment;
                if (sleeveID >= sleeveSprites.Length) sleeveID = 0;
                if (sleeveID < 0) sleeveID = sleeveSprites.Length - 1;
                ChangePart(Part.Shirt, 0);
                break;
            case Part.Skin:
                skinID += _increment;
                if (skinID >= skinSprites.Length / skinParts) skinID = 0;
                if (skinID < 0) skinID = (skinSprites.Length / skinParts) - 1;
                ChangeSkinColour();
                break;
        }
    }

    Color GenerateRandomColour()
    {
        float r, g, b;
        r = Random.Range(0f, 255f);
        g = Random.Range(0f, 255f);
        b = Random.Range(0f, 255f);
        Color c = new Color(r / 255, g / 255, b / 255);
        return c;
    }

    void UserInput()
    {
        if (uiController.currentColumn == 0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangePart((Part)uiController.currentID, 1);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangePart((Part)uiController.currentID, -1);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            uiController.VerticalChange(-1);
            if(uiController.currentColumn == 1)
            {
                ChangePart((Part)uiController.currentID, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            uiController.VerticalChange(1);
            if (uiController.currentColumn == 1)
            {
                ChangePart((Part)uiController.currentID, 0);
            }
        }
        if ((Part)uiController.currentID != Part.Skin && (Part)uiController.currentID != Part.Nose && (Part)uiController.currentID != Part.Mouth && (Part)uiController.currentID != Part.Sleeve)
        {
            uiController.SetDarkener(false);
            if (Input.GetKeyDown(KeyCode.A) && uiController.useColour)
            {

                uiController.HorizontalChange(-1);
            }
            if (Input.GetKeyDown(KeyCode.D) && uiController.useColour)
            {
                uiController.HorizontalChange(1);
            }
        }
        else
        {
            uiController.SetDarkener(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomiseAll();

        }
    }

    void RandomiseAll()
    {
        uiController.RandomiseColours();
        eyebrowID = Random.Range(0, eyebrowSprites.Length);
        eyeID = Random.Range(0, eyeSprites.Length);
        mouthID = Random.Range(0, mouthSprites.Length);
        noseID = Random.Range(0, noseStyles);
        hairID = Random.Range(0, hairSprites.Length);
        pantsID = Random.Range(1, pantSprites.Length);
        shirtID = Random.Range(0, torsoSprites.Length);
        sleeveID = Random.Range(0, sleeveSprites.Length);
        shoeID = Random.Range(0, shoeSprites.Length);
        skinID = Random.Range(0, skinSprites.Length/skinParts);
        ChangeEyebrows(uiController.colourList[uiController.colourID[1]]);
        ChangeEyes(uiController.colourList[uiController.colourID[2]]);
        ChangeMouth();
        ChangeNose();
        ChangeHair(uiController.colourList[uiController.colourID[0]]);
        ChangePants(uiController.colourList[uiController.colourID[8]]);
        ChangeShirt(uiController.colourList[uiController.colourID[6]]);
        ChangeShoes(uiController.colourList[uiController.colourID[9]]);
        ChangeSkinColour();
    }
}
