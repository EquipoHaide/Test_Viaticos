using System;
namespace Dominio.Nucleo
{
    public interface IFlujoModel<TPaso> : IFlujo<TPaso>, IModel
        where TPaso : IPaso
    {
    }
}
