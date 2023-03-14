﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven.Repositories
{
    public interface IVehiculosPatrullajeRepo: IVehiculoPatrullajeCommand, IVehiculoPatrullajeQuery
    {
    }
}
