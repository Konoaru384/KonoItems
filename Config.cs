namespace Config{
    using Exiled.Loader;
    using Exiled.API.Interfaces;
    using Exiled.API.Features;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; }

    }}