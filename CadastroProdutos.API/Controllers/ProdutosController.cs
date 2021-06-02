using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroProdutos.BLL;
using CadastroProdutos.DAL;

namespace CadastroProdutos.API.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly Contexto _context;

        public ProdutosController(Contexto context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Produtos.Include(p => p.usuario);
            return View(await contexto.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.Include(p => p.usuario).FirstOrDefaultAsync(m => m.produtoId == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["usuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("produtoId,nomedoProduto,valordeVenda,imagem,usuarioId")] Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["usuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", produtos.usuarioId);
            return View(produtos);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.FindAsync(id);
            if (produtos == null)
            {
                return NotFound();
            }
            ViewData["usuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", produtos.usuarioId);
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("produtoId,nomedoProduto,valordeVenda,imagem,usuarioId")] Produtos produtos)
        {
            if (id != produtos.produtoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutosExists(produtos.produtoId))
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
            ViewData["usuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", produtos.usuarioId);
            return View(produtos);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .Include(p => p.usuario)
                .FirstOrDefaultAsync(m => m.produtoId == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutosExists(int id)
        {
            return _context.Produtos.Any(e => e.produtoId == id);
        }
    }
}
