using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum FiltroProgramaOpcion
    {
        Extraordinarios,        //PATRULLAJES EXTRAORDINARIOS REGISTRADOS y PATRULLAJES PROGRAMADOS (opciones para programar un patrullaje EXTRAORDINARIO y para notificar inicio de patrullaje PROGRAMADO)
        PatrullajesEnProceso,   //PATRULLAJES EN PROGRESO
        PatrullajesConcluidos,  //PATRULLAJES CONCLUIDOS
        PatrullajesCancelados,  //PATRULLAJES CANCELADOS
        PatrullajeTodos,        //TODOS LOS PATRULLAJES DEL PROGRAMA
        PropuestasPendientes,   //PROPUESTAS DE PATRULLAJE PENDIENTES DE AUTORIZACION POR LA SSF
        PropuestasAutorizadas,  //PROPUESTAS DE PATRULLAJE AUTORIZADAS POR LA SSF
        PropuestasRechazadas,   //PROPUESTAS DE PATRULLAJE RECHAZADAS POR LA SSF
        PropuestaTodas,         //TODAS LAS PROPUESTAS DE PATRULLAJE
        PropuestasEnviadas      //Propuestas enviadas a aprobación de la comandancia regional
    }
}