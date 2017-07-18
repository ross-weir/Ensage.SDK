// <copyright file="ServiceManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    public abstract class ServiceManager<TService> : ControllableService, IServiceManager<TService>
        where TService : class, IControllable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private TService active;

        protected ServiceManager(bool activateOnCreation = false)
            : base(activateOnCreation)
        {
        }

        public TService Active
        {
            get
            {
                if (this.active == null)
                {
                    try
                    {
                        this.active = this.GetSelection();

                        Log.Debug($"Activate Service {this.active}");
                        this.active?.Activate();
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                return this.active;
            }

            set
            {
                if (EqualityComparer<TService>.Default.Equals(this.active, value))
                {
                    return;
                }

                if (this.active != null)
                {
                    try
                    {
                        Log.Debug($"Deactivate Service {this.active}");
                        this.active.Deactivate();
                        this.active = null;
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                if (value == null)
                {
                    return;
                }

                try
                {
                    Log.Debug($"Activate Service {value}");
                    this.active = value;
                    this.active?.Activate();
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }

        public abstract IEnumerable<Lazy<TService>> Services { get; protected set; }

        protected abstract TService GetSelection();
    }

    public abstract class ServiceManager<TService, TServiceMetadata> : ControllableService, IServiceManager<TService, TServiceMetadata>
        where TService : class, IControllable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private TService active;

        protected ServiceManager(bool activateOnCreation = false)
            : base(activateOnCreation)
        {
        }

        public TService Active
        {
            get
            {
                if (this.active == null)
                {
                    try
                    {
                        this.active = this.GetSelection();

                        Log.Debug($"Activate Service {this.active}");
                        this.active?.Activate();
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                return this.active;
            }

            set
            {
                if (EqualityComparer<TService>.Default.Equals(this.active, value))
                {
                    return;
                }

                if (this.active != null)
                {
                    try
                    {
                        Log.Debug($"Deactivate Service {this.active}");
                        this.active.Deactivate();
                        this.active = null;
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                if (value == null)
                {
                    return;
                }

                try
                {
                    Log.Debug($"Activate Service {value}");
                    this.active = value;
                    this.active?.Activate();
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }

        public abstract IEnumerable<Lazy<TService, TServiceMetadata>> Services { get; protected set; }

        [CanBeNull]
        protected abstract TService GetSelection();
    }
}