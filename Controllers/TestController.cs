using Microsoft.AspNetCore.Mvc;
using testapi.Models;
using testapi.DBContext;
using Microsoft.EntityFrameworkCore.Diagnostics;
[ApiController]
[Route("/test")]
public class TestController : ControllerBase
{
    
    private static List<testModel> _testModel = new List<testModel>
    {
        new testModel {Id = Guid.NewGuid(), Name = "Test1"},
        new testModel {Id = Guid.NewGuid(), Name = "Test2"}
    };

    [HttpGet()]
    public IActionResult GetTestModel()
    {
        return Ok(_testModel);
    }

    [HttpPost]
    public IActionResult CreateTestModel([FromBody] testModel model)
    {
        model.Id = Guid.NewGuid();
        _testModel.Add(model);
        return CreatedAtAction(nameof(GetTestModel), new { id = model.Id }, model);
    }
    [HttpDelete]
    public IActionResult DeleteTestModel([FromQuery] Guid id)
    {
        var model = _testModel.FirstOrDefault(m => m.Id == id);
        if (model == null)
        {
            return NotFound();
        }
        _testModel.Remove(model);
        return NoContent();
    }   
}

[ApiController]
[Route("api/[controller]")]
public class ArtikelController : ControllerBase
{
    private readonly AppDbContext _db;

    public ArtikelController()
    {
        _db = new AppDbContext();
    }

    // GET api/artikel/expensive?preis=50
    [HttpGet("expensive")]
    public IActionResult GetTeureArtikel([FromQuery] decimal preis)
    {
        var teureArtikel = _db.Artikel
                              .Where(a => a.ArtikelPreis > preis)
                              .ToList();

        return Ok(teureArtikel); // gibt JSON zurück
    }

    // GET api/artikel/sell?id=123
    [HttpPost("sell")]
    public async Task<IActionResult> GetTeureArtikel([FromQuery] int id)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();
        var artikel = _db.Artikel
                              .Where(a => a.Id.Equals(id))
                              .FirstOrDefault();
        if (artikel == null)
        {
            return NotFound();
        }
        if (artikel.ArtikelBestand <= 0)
        {
            return BadRequest("Artikel nicht mehr auf Lager");
        }
        artikel.ArtikelBestand -=1;                 
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();
        return Ok(artikel); // gibt JSON zurück
    }

    [HttpPost("restock")]
    public  async Task<IActionResult> RestockArtikel([FromQuery] int id, [FromQuery] int menge)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();
        var artikel = _db.Artikel
                              .Where(a => a.Id.Equals(id))
                              .FirstOrDefault();          
        if (artikel == null)
        {
            return NotFound();
        }
        artikel.ArtikelBestand += menge;                 
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();
        return Ok(artikel); // gibt JSON zurück
    }

}



