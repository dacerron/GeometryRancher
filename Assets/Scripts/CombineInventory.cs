using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CombineInventory : Inventory
{
    public InventorySlot LeftSlot;
    public InventorySlot RightSlot;
    public InventorySlot MergeSlot;
    public GameObject OutputText;
    public GameObject MutationText;
    public Dictionary<string, float> rarityWeights;
    public List<string> rarities;
    public float mutationChance = 0.05f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        occupiedSlots = new Dictionary<int, bool>();
        rarityWeights = new Dictionary<string, float>();
        rarityWeights.Add("Common", 0.4f);
        rarityWeights.Add("Uncommon", 0.25f);
        rarityWeights.Add("Magic", 0.2f);
        rarityWeights.Add("Rare", 0.1f);
        rarityWeights.Add("Legendary", 0.05f);
        rarities = new List<string> { "Common", "Uncommon", "Magic", "Rare", "Legendary" };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MergeShapes()
    {
        string outputText = "";
        if (LeftSlot.transform.childCount > 0 && RightSlot.transform.childCount > 0 && MergeSlot.transform.childCount == 0)
        {
            GameObject leftShape = LeftSlot.gameObject.transform.GetChild(0).gameObject;
            GameObject rightShape = RightSlot.gameObject.transform.GetChild(0).gameObject;

            float leftRarityWeight = rarityWeights[leftShape.GetComponent<Shape>().rarity];
            float rightRarityWeight = rarityWeights[rightShape.GetComponent<Shape>().rarity];

            float rarityNumber = Random.Range(0f, leftRarityWeight + rightRarityWeight);
            float shapeNumber = Random.Range(0f, 1f);

            string generatedRarity = "";
            string generatedShape = "";

            float rarityProbability = 0f;
            if (Mathf.Abs(rarityNumber - leftRarityWeight) < Mathf.Abs(rarityNumber - rightRarityWeight))
            {
                generatedRarity = leftShape.GetComponent<Shape>().rarity;
                rarityProbability = leftRarityWeight / (leftRarityWeight + rightRarityWeight);
            }
            else
            {
                generatedRarity = rightShape.GetComponent<Shape>().rarity;
                rarityProbability = rightRarityWeight / (leftRarityWeight + rightRarityWeight);
            }

            if(leftShape.GetComponent<Shape>().rarity.Equals(rightShape.GetComponent<Shape>().rarity))
            {
                rarityProbability = 1;
            }

            float mutationNumber = Random.Range(0f, 1f);
            if (mutationNumber < mutationChance)
            {
                int generatedRarityIndex = rarities.IndexOf(generatedRarity);
                if (generatedRarityIndex < rarities.Count - 1)
                {
                    generatedRarity = rarities[generatedRarityIndex + 1];
                    rarityProbability = rarityProbability * mutationChance;
                    MutationText.GetComponent<TMP_Text>().text = "mutation! ";
                }
            } else
            {
                MutationText.GetComponent<TMP_Text>().text = "";
            }

            if (shapeNumber >= 0.5f)
            {
                generatedShape = leftShape.GetComponent<Shape>().shape;
            }
            else
            {
                generatedShape = rightShape.GetComponent<Shape>().shape;
            }

            float shapeProbability = 0.5f;
            if (leftShape.GetComponent<Shape>().shape.Equals(rightShape.GetComponent<Shape>().shape))
            {
                shapeProbability = 1;
            }
            float probability = Mathf.Round(shapeProbability * rarityProbability * 1000f) * 0.001f;
            string shapeText;
            switch(generatedShape)
            {
                case ("Ico"):
                    shapeText = "Icosahedron";
                    break;
                case ("Dod"):
                    shapeText = "Dodecahedron";
                    break;
                default:
                    shapeText = generatedShape;
                    break;
            }
            outputText += "You bred a: " + generatedRarity + " " + shapeText + " probability: " + probability * 100f + "%";

            GenerateShape(new InventoryItem { rarity = generatedRarity, shape = generatedShape }, MergeSlot.transform, MergeSlot.InventoryIndex);
            OutputText.GetComponent<TMP_Text>().text = outputText;
        }
        else
        {
            Debug.Log("Merge slots not set up properly");
        }
    }
}
