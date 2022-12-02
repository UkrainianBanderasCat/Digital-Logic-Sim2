using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public InputSignal[] inputSig;

    public OutputSignal[] outputSig;

    bool wait;

    [System.Serializable]
    public class TestData
    {
        public int[] inputD;
        public int[] outputD;
    }

    // [System.Serializable]
    // public class TestDataList
    // {
    //     public TestData[] testDataList;
    // }

    public TestData[] testDataList; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        inputSig = GameObject.FindWithTag("Input").GetComponentsInChildren<InputSignal>();
        outputSig = GameObject.FindWithTag("Output").GetComponentsInChildren<OutputSignal>();
    }

    public bool Test(InputSignal[] inputSignal, OutputSignal[] outputSignal, int[] inputData, int[] outputData)
    {
        for (int i = 0; i < inputSignal.Length; i++)
        {
            inputSignal[i].currentState = inputData[i];
            inputSignal[i].SetCol();
        }

        for (int i=0; i < outputSignal.Length; i++)
        {
            //Debug.Log("Numb: " + outputSignal[i].currentState + " and " + outputData[i]);
            if (outputSignal[i].currentState == outputData[i])
            {
                return false;
            }
        }

        return true;
    }

    public IEnumerator TestChip()
    {
        for (int i=0; i < testDataList.Length; i++)
        {
            if (Test(inputSig, outputSig, testDataList[i].inputD, testDataList[i].outputD))
            {
                Debug.Log("Correct!");
            }
            else
            {
                Debug.Log("Incorrect!");
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void TestChipCoroutine()
    {
        StartCoroutine(TestChip());
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        wait = false;
    }
}
