﻿using System;
using MicroServices.Platform.Repository.Core;
using RepositorioBase = Dominio.Nucleo.Repositorios.ConfiguracionFlujo;

namespace Dominio.Viaticos.Repositorios
{
    public interface IRepositorioAutorizacionViaticos: IRepository<Entidades.Autorizacion>, RepositorioBase.IRepositorioAutorizacionBase<Entidades.Autorizacion,Modelos.ConsultaSolicitudes>
    {
    }
}