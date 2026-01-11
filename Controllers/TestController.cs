using Microsoft.AspNetCore.Mvc;
using testapi.Models;
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
}


