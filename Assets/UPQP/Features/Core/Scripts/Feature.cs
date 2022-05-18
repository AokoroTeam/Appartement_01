using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPQP.Managers;

namespace UPQP.Features
{
    public abstract class Feature
    {
        public bool IsActive;
        public abstract string FeatureName { get; }
        public static event Action<Feature> OnNewFeatureIsInitiated;
        public event Action onFeatureStarts;
        public event Action onFeatureEnds;

        internal void Setup(LevelManager controller)
        {
            GenerateNeededContentOnSetup(controller);

            OnNewFeatureIsInitiated?.Invoke(this);
        }

        protected abstract void GenerateNeededContentOnSetup(LevelManager controller);
        
        public abstract void CleanContentOnDestroy(LevelManager controller);

        public void StartFeature()
        {
            OnFeatureStarts();
            onFeatureStarts?.Invoke();
        }
        
        public void EndFeature()
        {
            OnFeatureEnds();
            onFeatureEnds?.Invoke();
        }

        protected abstract void OnFeatureStarts();
        protected abstract void OnFeatureEnds();
    }
}