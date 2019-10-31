using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System;

public class RandomFeature : MonoBehaviour
{
    public VoucherDatabase vdb;
    public int temp_total = 0;
    public List<ProbabilityCheck> voucher_probability = new List<ProbabilityCheck>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeRandomProbability()
    {
        ClearVariable();
        vdb.ClearList();
        vdb.GetData();
        int count = 0;
        foreach (VoucherEntity a in vdb.myList)
        {
            if (a._stock <= 0)
            {

            }
            else
            {
                ProbabilityCheck temp = new ProbabilityCheck();
                voucher_probability.Add(temp);
                voucher_probability[count].id = a._id;
                voucher_probability[count].min_prob = temp_total;
                temp_total += a._stock;
                voucher_probability[count].max_prob = temp_total;
                voucher_probability[count].type = a._type;
                count += 1;
            }
        }
    }

    public void CalculateProbability()
    {
        int rand = UnityEngine.Random.Range(0, temp_total);
        foreach (ProbabilityCheck s in voucher_probability)
        {
            if (rand >= s.min_prob && rand < s.max_prob)
            {
                vdb.ChosenVoucher = s.id;
                Debug.Log("Chosen Voucher is " + s.id + " & type is " + s.type);
            }
        }
    }

    public void ClearVariable()
    {
        voucher_probability = new List<ProbabilityCheck>();
        temp_total = 0;
    }

    public class ProbabilityCheck
    {
        public int id;
        public int min_prob;
        public int max_prob;
        public string type;
    }
}
