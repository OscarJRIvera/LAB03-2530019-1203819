using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB3.Data;
using LAB3.Models;
using DoubleLinkedListLibrary1;
using Microsoft.AspNetCore.Http;
using System.Text;

using System.IO;
using System.Diagnostics;

namespace LAB3.Controllers
{
    public class FarmacoesController : Controller
    {
        public DoubleLinkedList<Farmaco> List2;//mi lista aqui 

        //base de datos

        //private readonly LAB2_2530019_1203819Context _context;
        private readonly Models.Data.Singleton F = Models.Data.Singleton.Instance;

        public FarmacoesController(LAB3Context context)
        {
            //_context = context;
        }

        // GET: Farmacoes
        public IActionResult Index()
        {
            //return View(F.Arbol_Farmacos.ConvertirLista());
            if (F == null)
                return RedirectToAction("Index", "Home");
            return View(F.List2);
        }

        // GET: Farmacoes/Details/5
        public IActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var farmaco = await _context.Farmaco
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (farmaco == null)
            //{
            //    return NotFound();
            //}

            //return View(farmaco);
            return RedirectToAction(nameof(Index));
        }

        // GET: Farmacoes/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Search(string nombreFarmaco)
        {
            LlaveArbol llave = F.Arbol_Farmacos.Find(new LlaveArbol { Nombre_Farmaco = nombreFarmaco });
            if (llave == null) return View("ErrorBusqueda");
            var farmacoEncontrado = F.List2.GetbyIndex(llave.Fila);
            var lis = new List<Farmaco>(1);// solo para que la lisa tenga una posicion como un arreglo
            lis.Add(farmacoEncontrado);
            return View("Index", lis);
        }

        // POST: Farmacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre_Farmaco,Cantidad_Disponible,Precio_Unitario")] Farmaco farmaco)
        {
            if (ModelState.IsValid)
            {
                F.List2.Add(farmaco);
                var numFila = F.List2.Count() - 1;
                var LlaveArbol = new LlaveArbol
                {
                    Nombre_Farmaco = farmaco.Nombre_Farmaco,
                    Fila = numFila
                };
                F.Arbol_Farmacos.Add(LlaveArbol);
                return RedirectToAction(nameof(Index));
            }
            return View(farmaco);
        }


        // GET: Farmacoes/Edit/5
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var farmaco = await _context.Farmaco.FindAsync(id);
            //if (farmaco == null)
            //{
            //    return NotFound();
            //}
            //return View(farmaco);
            return RedirectToAction(nameof(Index));
        }

        // POST: Farmacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre_Farmaco,Cantidad_Disponible,Precio_Unitario")] Farmaco farmaco)
        {
            //if (id != farmaco.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(farmaco);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!FarmacoExists(farmaco.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(farmaco);
            return RedirectToAction(nameof(Index));
        }

        // GET: Farmacoes/Delete/5
        public IActionResult Delete(string? nombre)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var farmaco = await _context.Farmaco
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (farmaco == null)
            //{
            //    return NotFound();
            //}

            //return View(farmaco);

            return RedirectToAction(nameof(Index));
        }

        // POST: Farmacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //var farmaco = await _context.Farmaco.FindAsync(id);
            //_context.Farmaco.Remove(farmaco);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }

        private bool FarmacoExists(int id)
        {
            //return _context.Farmaco.Any(e => e.Id == id);
            return true;
        }
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {

            int i = F.List2.Count();

            if (file == null || file.Length == 0) return Content("file not selected");
            byte[] byts = new byte[file.Length];
            using (var strm = file.OpenReadStream())
            {
                await strm.ReadAsync(byts, 0, byts.Length);
            }
            string cnt = Encoding.UTF8.GetString(byts);
            string[] lines = cnt.Split('\n');

            foreach (string line in lines)
            {
                if (line == "id,nombre,descripcion,casa_productora,precio,existencia\r") { }


                else if (line == "") { }
                else
                {
                    string id;
                    string nombre;
                    string descripcion;
                    string casa;
                    string precio;
                    string existencia;
                    id = line.Substring(0, line.IndexOf(","));
                    int id2;
                    id2 = Convert.ToInt32(id) - F.eliminados;
                    string nuevoline = line.Substring(line.IndexOf(","), line.Length - line.IndexOf(","));
                    string n1;
                    if (nuevoline.Substring(1, 1) == "\"")
                    {
                        string[] part = nuevoline.Split("\"");
                        nombre = part[1];
                        n1 = nuevoline.Substring(2, nuevoline.Length - 2);
                        nuevoline = n1.Substring(n1.IndexOf("\"") + 1, n1.Length - n1.IndexOf("\"") - 1);
                    }
                    else
                    {
                        string[] part = nuevoline.Split(",");
                        nombre = part[1];
                        n1 = nuevoline.Substring(1, nuevoline.Length - 1);
                        nuevoline = n1.Substring(n1.IndexOf(","), n1.Length - n1.IndexOf(","));
                    }

                    if (nuevoline.Substring(1, 1) == "\"")
                    {
                        string[] part = nuevoline.Split("\"");
                        descripcion = part[1];
                        n1 = nuevoline.Substring(2, nuevoline.Length - 2);
                        nuevoline = n1.Substring(n1.IndexOf("\"") + 1, n1.Length - n1.IndexOf("\"") - 1);
                    }
                    else
                    {
                        string[] part = nuevoline.Split(",");
                        descripcion = part[1];
                        n1 = nuevoline.Substring(1, nuevoline.Length - 1);
                        nuevoline = n1.Substring(n1.IndexOf(","), n1.Length - n1.IndexOf(","));
                    }
                    if (nuevoline.Substring(1, 1) == "\"")
                    {
                        string[] part = nuevoline.Split('"');
                        casa = part[1];
                        n1 = nuevoline.Substring(2, nuevoline.Length - 2);
                        nuevoline = n1.Substring(n1.IndexOf("\"") + 1, n1.Length - n1.IndexOf("\"") - 1);
                    }
                    else
                    {
                        string[] part = nuevoline.Split(",");
                        casa = part[1];
                        n1 = nuevoline.Substring(1, nuevoline.Length - 1);
                        nuevoline = n1.Substring(n1.IndexOf(","), n1.Length - n1.IndexOf(","));
                    }
                    string[] part2 = nuevoline.Split(",");
                    precio = part2[1];
                    n1 = nuevoline.Substring(1, nuevoline.Length - 1);
                    nuevoline = n1.Substring(n1.IndexOf(","), n1.Length - n1.IndexOf(",") - 1);
                    string[] part3 = nuevoline.Split(",");
                    existencia = part3[1];

                    Farmaco nuevo = new Farmaco();
                    nuevo.Id = id2;
                    nuevo.Nombre_Farmaco = nombre;
                    nuevo.Descripción_Farmaco = descripcion;
                    nuevo.Casa_Productora = casa;
                    nuevo.Precio = Convert.ToDouble(precio.Substring(1, precio.Length - 1));
                    nuevo.Existencia = Convert.ToInt32(existencia);


                    F.List2.Add(nuevo);
                    var numFila = F.List2.Count();
                    var numfila2 = F.List2.Count2();
                    var LlaveArbol = new LlaveArbol
                    {
                        Nombre_Farmaco = nuevo.Nombre_Farmaco,
                        Fila = numfila2
                    };
                    try
                    {
                        F.Arbol_Farmacos.Add(LlaveArbol);
                    }
                    catch
                    {
                        F.List2.RemoveAt(numFila);
                        F.eliminados += 1;
                    }

                    i++;
                }
            }


            return RedirectToAction(nameof(Index));
        }
        //GET
        public IActionResult AgregarPedido(int? Id)
        {
            F.id = Convert.ToInt32(Id);
            Farmaco nuevo = new Farmaco();
            nuevo = F.List2.Find(m => m.Id == Id);
            F.nombre = nuevo.Nombre_Farmaco;

            if (F.Nit == 0)
            {
                return RedirectToAction("AgregarPedido2", "Farmacoes");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AgregarPedido(int id, [Bind("Id,Cantidad")] PedidosFarmacos ProductModel)
        {
            Farmaco nuevo = new Farmaco();
            nuevo = F.List2.Find(m => m.Id == ProductModel.Id);
            if (nuevo.Existencia < ProductModel.Cantidad)
            {
                return RedirectToAction("ErrorCantidad", "Farmacoes");
            }
            ProductModel.Nombre = F.nombre;
            ProductModel.Nit = F.Nit;
            ProductModel.NombreDelCliente = F.Nombrecliente;
            ProductModel.Dirrecion = F.dirrecion;
            ProductModel.Precio = nuevo.Precio;
            F.Pedidos.Add(ProductModel);

            F.List2.Find(m => m.Id == ProductModel.Id).Existencia = nuevo.Existencia - ProductModel.Cantidad;
            if (nuevo.Existencia == 0)
            {
                var llave = F.Arbol_Farmacos.Find(new LlaveArbol { Nombre_Farmaco = nuevo.Nombre_Farmaco });
                F.Arbol_Farmacos.RemoveAt(llave);

            }

            return RedirectToAction("Index", "PedidosFarmacos");
        }
        //GET
        public IActionResult AgregarPedido2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarPedido2(int id, [Bind("Id,NombreDelCliente,Dirrecion,Nit,Cantidad")] PedidosFarmacos ProductModel)
        {
            ProductModel.Id = F.id;
            Farmaco nuevo = new Farmaco();
            nuevo = F.List2.Find(m => m.Id == ProductModel.Id);
            if (nuevo.Existencia < ProductModel.Cantidad)
            {
                return RedirectToAction("ErrorCantidad", "Farmacoes");
            }

            ProductModel.Nombre = F.nombre;
            F.Nit = ProductModel.Nit;
            F.Nombrecliente = ProductModel.NombreDelCliente;
            F.dirrecion = ProductModel.Dirrecion;
            ProductModel.Precio = nuevo.Precio;
            F.Pedidos.Add(ProductModel);

            F.List2.Find(m => m.Id == ProductModel.Id).Existencia = nuevo.Existencia - ProductModel.Cantidad;
            if (nuevo.Existencia == 0)
            {
                var llave = F.Arbol_Farmacos.Find(new LlaveArbol { Nombre_Farmaco = nuevo.Nombre_Farmaco });
                F.Arbol_Farmacos.RemoveAt(llave);

            }
            return RedirectToAction("Index", "PedidosFarmacos");
        }
        public IActionResult ErrorCantidad()
        {
            return View();
        }
        public IActionResult ErrorBusqueda()
        {
            return View();
        }

        public IActionResult Compra_Finalizada()
        {
            F.Nit = 0;
            F.dirrecion = "";
            F.Nombrecliente = "";
            F.Pedidos.eleiminartodo();
            var llave = new LlaveArbol();
            for (int i = 1; i <= F.List2.Count() - F.List2.EleCount(); i++)
            {
                var ProductModel = F.List2.GetbyIndex(i);
                llave.Fila = i;
                llave.Nombre_Farmaco = ProductModel.Nombre_Farmaco;
                if (ProductModel.Existencia == 0)
                {
                    F.List2.Find(m => m.Id == ProductModel.Id).Existencia = F.random.Next(1, 15);
                    F.Arbol_Farmacos.Add(llave);
                }
            }
            return View();

        }
    }
}