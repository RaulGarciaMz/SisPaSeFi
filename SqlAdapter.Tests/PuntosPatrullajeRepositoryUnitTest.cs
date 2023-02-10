using SqlServerAdapter.Data;
using SqlServerAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace SqlAdapter.Tests
{
    public class PuntosPatrullajeRepositoryUnitTest
    {
        [Fact]
        public void Agrega_ReturnsOk()
        {
            var pc = new PuntoPatrullajeRepository(new PuntoPatrullajeContext());

            var p = new PuntoPatrullaje() 
            {
                
            };

            pc.Agrega(p);
        }
    }
}
