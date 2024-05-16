using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet(Name = "GetVideoGames")]
        public IEnumerable<VideoGame> Get()
        {
            return new[]
            {
                new VideoGame
                {
                    Id = 1,
                    Name = "Minecraft",
                    Year = 2011,
                    MinPlayers = 1,
                    MaxPlayers = 16
                },
                new VideoGame
                {
                    Id = 2,
                    Name = "Stardew Valley",
                    Year = 2016,
                    MinPlayers = 1,
                    MaxPlayers = 8
                },
                new VideoGame
                {
                    Id = 3,
                    Name = "Apex Legends",
                    Year = 2019,
                    MinPlayers = 1,
                    MaxPlayers = 3
                }
            };
        }
    }
}
