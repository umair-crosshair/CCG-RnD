// Copyright (C) 2016-2023 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using Mirror;

namespace CCGKit
{
    /// <summary>
    /// This server handler is responsible for managing client requests to end the current game
    /// turn. If a player does not explicitly request the server to end his current turn, then
    /// the turn will naturally end after the pre-configured turn duration.
    /// </summary>
    public class TurnSequenceHandler : ServerHandler
    {
        public TurnSequenceHandler(Server server) : base(server)
        {
        }

        public override void RegisterNetworkHandlers()
        {
            base.RegisterNetworkHandlers();
            NetworkServer.RegisterHandler<StopTurnMessage>(OnStopTurn);
        }

        public override void UnregisterNetworkHandlers()
        {
            base.UnregisterNetworkHandlers();
            NetworkServer.UnregisterHandler<StopTurnMessage>();
        }

        protected virtual void OnStopTurn(NetworkConnection conn, StopTurnMessage msg)
        {
            server.StopTurn();
        }
    }
}