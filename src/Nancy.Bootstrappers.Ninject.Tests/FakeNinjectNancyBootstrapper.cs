﻿namespace Nancy.Bootstrappers.Ninject.Tests
{
    using Bootstrapper;
    using global::Ninject;

    /// <summary>
    /// Fake Ninject boostrapper that can be used for testing.
    /// </summary>
    public class FakeNinjectNancyBootstrapper : NinjectNancyBootstrapper
    {
        public bool RequestContainerConfigured { get; set; }
        public bool ApplicationContainerConfigured { get; set; }
        private readonly NancyInternalConfiguration configuration;

        public FakeNinjectNancyBootstrapper()
            : this(null)
        {
        }

        public FakeNinjectNancyBootstrapper(NancyInternalConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return configuration ?? base.InternalConfiguration; }
        }

        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            this.ApplicationContainerConfigured = true;
        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Bind(typeof(IFoo)).To(typeof(Foo)).InSingletonScope();
            container.Bind(typeof(IDependency)).To(typeof(Dependency)).InSingletonScope();

            this.RequestContainerConfigured = true;
        }

        public T Resolve<T>()
        {
            return this.ApplicationContainer.Get<T>();
        }
    }
}