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
    public class HabitacionesController : Controller
    {
        private readonly OceanHillDBContext _context;

        public HabitacionesController(OceanHillDBContext context)
        {
            _context = context;
        }

        // GET: Habitaciones
        public async Task<IActionResult> Index()
        {
            ViewBag.totales = TotalHabitaciones();
            ViewBag.disponibles = HabitacionesDisponibles();
            ViewBag.ocupadas = HabitacionesOcupadas();
            ChequeoDeHabitaciones();
            return View(await _context.Habitaciones.ToListAsync());
        }

        // GET: Habitaciones/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var habitaciones = await _context.Habitaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitaciones == null)
            {
                return NotFound();
            }

            if (habitaciones.EstaOcupada) {
                ViewBag.Message = TotalPagarPorHabitacion(id);
            }
            else
            {
                ViewBag.Message = "-";
            }



            return View(habitaciones);
        }

        // GET: Habitaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habitaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NroHabitacion,PrecioPorDia,CapacidadMax")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid && !HabitacionExists(habitaciones.NroHabitacion) && PrecioValido(habitaciones.PrecioPorDia) && CapacidadValida(habitaciones.CapacidadMax))
            {
                habitaciones.EstaOcupada = false;
                _context.Add(habitaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (HabitacionExists(habitaciones.NroHabitacion))
            {
                ViewBag.habitacionExiste = "alert('No se puede crear: La habitacion ya existe');";
                return View(habitaciones);

            }
            else if (!PrecioValido(habitaciones.PrecioPorDia)) {
                ViewBag.habitacionExiste = "alert('Precio por dia invalido');";
                return View(habitaciones);
            }

            else
            {
                ViewBag.habitacionExiste = "alert('Capacidad max. invalida');";
                return View(habitaciones);
            }


        }

        // GET: Habitaciones/Edit/5

        public async Task<IActionResult> Edit(int id)
        {
            var habitaciones = await _context.Habitaciones.FindAsync(id);
            if (habitaciones.EstaOcupada || habitaciones == null)
            {
                return RedirectToAction(nameof(Alerta));
            }
            else
            {
                return View(habitaciones);
            }

        }

        public IActionResult Alerta()
        {
            ViewBag.miCodigoScript = "alert('No puede editar o eliminar habitaciones ocupadas');";
            return View();
        }

        // POST: Habitaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NroHabitacion,PrecioPorDia,CapacidadMax")] Habitaciones habitaciones)
        {
            if (id != habitaciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid &&  PrecioValido(habitaciones.PrecioPorDia) && CapacidadValida(habitaciones.CapacidadMax))
            {
                try
                {
                    habitaciones.EstaOcupada = false;
                    _context.Update(habitaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionesExists(habitaciones.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
            else if (!PrecioValido(habitaciones.PrecioPorDia))
            {
                ViewBag.habitacionExiste = "alert('Precio por dia invalido');";
                return View(habitaciones);
            }

            else
            {
                ViewBag.habitacionExiste = "alert('Capacidad max. invalida');";
                return View(habitaciones);
            }

        }

        // GET: Habitaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitaciones = await _context.Habitaciones.FirstOrDefaultAsync(m => m.Id == id);
            if (habitaciones.EstaOcupada || habitaciones == null)
            {
                return RedirectToAction(nameof(Alerta));
            }
            else
            {
                return View(habitaciones);
            }
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitaciones = await _context.Habitaciones.FindAsync(id);
            _context.Habitaciones.Remove(habitaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Liberar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitaciones = await _context.Habitaciones.FindAsync(id);
            if (habitaciones == null)
            {
                return NotFound();
            }

            return View(habitaciones);

        }

        [HttpPost, ActionName("Liberar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LiberarConfirmar(int id)
        {
            var habitaciones = await _context.Habitaciones.FindAsync(id);
            habitaciones.EstaOcupada = false;
            EliminarHuespedesPorHabitacion(id);
            _context.Habitaciones.Update(habitaciones);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> HuespedesEnHabitacion(int id)
        {

            var habitaciones = await _context.Habitaciones.FindAsync(id);
            if (habitaciones == null)
            {
                return NotFound();
            }
            return View(HuespedPorHabitacion(id));

        }

        public async Task<IActionResult> Registro(int id)
        {

            var habitaciones = await _context.Habitaciones.FindAsync(id);
            if (habitaciones == null)
            {
                return NotFound();
            }
            return View(HuespedPorHabitacionRegistro(id));

        }

        //verifica si exsite por ID
        private bool HabitacionesExists(int id)
        {
            return _context.Habitaciones.Any(e => e.Id == id);
        }
        //verifica si exsite por Nro de habitacion
        private bool HabitacionExists(int nro)
        {
            return _context.Habitaciones.Any(e => e.NroHabitacion == nro);
        }    

        private void EliminarHuespedesPorHabitacion(int id)
        {

            int nroHabitacion = _context.Habitaciones.Find(id).NroHabitacion;

            var huespedes = (from s in _context.Huespedes where s.NroHabitacion == nroHabitacion select s).ToList();

            foreach (Huespedes huesped in huespedes)
            {
                _context.Huespedes.Remove(huesped);
                _context.SaveChanges();
            }
        }

        private List<Huespedes> HuespedPorHabitacion(int id)
        {
            int nroHabitacion = _context.Habitaciones.Find(id).NroHabitacion;

            var huespedes = (from s in _context.Huespedes where s.NroHabitacion == nroHabitacion select s).ToList();

            return huespedes;
        }

        private List<Registro> HuespedPorHabitacionRegistro(int id)
        {
            int nroHabitacion = _context.Habitaciones.Find(id).NroHabitacion;

            var huespedes = (from s in _context.Registro where s.NroHabitacion == nroHabitacion select s).ToList();

            return huespedes;
        }

        private void ChequeoDeHabitaciones()
        {
            foreach (Habitaciones habitacion in _context.Habitaciones)
            {
                int cant = HuespedPorHabitacion(habitacion.Id).Count;

                if (habitacion.EstaOcupada && cant == 0)
                {
                    habitacion.EstaOcupada = false;
                    _context.SaveChanges();
                }

                else if(!habitacion.EstaOcupada && cant > 0)
                {
                    habitacion.EstaOcupada = true;
                    _context.SaveChanges();
                }
            }
        }

        private double TotalPagarPorHabitacion (int id)
        {
            double MaxDias = 0;

            double precioPorDia = _context.Habitaciones.Find(id).PrecioPorDia;

            //busca al que se queda mas dias en la habitacion y calcula cuanto pagar en base a esos dias.
            var huespedes = HuespedPorHabitacion(id);

            foreach(Huespedes h in huespedes)
            {
                if (h.DiasHospedado > MaxDias) { 
                MaxDias = h.DiasHospedado;
                }

            }

           return MaxDias * precioPorDia;
        }

        private bool PrecioValido(double precio)
        {
            return precio >= 0;
        }

        private bool CapacidadValida(int capacidad)
        {
            return capacidad > 0;
        }

        private int TotalHabitaciones() {
            return _context.Habitaciones.Count();
        }

        private int HabitacionesDisponibles()
        {
            int cant = 0;

            var habitaciones = (from s in _context.Habitaciones select s).ToList();

            foreach (Habitaciones h in habitaciones)
            {
                if (!h.EstaOcupada)
                {
                    cant++;
                }
            }

            return cant;
        }

        private int HabitacionesOcupadas()
        {
            int cant = 0;

            var habitaciones = (from s in _context.Habitaciones select s).ToList();

            foreach (Habitaciones h in habitaciones)
            {
                if (h.EstaOcupada)
                {
                    cant++;
                }
            }

            return cant;
        }

        

    }


}
