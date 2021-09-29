using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotDLL
{
    /// <summary>
    /// The l f_ server info.
    /// </summary>
    public class LF_ServerInfo
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Gets or sets the hostname.
        /// </summary>
        public string Hostname { get; set; }
        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        public string Map { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether is_online.
        /// </summary>
        public bool Is_online { get; set; }
        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public int Players { get; set; }
        /// <summary>
        /// Gets or sets the maxplayers.
        /// </summary>
        public int Maxplayers { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Gets or sets the uptime.
        /// </summary>
        public int Uptime { get; set; }
        /// <summary>
        /// Gets or sets the last_check.
        /// </summary>
        public DateTime Last_check { get; set; }
        /// <summary>
        /// Gets or sets the last_online.
        /// </summary>
        public DateTime Last_online { get; set; }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the l f_ uri.
        /// </summary>
        public Uri LF_Uri { get; set; }
        /// <summary>
        /// Gets or sets the l f_ stat uri.
        /// </summary>
        public Uri LF_StatUri { get; set; }
        /// <summary>
        /// Gets or sets the q c_ stat uri.
        /// </summary>
        public Uri QC_StatUri { get; set; }
        /// <summary>
        /// Gets or sets the l f_ header img u ri.
        /// </summary>
        public Uri LF_HeaderImgURi { get; set; }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <param name="db">The db.</param>
        /// <returns>A list of LF_ServerInfos.</returns>
        public static List<LF_ServerInfo> ReadAll(string db)
        {
            return DB_LF_ServerInfo.ReadAll(db);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LF_ServerInfo"/> class.
        /// </summary>
        public LF_ServerInfo()
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LF_ServerInfo"/> class.
        /// </summary>
        /// <param name="Key">The key.</param>
        public LF_ServerInfo(string Key)
        {
             DB_LF_ServerInfo.Read(this, Key);
        }
        /// <summary>
        /// Override of ToString.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Id,8} ██ {Name,-15} ██ {Address,20}:{Port,-5} ██ {Hostname,-30} ██ {Map,-20} ██ {Is_online,-6} ██ {Players,3}/{Maxplayers,-5} ██ {Version,10} ██ {Uptime,3}% ██  {Last_check,-19}  ██  {Last_online,-18}";
        }
        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="notification">If true, notification.</param>
        public static void DeleteAll(bool notification)
        {
            DB_LF_ServerInfo.DeleteAll(notification);
        }
        /// <summary>
        /// Creates the table listforge server info live.
        /// </summary>
        /// <param name="notification">If true, notification.</param>
        public static void CreateTable_LF_ServerInfoLive(bool notification)
        {
            DB_LF_ServerInfo.CreateTable_LF_ServerInfoLive(notification);
        }
    }
}
