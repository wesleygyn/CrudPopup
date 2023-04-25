using CrudPopup.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrudPopup.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Pessoa> pessoas { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}