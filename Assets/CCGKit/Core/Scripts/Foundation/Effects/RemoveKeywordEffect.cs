﻿// Copyright (C) 2016-2023 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

namespace CCGKit
{
    [CardTarget]
    public class RemoveKeywordEffect : CardEffect
    {
        [KeywordTypeField("Keyword")]
        [Order(3)]
        public int keywordTypeId;

        [KeywordValueField("Value")]
        [Order(4)]
        public int keywordValueId;

        public override void Resolve(GameState state, RuntimeCard card)
        {
            card.RemoveKeyword(keywordTypeId, keywordValueId);
        }
    }
}