using Autofac;
using Blog.API.Application.Behaviors;
using MediatR;
using System.Reflection;
namespace Blog.API.Infrastructure.Modules;

public class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(this.GetType().GetTypeInfo().Assembly)
           .Where(t => t.Name.EndsWith("Validator"))
           .AsImplementedInterfaces()
           .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    }
}
