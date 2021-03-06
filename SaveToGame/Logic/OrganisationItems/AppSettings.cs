﻿using Interfaces.OrganisationItems;
using SettingsManager;
using SharedData.Enums;

namespace SaveToGameWpf.Logic.OrganisationItems
{
    public class AppSettings : SettingsModel, IAppSettings
    {
        public virtual string Language { get; set; } = "EN";

        public virtual BackupType BackupType { get; set; } = BackupType.Titanium;

        public virtual string PopupMessage { get; set; } = "Modified by (Your name)";

        public virtual string Theme { get; set; } = "Dark";

        public virtual bool AlternativeSigning { get; set; }

        public virtual bool Notifications { get; set; } = true;

        public virtual int Version { get; set; } = GlobalVariables.LatestSettingsVersion;
    }
}
