using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoHotelNT1.Models;

namespace ProyectoHotelNT1.Controllers
{
    public class HuespedesController : Controller
    {
        private readonly OceanHillDBContext _context;

        public HuespedesController(OceanHillDBContext context)
        {
            _context = context;
        }

        // GET: Huespedes
        public async Task<IActionResult> Index()
        {
            ViewBag.totales = TotalHuespedes();
            if (_context.Huespedes.Count() > 0) {
                AgregarIdActivo();
            }
            return View(await _context.Huespedes.ToListAsync());
        }

        // GET: Huespedes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespedes = await _context.Huespedes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (huespedes == null)
            {
                return NotFound();
            }

            return View(huespedes);
        }

        // GET: Huespedes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Huespedes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,NroHabitacion,FechaIngreso,FechaEgreso")] Huespedes huespedes)
        {
            double dif = CalcularDiasHospedado(huespedes.FechaEgreso, huespedes.FechaIngreso);

            if (ModelState.IsValid && HabitacionDisponible(huespedes.NroHabitacion) && StringValidado(huespedes.Nombre) && StringValidado(huespedes.Apellido) && dif >= 0)
            {
                huespedes.DiasHospedado = dif;
                _context.Add(huespedes);
                _context.Registro.Add(CrearRegistro(huespedes.Nombre, huespedes.Apellido, huespedes.Dni, huespedes.NroHabitacion, huespedes.FechaIngreso, huespedes.FechaEgreso, dif));
                MarcarHabitacionOcupada(huespedes.NroHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            else if (!HabitacionDisponible(huespedes.NroHabitacion))
            {
                ViewBag.Validacion = "alert('Habitacion inexistente o llena');";
                return View(huespedes);
            }

            else if (!StringValidado(huespedes.Nombre) || !StringValidado(huespedes.Apellido))              
            {
                ViewBag.Validacion = "alert('El nombre o apellido excede 20 caracteres');";
                return View(huespedes);
            }

            else if (dif<0)
            {
                ViewBag.Validacion = "alert('La fecha de ingreso no puede ser mayor a la de salida');";
                return View(huespedes);
            }

            return View(huespedes);
        }
  

        // GET: Huespedes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespedes = await _context.Huespedes.FindAsync(id);
            if (huespedes == null)
            {
                return NotFound();
            }
            return View(huespedes);
        }

        // POST: Huespedes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Dni,NroHabitacion,FechaIngreso,FechaEgreso,DiasHospedado")] Huespedes huespedes)
        {
            if (id != huespedes.Id)
            {
                return NotFound();
            }

            double dif = CalcularDiasHospedado(huespedes.FechaEgreso, huespedes.FechaIngreso);

            if (ModelState.IsValid && HabitacionDisponible(huespedes.NroHabitacion) && StringValidado(huespedes.Nombre) && StringValidado(huespedes.Apellido) && dif >= 0)
            {

                int idActivo = huespedes.Id;
                huespedes.DiasHospedado = dif;
                ActualizarRegistro(id, huespedes.Nombre, huespedes.Apellido, huespedes.Dni, huespedes.NroHabitacion, huespedes.FechaIngreso, huespedes.FechaEgreso, dif);
                _context.Update(huespedes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            else if (!HabitacionDisponible(huespedes.NroHabitacion))
            {
                ViewBag.Validacion = "alert('Habitacion inexistente o llena');";
                return View(huespedes);
            }

            else if (!StringValidado(huespedes.Nombre) || !StringValidado(huespedes.Apellido))
            {
                ViewBag.Validacion = "alert('El nombre o apellido es invalido');";
                return View(huespedes);
            }

            else if (dif < 0)
            {
                ViewBag.Validacion = "alert('La fecha de ingreso no puede ser mayor a la de salida');";
                return View(huespedes);
            }

            return View(huespedes);
        }

        // GET: Huespedes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespedes = await _context.Huespedes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (huespedes == null)
            {
                return NotFound();
            }

            return View(huespedes);
        }

        // POST: Huespedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var huespedes = await _context.Huespedes.FindAsync(id);
            _context.Huespedes.Remove(huespedes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HuespedesExists(int id)
        {
            return _context.Huespedes.Any(e => e.Id == id);
        }

        private bool HabitacionExists(int nro)
        {
            return _context.Habitaciones.Any(e => e.NroHabitacion == nro);
        }


        private Habitaciones buscarHabitacion(int nro)
        {
            Habitaciones habitacion = (from s in _context.Habitaciones where s.NroHabitacion == nro select s).FirstOrDefault<Habitaciones>();
            return habitacion;
        }

        private bool HabitacionOcupada(int nro)
        {
            return buscarHabitacion(nro).EstaOcupada;

        }

        private bool HabitacionDisponible(int nro)
        {
            return HabitacionExists(nro) && !HabitacionLLena(nro);
        }

        private bool HabitacionLLena(int nro)
        {
            return CantHuespedesEnHabitacion(nro) + 1 > buscarHabitacion(nro).CapacidadMax;
        }


        private int CantHuespedesEnHabitacion(int nro)
        {
            return _context.Huespedes.Count(e => e.NroHabitacion == nro);
        }

        private void MarcarHabitacionOcupada(int nro)
        {
            Habitaciones habitacion = buscarHabitacion(nro);
            habitacion.EstaOcupada = true;
            _context.Habitaciones.Update(habitacion);
         
        }

        private double CalcularDiasHospedado(DateTime egreso,DateTime ingreso)
        {
            return (egreso - ingreso).TotalDays;
        }

        private Registro CrearRegistro(String nombre, String apellido, int dni, int nrohabitacion, DateTime ingreso, DateTime egreso, double diashospedados)
        {
            Registro registro = new Registro();
            registro.Nombre = nombre;
            registro.Apellido = apellido;
            registro.Dni = dni;
            registro.NroHabitacion = nrohabitacion;
            registro.FechaIngreso = ingreso;
            registro.FechaEgreso = egreso;
            registro.DiasHospedado = diashospedados;

            return registro;
        }   

        private Huespedes BuscarHuespedPorId(int id)
        {
            return _context.Huespedes.Find(id);
        }

        private Registro BuscarRegistro(int id)
        {
            Registro registro1 = null;

            var x =  (from s in _context.Registro where s.IdActivo == id select s).ToList();

            foreach (Registro registro in x)
            {
                registro1 = registro;
            }

            return registro1;

        }          
      
        private void ActualizarRegistro(int id, String Nombre, String Apellido,int Dni, int NroHabitacion, DateTime FechaIngreso, DateTime FechaEgreso, double DiasHospedado)
        {

            Registro registro = BuscarRegistro(id);
          
            registro.Nombre = Nombre;
            registro.Apellido = Apellido;
            registro.Dni = Dni;
            registro.NroHabitacion = NroHabitacion;
            registro.FechaIngreso = FechaIngreso;
            registro.FechaEgreso = FechaEgreso;
            registro.DiasHospedado = DiasHospedado;

            _context.Remove(registro);
            _context.Registro.Update(registro);
            _context.SaveChanges();


        }

        private void AgregarIdActivo()
        {
            int id = _context.Huespedes.Last().Id;
            Registro registro = _context.Registro.Last();
            registro.IdActivo = id;
            _context.Registro.Update(registro);
            _context.SaveChanges();

        }

        private bool StringValidado(String palabra)
        {
            int maximo = 20;
            if (palabra != null)
            {
                return palabra.Length <= maximo;
            }
            else
            {
                return false;
            }
           
        }

        private int TotalHuespedes()
        {
            return _context.Huespedes.Count();
        }






    }
}
