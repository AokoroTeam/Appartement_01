using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPQP.Managers;

namespace UPQP.Features.SliceView
{
    [CreateAssetMenu(fileName = "SliceView Data", menuName = "Aokoro/UPQP/SliceView/Data")]
    public class SliceView_Data : FeatureData<SliceView>
    {
        public GameObject PlayerComponent;
        public GameObject VirtualCamera;
        public GameObject UI;

        public override SliceView GenerateFeatureFromData(LevelManager controller)
        {
            SliceView sliceView = new SliceView(PlayerComponent, VirtualCamera, UI);
            sliceView.Setup(controller);

            return sliceView;
        }

        internal override bool CanGenerateFeature() => SliceView_SceneManager.Instance != null;
    }
}