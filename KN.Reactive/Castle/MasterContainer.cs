using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KN.Reactive.ViewModel;

namespace KN.Reactive.Castle
{
    public class MasterContainer : WindsorContainer
    {
        public MasterContainer()
        {
            Install(new MainWindowInstaller());
        }
    }

    public class MainWindowInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainViewModel>(), Component.For<View.MainWindow>());
        }
    }
}