using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Гуртожиток
{
	public int Id { get; set; }
	public string Корпус { get; set; }
	public int Кімнати { get; set; }
	public int Студенти { get; set; }
	public int Оплата { get; set; }
	public string Умови { get; set; }
}

public class YourDbContext : DbContext
{
	public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
	{
	}

	public DbSet<Гуртожиток> Гуртожиток { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class YourController : ControllerBase
{
	private readonly YourDbContext _context;

	public YourController(YourDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Гуртожиток>>> GetГуртожитки()
	{
		return await _context.Гуртожиток.ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Гуртожиток>> GetГуртожиток(int id)
	{
		var гуртожиток = await _context.Гуртожиток.FindAsync(id);

		if (гуртожиток == null)
		{
			return NotFound();
		}

		return гуртожиток;
	}

	[HttpPost]
	public async Task<ActionResult<Гуртожиток>> PostГуртожиток(Гуртожиток гуртожиток)
	{
		_context.Гуртожиток.Add(гуртожиток);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetГуртожиток), new { id = гуртожиток.Id }, гуртожиток);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutГуртожиток(int id, Гуртожиток гуртожиток)
	{
		if (id != гуртожиток.Id)
		{
			return BadRequest();
		}

		_context.Entry(гуртожиток).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!ГуртожитокExists(id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteГуртожиток(int id)
	{
		var гуртожиток = await _context.Гуртожиток.FindAsync(id);

		if (гуртожиток == null)
		{
			return NotFound();
		}

		_context.Гуртожиток.Remove(гуртожиток);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool ГуртожитокExists(int id)
	{
		return _context.Гуртожиток.Any(e => e.Id == id);
	}
}

public class Program
{
	public static async Task Main()
	{
		// Your connection string
		string connectionString = "Data Source=(local);Initial Catalog=<назва_групи>_<прізвище_студента>;Integrated Security=True";

		// Create a host
		using (var host = new YourHost(connectionString))
		{
			// Start the host
			await host.StartAsync();
		}
	}
}

public class YourHost
{
	private readonly IWebHost _webHost;

	public YourHost(string connectionString)
	{
		_webHost = CreateWebHostBuilder(connectionString).Build();
	}

	public async Task StartAsync()
	{
		await _webHost.StartAsync();
	}

	private IWebHostBuilder CreateWebHostBuilder(string connectionString) =>
		WebHost.CreateDefaultBuilder()
			.ConfigureServices(services =>
			{
				services.AddDbContext<YourDbContext>(options =>
					options.UseSqlServer(connectionString));
				services.AddControllers();
			})
			.Configure(app =>
			{
				app.UseRouting();
				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				});
			});
}