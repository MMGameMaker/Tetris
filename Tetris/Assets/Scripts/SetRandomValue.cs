using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SetRandomValue : MonoBehaviour
{
    [System.Serializable]
    public struct ValueRate
    {
        public int value;
        public float rate;
    }

    public ValueRate[] valueRates;

    List<ValueRate> valueList;

    List<ValueRate> sortedRateList = new List<ValueRate>();

    private float rateSum;

    int result;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        valueList = new List<ValueRate>();
        for (int i = 0; i < valueRates.Length; i++)
        {
            valueList.Add(valueRates[i]);

            rateSum += valueRates[i].rate;
        }

        sortedRateList = valueList.OrderByDescending(V => V.rate).ToList<ValueRate>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRandomPieceValue()
    {
        float randomNum = Random.Range(0, rateSum);
        
        foreach(ValueRate value in sortedRateList)
        {
            randomNum -= value.rate;
            
            if (randomNum < 0)
            {
                result = value.value;
                break;
            }
        }
        return result;
    }

}
