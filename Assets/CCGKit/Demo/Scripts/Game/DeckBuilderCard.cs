// Copyright (C) 2016-2023 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace CCGKit
{
    public class DeckBuilderCard : MonoBehaviour
    {
        public DeckBuilderScreen scene;
        public Card card;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && scene.currentPopup == null)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    scene.AddCardToDeck(card);
                }
            }
        }
    }
}