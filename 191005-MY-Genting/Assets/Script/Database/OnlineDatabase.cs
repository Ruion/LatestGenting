using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataBank;
using System;
using UnityEngine.UI;

public class OnlineDatabase : MonoBehaviour
{
    //public const string userURL = "http://191005-my-genting.unicom-interactive-digital.com/public/api/submit-player-data";
    //public const string dupreferURL = "http://191005-my-genting.unicom-interactive-digital.com/public/api/submit-reference-data";
    //public const string voucherdistributionURL = "http://191005-my-genting.unicom-interactive-digital.com/public/api/submit-voucher_distribution-data";
    public const string CheckConnectionURL = "https://www.google.com/";

    public const string userURL = "https://191005-my-genting.unicom-interactive-digital.com/public/api/submit-player-data";
    public const string dupreferURL = "https://191005-my-genting.unicom-interactive-digital.com/public/api/submit-reference-data";
    public const string voucherdistributionURL = "https://191005-my-genting.unicom-interactive-digital.com/public/api/submit-voucher_distribution-data";

    public UserDatabase userdb;
    public DupReferDatabase duprdb;
    public VoucherDistributionDatabase vddb;
    public VoucherDatabase vdb;

    public GameObject greenbar;
    public GameObject redbar;
    public Text greencount_text;
    public Text redcount_text;

    int user_greencount;
    int user_redcount;
    int duprefer_greencount;
    int duprefer_redcount;
    int voucherdis_greencount;
    int voucherdis_redcount;

    // Start is called before the first frame update
    void Start()
    {
        //SyncUserData();
        //SynchVoucherDistributionData();
        StartCoroutine(CheckInternet());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator CheckInternet()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(CheckConnectionURL))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Connection Error : " + www.error);
            }
            else
            {
                Debug.Log("Connection Success!");
                user_greencount = 0;
                user_redcount = 0;
                duprefer_greencount = 0;
                duprefer_redcount = 0;
                voucherdis_greencount = 0;
                voucherdis_redcount = 0;
                SyncUserData();
            }
        }
    }

    #region Sync User Data Online

    [ContextMenu("Sync")]
    public void SyncUserData()
    {
        userdb.ClearList();
        userdb.GetDataByOnlineStatus();
        StartCoroutine(UserData());
        // StartCoroutine(TestUserData());
    }

    IEnumerator UserData()
    {
        foreach (UserEntity a in userdb.myList)
        {
            DateTime temp = DateTime.Parse(a._dateCreated);
            Debug.Log("Parse Datetime : " + temp);
            string datetime = temp.ToString("yyyy-MM-dd HH:mm:ss");
            Debug.Log("Change to string Datetime : " + datetime);
            /*string newURL = userURL
                + "/name/" + a._name
                + "/email/" + a._email
                + "/contact/" + a._phone
                + "/reference_code/" + a._referencecode
                + "/card_number/" + a._memberid
                + "/isNewMember/" + a._status
                + "/register_datetime/" + datetime;*/


            WWWForm form = new WWWForm();
            form.AddField("name", a._name);
            form.AddField("email", a._email);
            form.AddField("contact", a._phone);
            form.AddField("reference_code", a._referencecode);
            form.AddField("card_number", a._memberid);
            form.AddField("isNewMember", a._status);
            form.AddField("register_datetime", datetime);
            Debug.LogError("User Data : " + a._name  + " | " + a._email + " | " + a._phone + " | " + a._referencecode + " | " + a._memberid + " | " + a._status + " | " + datetime);
            // newURL = UnityWebRequest.EscapeURL(newURL);
            //Debug.Log("New User URL = " + newURL);
            //  using (UnityWebRequest www = UnityWebRequest.Post(newURL, form))
            //  using (UnityWebRequest www = UnityWebRequest.Get(newURL)
            using (UnityWebRequest www = UnityWebRequest.Post(userURL, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("User Sync Error : " + www.error);
                    //break;
                    user_redcount += 1;
                    redcount_text.text = user_redcount + " | " + duprefer_redcount + " | " + voucherdis_redcount;
                    redbar.SetActive(true);
                }
                else
                {
                    Debug.Log("User Form upload complete! Response = " + www.downloadHandler.text);
                    var serverresponse = JsonUtility.FromJson<serverresponse>(www.downloadHandler.text);
                    string temp_msg = serverresponse.result;
                    if (temp_msg == "Success")
                    {
                        userdb.UpdateDataOnline(a);
                        Debug.LogWarning("User Data has been successfully Updated!!");
                        user_greencount += 1;
                        greencount_text.text = user_greencount + " | " + duprefer_greencount + " | " + voucherdis_greencount;
                        greenbar.SetActive(true);
                    }                    
                }
            }
        }
        SyncDupReferData();
        SynchVoucherDistributionData();
    }

    #endregion

    #region Sync DoubleUp Reference Data
    public void SyncDupReferData()
    {
        duprdb.ClearList();
        duprdb.GetDataByOnlineStatus();
        StartCoroutine(DupReferData());
    }
    IEnumerator DupReferData()
    {
        foreach (DupReferEntity s in duprdb.myList)
        {
            /*string newURL = dupreferURL
                + "/referer_contact/" + s._userphone
                + "/referral_name/" + s._name
                + "/referral_email/" + s._email
                + "/referral_contact/" + s._phone;
            Debug.Log("New Reference Data URL = " + newURL);*/

            WWWForm form = new WWWForm();
            form.AddField("referer_contact", s._userphone);
            form.AddField("referral_name", s._name);
            form.AddField("referral_email", s._email);
            form.AddField("referral_contact", s._phone);
            Debug.LogError("Double Up Reference Data : " + s._userphone + " | " + s._name + " | " + s._email + " | " + s._phone);
           // newURL = UnityWebRequest.EscapeURL(newURL);
            //using (UnityWebRequest www = UnityWebRequest.Get(newURL))
            using (UnityWebRequest www = UnityWebRequest.Post(dupreferURL, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("DoubleUp Reference Sync Error : " + www.error);
                    //break;.
                    duprefer_redcount += 1;
                    redcount_text.text = user_redcount + " | " + duprefer_redcount + " | " + voucherdis_redcount;
                    redbar.SetActive(true);
                }
                else
                {
                    Debug.Log("DoubleUp Reference Form upload complete! Response = " + www.downloadHandler.text);
                    var serverresponse = JsonUtility.FromJson<serverresponse>(www.downloadHandler.text);
                    string temp_msg = serverresponse.result;
                    if (temp_msg == "Success")
                    {
                        duprdb.UpdateOnlineStatusData(s);
                        Debug.LogWarning("Reference Data has been successfully updated!");
                        duprefer_greencount += 1;
                        greencount_text.text = user_greencount + " | " + duprefer_greencount + " | " + voucherdis_greencount;
                        greenbar.SetActive(true);
                    }
                }
            }
        }
    }
    #endregion

    #region Sync Voucher Distribution Data
    public void SynchVoucherDistributionData()
    {
        vddb.ClearList();
        vddb.GetDataByStatus();
        vdb.ClearList();
        vdb.GetData();
        StartCoroutine(VoucherDistributionData());
    }
    IEnumerator VoucherDistributionData()
    {
        foreach (VoucherDistributionEntity d in vddb.myList)
        {
            string vouchername = "0";
            foreach (VoucherEntity s in vdb.myList)
            {
                if (d._voucherID == s._id)
                {
                    vouchername = s._type;
                }
            }
            /*string newURL = voucherdistributionURL
                + "/player_contact/" + d._userPhone
                + "/voucher_code/" + vouchername;
            Debug.Log("Voucher Distribution New URL = " + newURL);*/

            WWWForm form = new WWWForm();
            form.AddField("player_contact", d._userPhone);
            form.AddField("voucher_code", vouchername);
            Debug.LogError("Voucher Distribution Data : " + d._userPhone + " | " + vouchername);
            //newURL = UnityWebRequest.EscapeURL(newURL);
            //using (UnityWebRequest www = UnityWebRequest.Get(newURL))
            using (UnityWebRequest www = UnityWebRequest.Post(voucherdistributionURL, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("Voucher Distribution Sync Error : " + www.error);
                    //break;
                    voucherdis_redcount += 1;
                    redcount_text.text = user_redcount + " | " + duprefer_redcount + " | " + voucherdis_redcount;
                    redbar.SetActive(true);
                }
                else
                {
                    Debug.Log("User Form upload complete! Response = " + www.downloadHandler.text);
                    var serverresponse = JsonUtility.FromJson<serverresponse>(www.downloadHandler.text);
                    string temp_msg = serverresponse.result;
                    if (temp_msg == "Success")
                    {
                        vddb.UpdateOnlineStatusData(d);
                        Debug.LogWarning("Voucher Distribution Data has been successfully updated!");
                        voucherdis_greencount += 1;
                        greencount_text.text = user_greencount + " | " + duprefer_greencount + " | " + voucherdis_greencount;
                        greenbar.SetActive(true);
                    }
                }
            }
        }
        greenbar.GetComponent<StatusBar>().Finish();
        redbar.GetComponent<StatusBar>().Finish();
    }
    #endregion

    [System.Serializable]
    public class serverresponse
    {
        public string result;
    }
}
