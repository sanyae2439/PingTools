using System.Collections.Generic;
using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Commands;
using UnityEngine;
using UnityEngine.Networking;

namespace PingTools
{
    [PluginDetails(
        author = "sanyae2439",
        name = "PingTools",
        description = "Simply Ping Commands.",
        id = "sanyae2439.pingtools",
        version = "1.0",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
        )
    ]
    public class PingTools : Plugin
    {
        public override void OnDisable()
        {
            this.Info($"{this.Details.name} was Disabled.");
        }

        public override void OnEnable()
        {
            this.Info($"{this.Details.name} was Enabled.");
        }

        public override void Register()
        {
            AddCommand("ping", new PingCommand(this));
        }
    }

    public class PingCommand : ICommandHandler
    {
        private readonly PingTools plugin;

        public PingCommand(PingTools plugin)
        {
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            return "Ping to clients.";
        }

        public string GetUsage()
        {
            return "PING";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            List<string> pinglist = new List<string>();
            byte b;

            foreach(Player player in plugin.Server.GetPlayers())
            {
                NetworkConnection conn = (player.GetGameObject() as GameObject).GetComponent<NicknameSync>().connectionToClient;
                pinglist.Add($"Name: {player.Name} IP: {player.IpAddress} Ping: {NetworkTransport.GetCurrentRTT(conn.hostId, conn.connectionId, out b)}ms");
            }

            return pinglist.ToArray();
        }
    }
}
