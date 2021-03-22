using System;
using System.Collections.Generic;

namespace ProyectoHotelNT1.Models
{
    public partial class Huespedes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int NroHabitacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }
        public double DiasHospedado { get; set; }
    }
}
