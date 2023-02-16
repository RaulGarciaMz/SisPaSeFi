using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum FiltroProgramaOpcion
    {
        ExtraordinariosyProgramados,        //PATRULLAJES EXTRAORDINARIOS REGISTRADOS y PATRULLAJES PROGRAMADOS (opciones para programar un patrullaje EXTRAORDINARIO y para notificar inicio de patrullaje PROGRAMADO)
        PatrullajesEnProgreso,   //PATRULLAJES EN PROGRESO
        PatrullajesConcluidos,  //PATRULLAJES CONCLUIDOS
        PatrullajesCancelados,  //PATRULLAJES CANCELADOS
        PatrullajeTodos,        //TODOS LOS PATRULLAJES DEL PROGRAMA
        PropuestaTodas,         //TODAS LAS PROPUESTAS DE PATRULLAJE
        PropuestasPendientes,   //PROPUESTAS DE PATRULLAJE PENDIENTES DE AUTORIZACION POR LA SSF
        PropuestasAutorizadas,  //PROPUESTAS DE PATRULLAJE AUTORIZADAS POR LA SSF
        PropuestasRechazadas,   //PROPUESTAS DE PATRULLAJE RECHAZADAS POR LA SSF        
        PropuestasEnviadas      //Propuestas enviadas a aprobación de la comandancia regional
    }
}