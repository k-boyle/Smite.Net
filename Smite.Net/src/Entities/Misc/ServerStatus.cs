﻿using System;

namespace Smite.Net
{
    public sealed class ServerStatus : BaseEntity
    {
        private readonly ServerStatusModel _model;

        /// <summary>
        /// If the servers have limited access.
        /// </summary>
        public bool IsLimitedAccess => _model.limited_access;

        /// <summary>
        /// The current status of the servers.
        /// </summary>
        public APIStatus Status
        {
            get
            {
                switch(_model.status)
                {
                    case "UP":
                        return APIStatus.Up;

                    case "DOWN":
                        return APIStatus.Down;

                    default:
                        return APIStatus.Unknown;
                }
            }
        }

        private DateTimeOffset _entryTime;

        /// <summary>
        /// The time of when this entry was made.
        /// </summary>
        public DateTimeOffset EntryTime => _entryTime == default
            ? _entryTime = DateTimeOffset.Parse(_model.entry_datetime)
            : _entryTime;

        /// <summary>
        /// The version of Smite that the servers are running.
        /// </summary>
        public string Version => _model.version;

        internal ServerStatus(SmiteClient client, ServerStatusModel model) : base(client)
        {
            _model = model;
        }
    }
}
