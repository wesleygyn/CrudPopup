using CrudPopup.Data;
using CrudPopup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudPopup.Controllers
{
	public class PessoasController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _environment;

		public PessoasController(IWebHostEnvironment environment, ApplicationDbContext context)
		{
			_environment = environment;
			_context = context;
		}
		// GET: Pessoas
		public async Task<IActionResult> Index()
		{
			return View(await _context.pessoas.ToListAsync());
		}

		// GET: Pessoas/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.pessoas == null)
			{
				return NotFound();
			}

			var pessoa = await _context.pessoas
				.FirstOrDefaultAsync(m => m.Id == id);
			if (pessoa == null)
			{
				return NotFound();
			}

            return PartialView("_DetailsPessoa", pessoa);
        }

		// GET: Pessoas/Create
		public IActionResult Create()
		{
			return PartialView("_CreatePessoa");
		}

        // POST: Pessoas/Create
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Pessoa pessoa)
		{
			if (ModelState.IsValid)
			{
				_context.Add(pessoa);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(pessoa);
		}

		// GET: Pessoas/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.pessoas == null)
			{
				return NotFound();
			}

			var pessoa = await _context.pessoas.FindAsync(id);
			if (pessoa == null)
			{
				return NotFound();
			}
            return PartialView("_EditPessoa", pessoa);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Pessoa pessoa)
		{
			if (id != pessoa.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(pessoa);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PessoaExists(pessoa.Id))
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
			return View(pessoa);
		}

		// GET: Pessoas/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.pessoas == null)
			{
				return NotFound();
			}

			var pessoa = await _context.pessoas
				.FirstOrDefaultAsync(m => m.Id == id);
			if (pessoa == null)
			{
                return NotFound();
            }

            return PartialView("_DeletePessoa", pessoa);
        }

		// POST: Pessoas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.pessoas == null)
			{
				return Problem("Entity set 'ApplicationDbContext.pessoas'  is null.");
			}
			var pessoa = await _context.pessoas.FindAsync(id);
			if (pessoa != null)
			{
				_context.pessoas.Remove(pessoa);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PessoaExists(int id)
		{
			return _context.pessoas.Any(e => e.Id == id);
		}
	}
}