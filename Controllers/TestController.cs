using Microsoft.AspNetCore.Mvc;
using testapi.Models;

public class TestCrontoller : ControllerBase
{
    
    private static List<testModel> _testModel = new List<testModel>
    {
        new testModel {Id = 1, Name = "Test1"},
        new testModel {Id = 2, Name = "Test2"}
    };

    [HttpGet("/test")]
    public IActionResult GetTestModel()
    {
        return Ok(_testModel);
    }
}


