using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;

namespace M2MqttUnity.Examples
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        public GameObject buttonPrefab = null;
        [SerializeField]
        public GridObjectCollection objectCollection;
        [SerializeField]
        public ScrollingObjectCollection scrollingCollection;
        public List<string> tempIdList;
        public static Dictionary<string, bool> buttonsState { get; set; } = new Dictionary<string, bool>();

        void Start()
        {
            tempIdList = new List<string>(M2MqttUnityTest.idList);
        }

        // Update is called once per frame
        void Update()
        {
            if (tempIdList.Count != M2MqttUnityTest.idList.Count)
            {
                //StartCoroutine(AddButton());
                AddButton();
                tempIdList = new List<string>(M2MqttUnityTest.idList);
            }            
        }
        public void AddButton()
        {
            for(int i = tempIdList.Count; i != M2MqttUnityTest.idList.Count; i++)
            {
                var button = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                button.transform.parent = GameObject.Find("Grid").transform;
                TextMeshPro mText = button.GetComponentInChildren<TextMeshPro>();
                mText.text = M2MqttUnityTest.idList[i];
                buttonsState.Add(M2MqttUnityTest.idList[i], false);
            }

            //yield return new WaitForEndOfFrame();
            objectCollection.UpdateCollection();
        }
    }
}
