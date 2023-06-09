// Copyright (C) 2016-2023 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using Mirror;

namespace CCGKit
{
    /// <summary>
    ///  This server handler is responsible for managing the network aspects of a combat between
    ///  two cards.
    ///
    /// Combat follow this sequence:
    ///     - A player selects a card that is eligible as an attacker during his turn and sends
    ///       this information to the server.
    ///     - A player then selects a target player or card as the attack's target and sends this
    ///       information to the server.
    ///     - The server then proceeds with resolving the attack authoritatively and updating all
    ///       the clients afterwards.
    ///
    /// This process is currently very much suited to the traditional way of resolving combats in
    /// CCGs (the attacker's attack value is substracted from the attacked's defense value, and
    /// vice versa). This is something we would like to expand upon in future updates to allow for
    /// more varied/complex mechanics.
    /// </summary>
    public class CombatHandler : ServerHandler
    {
        public CombatHandler(Server server) : base(server)
        {
        }

        public override void RegisterNetworkHandlers()
        {
            base.RegisterNetworkHandlers();
            NetworkServer.RegisterHandler<FightPlayerMessage>(OnFightPlayer);
            NetworkServer.RegisterHandler<FightCreatureMessage>(OnFightCreature);
        }

        public override void UnregisterNetworkHandlers()
        {
            base.UnregisterNetworkHandlers();
            NetworkServer.UnregisterHandler<FightCreatureMessage>();
            NetworkServer.UnregisterHandler<FightPlayerMessage>();
        }

        public virtual void OnFightPlayer(NetworkConnection conn, FightPlayerMessage msg)
        {
            // Only the current player can fight.
            if (conn.connectionId != server.gameState.currentPlayer.connectionId)
            {
                return;
            }

            var playerAttackedMsg = new PlayerAttackedMessage();
            playerAttackedMsg.attackingPlayerNetId = msg.attackingPlayerNetId;
            playerAttackedMsg.attackingCardInstanceId = msg.cardInstanceId;
            server.SafeSendToClient(server.gameState.currentOpponent, playerAttackedMsg);

            server.effectSolver.FightPlayer(msg.attackingPlayerNetId, msg.cardInstanceId);
        }

        public virtual void OnFightCreature(NetworkConnection conn, FightCreatureMessage msg)
        {
            // Only the current player can fight.
            if (conn.connectionId != server.gameState.currentPlayer.connectionId)
            {
                return;
            }

            var creatureAttackedMsg = new CreatureAttackedMessage();
            creatureAttackedMsg.attackingPlayerNetId = msg.attackingPlayerNetId;
            creatureAttackedMsg.attackingCardInstanceId = msg.attackingCardInstanceId;
            creatureAttackedMsg.attackedCardInstanceId = msg.attackedCardInstanceId;
            server.SafeSendToClient(server.gameState.currentOpponent, creatureAttackedMsg);

            var attackingCard = server.gameState.currentPlayer.namedZones["Board"].cards
                .Find(x => x.instanceId == msg.attackingCardInstanceId);
            var attackedCard = server.gameState.currentOpponent.namedZones["Board"].cards
                .Find(x => x.instanceId == msg.attackedCardInstanceId);
            if (attackingCard != null && attackedCard != null)
            {
                server.effectSolver.FightCreature(msg.attackingPlayerNetId, attackingCard, attackedCard);
            }
        }
    }
}