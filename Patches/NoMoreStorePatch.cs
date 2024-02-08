using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppAssets.Scripts.UI.Panels.PnlDLC;
using UnityEngine;
using UnityEngine.UI;

namespace ShopLift.Patches
{
    internal class NoMoreStorePatch
    {
        [HarmonyPatch(typeof(PnlStage), nameof(PnlStage.Awake))]
        internal class PnlStagePatch
        {
            private static void Postfix(PnlDlcTagControl __instance)
            {
                // Hide the store button
                GameObject.Find("UI/Standerd/PnlStage/StageUi/Tag&Difficulty&Dlc/BtnDlc").SetActive(false);

                var bigBarBehindEverythingObj =
                    GameObject.Find("UI/Standerd/PnlStage/StageUi/Tag&Difficulty&Dlc/ImgAlbumBase");

                var tagObj = GameObject.Find("UI/Standerd/PnlStage/StageUi/Tag&Difficulty&Dlc/BtnOwn");

                // Center the BtnOwn object
                var xValue = bigBarBehindEverythingObj.transform.localPosition.x;
                var vector = tagObj.transform.localPosition;
                tagObj.transform.localPosition = new Vector3(xValue, vector.y, vector.z);

                // Set the big bar behind everything to be smaller than it usually is
                var sizeBigBar = bigBarBehindEverythingObj.transform.GetComponent<RectTransform>().sizeDelta;
                bigBarBehindEverythingObj.transform.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(sizeBigBar.x * 0.6f, sizeBigBar.y);

                // Set the color of the new bar
                tagObj.GetComponentInChildren<Image>().sprite = bigBarBehindEverythingObj.GetComponent<Image>().sprite;
                tagObj.GetComponentInChildren<Image>().color = new Color32(156,85,255,255);

                // Create a new trapezoid to mimic the previous UI
                var newObject = bigBarBehindEverythingObj.FastInstantiate(bigBarBehindEverythingObj.transform);
                newObject.GetComponent<Image>().color = new Color32(129, 39, 255, 255);
                newObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -8, 0);
                newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(696, 90);

            }
        }
    }
}