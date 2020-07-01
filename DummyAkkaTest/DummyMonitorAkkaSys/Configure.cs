using Akka.Actor;
using Akka.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyMonitorAkkaSys
{
    public static class Configure
    {
        #region 專案整個系統的Akka Manager 建議寫成單例
        //public static ActorSystem actorSystem;

        // Local Main Sys
        public static readonly string AkaSysName = "MonoitorAkkaSys";
        public static readonly string AkaSysPort = "8201";

        // Local Sub Sys 
        public static readonly string TargetSysName = "SubSystemAkkaSys";
        public static readonly string TargetSysPort = "8202";
        public static readonly string TargetSysCoinActor = "CoinActor";

        // Outer Sys IP (TCP/IP Protocal)
        public static readonly string OutSysIp = "127.0.0.1";
        public static readonly int OutSysPort = 7788;
        #endregion

        // DI
        public static ServiceProvider provider;
        public static void DIManagerSetting()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<MonitorActor>();
            provider = collection.BuildServiceProvider();
        }

        /// <summary>
        ///     建立 config
        /// </summary>
        /// <param name="port"> 本地端接口埠號 </param>
        public static Config AkkaConfig(string port)
        {
            var strConfig = @"
                akka
                {
                    #loglevel = DEBUG
                    #loggers = [""Akka.Logger.NLog.NLogLogger, Akka.Logger.NLog""]
                    actor
                    {
                        provider = remote
                        debug
                        {
                            receive = on      # log any received message
                            autoreceive = on  # log automatically received messages, e.g. PoisonPill
                            lifecycle = on    # log actor lifecycle changes
                            event-stream = on # log subscription changes for Akka.NET event stream
                            unhandled = on    # log unhandled messages sent to actors
                        }
                    }
                    remote 
                    {
                        dot-netty.tcp 
                        {
                            port = {port}
                            hostname = 0.0.0.0
                            public-hostname = 127.0.0.1
                        }
                    }
                }";
            strConfig = strConfig.Replace("{port}", port);

            return ConfigurationFactory.ParseString(strConfig);
        }

    }
}
