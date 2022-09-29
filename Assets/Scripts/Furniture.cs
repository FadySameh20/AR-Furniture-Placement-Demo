using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Furniture")]

public class Furniture : ScriptableObject
{
    public string furnitureName;
    public float price;
    public List<MaterialCategory> materialCategory;
}
