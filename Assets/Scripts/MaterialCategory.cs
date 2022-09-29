using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Category", menuName = "Category")] 
public class MaterialCategory : ScriptableObject
{
    public string categoryName;
    public List<Material> materials;
}
