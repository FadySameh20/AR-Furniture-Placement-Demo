using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TotalPriceCalculation : MonoBehaviour
{
    [SerializeField]
    private Text totalPriceText;

    private List<float> furniturePrices = new List<float>();

    private void Awake()
    {
        totalPriceText.text = "Total Price:\nEGP 0";
    }

    public void AddToPriceList(float price)  // On instantiating new furniture, add its price to the list
    {
        furniturePrices.Add(price);
        CalculateTotalPrice();
    }

    public void RemoveFromPriceList(float price) // On deleting furniture, remove its price from the list
    {
        furniturePrices.Remove(price);
        CalculateTotalPrice();
    }

    public void CalculateTotalPrice()  // Calculate the sum of prices in the list
    {
        totalPriceText.text = "TotalPrice:\nEGP " + furniturePrices.Sum().ToString("0");
    }
}
