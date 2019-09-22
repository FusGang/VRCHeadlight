using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VRCModLoader;

using VRCMenuUtils;
using VRChat;
using VRChat.UI.QuickMenuUI;

using UnityEngine;

namespace VRCHeadlight
{
    [VRCModInfo("VRCHeadlight", "0.1.0", "AtiLion")]
    internal class VRCHeadlight : VRCMod
    {
        void OnApplicationStart()
        {
            ModManager.StartCoroutine(Setup());
        }

        #region VRCHeadlight Coroutines
        private IEnumerator Setup()
        {
            // Wait for load
            yield return VRCMenuUtilsAPI.WaitForInit();

            VRCMenuUtilsAPI.AddQuickMenuButton("headlight", "Toggle Headlight", "Enables/Disables a headlight on your avatar", ToggleHeadlight);
        }
        #endregion
        #region VRCHeadlight Functions
        public void ToggleHeadlight()
        {
            // Avatar check
            if (VRCUPlayer.Instance.VRCPlayer.avatarGameObject == null || VRCUPlayer.Instance.Animator == null)
                return;

            // Get head
            Transform head = VRCUPlayer.Instance.Animator.GetBoneTransform(HumanBodyBones.Head);
            if (head == null)
                return;
            Light light = head.GetComponent<Light>();

            // Toggle headlight
            if(light == null)
            {
                // Add headlight
                light = head.gameObject.AddComponent<Light>();

                light.color = Color.white;
                light.type = LightType.Spot;
                light.shadows = LightShadows.Soft;
                light.intensity = 0.8f;

                VRCModLogger.Log("Added headlight to user!");
            }
            else
            {
                // Remove headlight
                GameObject.Destroy(light);
                VRCModLogger.Log("Removed headlight from user!");
            }
        }
        #endregion
    }
}
