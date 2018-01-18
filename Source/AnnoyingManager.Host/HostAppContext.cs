using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core;
using AnnoyingManager.WindowsTrayAlert;
using AnnoyingManager.Core.Repository;

namespace AnnoyingManager.Host
{
    internal class HostAppContext : ApplicationContext
    {
        public HostAppContext()
        {
            TheManager manager = new TheManager(new TxtRepository(), new FormAlert(), new XmlConfigRepository());
            Application.ApplicationExit += manager.Application_ApplicationExit;
            manager.Initialize();
        }
    }
}
