﻿// Copyright (C) 2016-2023 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

using DG.Tweening;

namespace CCGKit
{
    public class Popup : MonoBehaviour
    {
        [HideInInspector] public BaseScreen parentScene;

        public void Close()
        {
            OnPopupClosedActions();

            if (parentScene != null)
            {
                parentScene.OnPopupClosed(this);
            }

            Destroy(gameObject);
        }

        public virtual void OnPopupClosedActions()
        {
            DOTween.KillAll();
        }
    }
}