using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven.Repositories
{
    public interface ITarjetaInformativaRepo: ITarjetaInformativaCommand, ITarjetaInformativaQuery
    {
    }
}
