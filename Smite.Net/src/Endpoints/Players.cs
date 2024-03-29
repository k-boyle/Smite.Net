﻿using Smite.Net.ReadOnlyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smite.Net
{
    // TODO /getplayeridbyportaluserid
    public partial class SmiteClient
    {
        /// <summary>
        /// Gets the player id corresponding to the specified name.
        /// </summary>
        /// <param name="name">The name of the player to search for.</param>
        /// <returns>A collection of found players.</returns>
        public async Task<IReadOnlyCollection<PlayerNameSearchResult>> GetPlayerIdFromNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var response = await GetCollectionAsync<PlayerIdByNameModel>(APIPlatform.PC, "getplayeridbyname", name)
                .ConfigureAwait(false);

            var results = response.Select(x => new PlayerNameSearchResult(this, x));

            return new ReadOnlyCollection<PlayerNameSearchResult>(results, () => response.Length);
        }

        /// <summary>
        /// Gets a player using their name.
        /// </summary>
        /// <param name="id">The id of the player.</param>
        /// <param name="portal">The portal to get this player from.</param>
        /// <returns>The player corresponding to the given id.</returns>
        public async Task<IReadOnlyCollection<Player>> GetPlayerAsync(string name, Portal portal)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var response = await GetCollectionAsync<PlayerModel>(APIPlatform.PC, "getplayer", name, (int)portal)
                .ConfigureAwait(false);

            var result = response.Select(x => new Player(this, x));

            return new ReadOnlyCollection<Player>(result, () => response.Length);
        }

        /// <summary>
        /// Gets the player ids corresponding to the specified name.
        /// </summary>
        /// <param name="gamertag">The name of the player.</param>
        /// <param name="portal">The portal to use.</param>
        /// <returns>A collection of found players.</returns>
        public async Task<IReadOnlyCollection<PlayerNameSearchResult>> GetPlayerIdsFromGamertagAsync(string gamertag, Portal portal)
        {
            if (string.IsNullOrWhiteSpace(gamertag))
                throw new ArgumentNullException(nameof(gamertag));

            var response = await GetCollectionAsync<PlayerIdByNameModel>(APIPlatform.PC, 
                "getplayeridsbygamertag", (int)portal, gamertag)
                .ConfigureAwait(false);

            var results = response.Select(x => new PlayerNameSearchResult(this, x));

            return new ReadOnlyCollection<PlayerNameSearchResult>(results, () => response.Length);
        }

        /// <summary>
        /// Gets the friends of the specified player.
        /// </summary>
        /// <param name="playerId">The player that you want to get the friends for.</param>
        /// <returns>A collection of friends of this player.</returns>
        public async Task<IReadOnlyCollection<Friend>> GetFriendsAsync(int playerId)
        {
            if (playerId < 0)
                throw new ArgumentOutOfRangeException(nameof(playerId));

            var response = await GetCollectionAsync<FriendModel>(APIPlatform.PC,
                "getfriends", playerId)
                .ConfigureAwait(false);

            var results = response.Select(x => new Friend(this, x));

            return new ReadOnlyCollection<Friend>(results, () => response.Length);
        }

        /// <summary>
        /// Gets the God stats for the specified player.
        /// </summary>
        /// <param name="playerId">The id of the player.</param>
        /// <returns>A collection of God stats.</returns>
        public async Task<IReadOnlyCollection<GodStats>> GetGodStatsAsync(int playerId)
        {
            if (playerId < 0)
                throw new ArgumentOutOfRangeException(nameof(playerId));

            var response = await GetCollectionAsync<GodStatsModel>(APIPlatform.PC,
                "getgodranks", playerId)
                .ConfigureAwait(false);

            var results = response.Select(x => new GodStats(this, x));

            return new ReadOnlyCollection<GodStats>(results, () => response.Length);
        }

        /// <summary>
        /// Gets the accolades for the specified player.
        /// </summary>
        /// <param name="playerId">The id of the player.</param>
        /// <returns>The players accolades.</returns>
        public async Task<PlayerAccolades> GetPlayerAccoladesAsync(int playerId)
        {
            if (playerId < 0)
                throw new ArgumentOutOfRangeException(nameof(playerId));

            var response = await GetAsync<PlayerAccoladesModel>(APIPlatform.PC,
                "getplayerachievements", playerId)
                .ConfigureAwait(false);

            return new PlayerAccolades(this, response);
        }

        /// <summary>
        /// Gets the current status of the specified player.
        /// </summary>
        /// <param name="playerId">The id of the player.</param>
        /// <returns>A collection of player statuses.</returns>
        public async Task<IReadOnlyCollection<PlayerCurrentStatus>> GetStatusAsync(int playerId)
        {
            if (playerId < 0)
                throw new ArgumentOutOfRangeException(nameof(playerId));

            var response = await GetCollectionAsync<PlayerStatusModel>(APIPlatform.PC,
                "getplayerstatus", playerId)
                .ConfigureAwait(false);

            var results = response.Select(x => new PlayerCurrentStatus(this, x));

            return new ReadOnlyCollection<PlayerCurrentStatus>(results, () => response.Length);
        }
    }
}
