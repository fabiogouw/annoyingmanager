using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core.StateMachine;
using AnnoyingManager.WindowsTrayAlert;
using AnnoyingManager.WindowsTrayAlert.Reports;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Host
{
    public class IocNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaskRepository>().To<XmlRepository>();
            Bind<ITaskSupplier>().To<FormSysTray>();
            Bind<IConfigRepository>().To<XmlConfigRepository>();
            Bind<ReportControl>().To<TasksReportControl>();
            Bind<IManagerStateFactory>().To<ManagerStateFactory>();
            Bind<FormTask>().To<FormTask>().InSingletonScope();
            Bind<FormAbout>().To<FormAbout>().InSingletonScope();
            Bind<FormConfig>().To<FormConfig>().InSingletonScope();
        }
    }
}
