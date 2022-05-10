using Aokoro.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPQP.Managers;

namespace UPQP.Features.SliceView
{

    public class SliceView : Feature<SliceView, SliceView_Data>
    {
        public override string FeatureName => "Vue coupée";
        public SliceView_Player Player { get; private set; }
        public SliceView_References References { get; private set; }
        public SliceView_UI UI { get; private set; }

        private GameObject P_PlayerComponent;
        private GameObject P_Manager;
        private GameObject P_UI;


        public SliceView(SliceView_Data data) : base(data)
        {
            this.P_PlayerComponent = data.PlayerComponent;
            this.P_Manager = data.Manager;
            this.P_UI = data.UI;
        }

        protected override void GenerateNeededContentOnSetup(LevelManager manager)
        {
            //Add Manager
            References = GameObject.FindObjectOfType<SliceView_References>(false);
            //Add player Component
            Player = GameObject.Instantiate(P_PlayerComponent, manager.Player.FeaturesRoot).GetComponent<SliceView_Player>();
            //Add UI
            UI = GameObject.Instantiate(P_UI, GameUIManager.MainUI.WindowsParent).GetComponent<SliceView_UI>();

            Player._Feature = this;
            UI._Feature = this;

            /*
            if (levelRoot == null)
                Debug.LogError("Please reference the root of the level");
            else
                levelRoot = levelRoot.transform;
            */

            GameNotifications.Instance.TriggerNotification(Data.title_Popup1, Data.description_Popup1, 10, 50);
        }

        public override void CleanContentOnDestroy(LevelManager controller)
        {
            GameObject.Destroy(Player);
            GameObject.Destroy(References.gameObject);
            GameObject.Destroy(UI.gameObject);
        }

        public override void EnableFeature()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            SliceView_HideGameObject.HideAll();
            UI.ShowCommands();

            Player.OnFeatureEnables();

            References.virtualCamera.enabled = true;
        }

        public override void DisableFeature()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            UI.HideCommands();
            Player.OnFeatureDisables();

            SliceView_HideGameObject.HideAll();
            References.virtualCamera.enabled = false;
        }

        public Bounds GetCurrentBounds()
        {
            Bounds bounds = new();
            var meshes = References.levelRoot.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshes.Length; i++)
            {
                if (i == 0)
                    bounds = meshes[i].bounds;
                else
                    bounds.Encapsulate(meshes[i].bounds);
            }

            return bounds;
        }
    }
}
