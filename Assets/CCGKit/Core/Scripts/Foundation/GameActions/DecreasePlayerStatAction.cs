// Copyright (C) 2016-2023 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

namespace CCGKit
{
    /// <summary>
    /// Game action to decrease a player stat.
    /// </summary>
    public class DecreasePlayerStatAction : GameAction
    {
        /// <summary>
        /// The stat of this action.
        /// </summary>
        [PlayerStatField("Stat")]
        public int statId;

        /// <summary>
        /// The value of this action.
        /// </summary>
        [ValueField("Value")]
        public Value value;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DecreasePlayerStatAction() : base("Decrease player stat")
        {
        }

        /// <summary>
        /// Resolves this action.
        /// </summary>
        /// <param name="state">The state of the game.</param>
        /// <param name="player">The player on which to resolve this action.</param>
        public override void Resolve(GameState state, PlayerInfo player)
        {
            player.stats[statId].baseValue -= value.GetValue(state, player);
        }
    }
}
