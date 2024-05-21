using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VGList.DTO;
using VGList.Models;

namespace VGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly ILogger<VideoGameController> _logger; //logger

        //constructor
        public VideoGameController(ILogger<VideoGameController> logger)
        {
            _logger = logger; //get the logger from the DI container
        }

        //Get all video games present in the app
        //added new implementation using DTOs that complies with the HATEAOS constraint of RESTful APIs.
        [HttpGet(Name = "GetVideoGames")]
        public RestDTO<VideoGame[]> Get()
        {
            return new RestDTO<VideoGame[]>()
            {
                Data = new VideoGame[]
                {
                    new VideoGame
                    {
                        Id = 1,
                        Name = "Minecraft",
                        Year = 2011,
                        MinPlayers = 1,
                        MaxPlayers = 8
                    }
                },
                Links = new List<LinkDTO>
                {
                    new LinkDTO (
                        Url.Action(null, "Video Games", null, Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }
    }
}
