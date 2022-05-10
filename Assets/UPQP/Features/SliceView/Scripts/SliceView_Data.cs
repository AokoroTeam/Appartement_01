using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UPQP.Managers;

namespace UPQP.Features.SliceView
{
    [CreateAssetMenu(fileName = "SliceView Data", menuName = "Aokoro/UPQP/SliceView/Data")]
    public class SliceView_Data : FeatureData<SliceView>
    {
        public GameObject PlayerComponent;
        public GameObject Manager;
        public GameObject UI;

        [BoxGroup("Popup 1")]
        public string title_Popup1 = "Nouvelle fonctionnalité !";
        [BoxGroup("Popup 1")]
        public string description_Popup1 = "Appuyez sur la touche 1 pour activer la vue découpée et observer l'environnement dans sa globalité.";

        public override SliceView GenerateFeatureFromData(LevelManager controller)
        {
            SliceView sliceView = new SliceView(this);
            sliceView.Setup(controller);

            return sliceView;
        }
    }
}