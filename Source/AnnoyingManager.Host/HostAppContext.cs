using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core;
using AnnoyingManager.WindowsTrayAlert;
using AnnoyingManager.Core.Repository;
using Ninject;

namespace AnnoyingManager.Host
{
    internal class HostAppContext : ApplicationContext
    {
        private IKernel _container = new StandardKernel(new IocNinjectModule());
        public HostAppContext()
        {
            var manager = _container.Get<TheManager>();
            Application.ApplicationExit += manager.Application_ApplicationExit;
            manager.Initialize();
        }
    }
}
