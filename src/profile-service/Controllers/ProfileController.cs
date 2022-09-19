using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using profile_service.Core.Domain;
using profile_service.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace profile_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public ProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProfileDto model)
        {
            await this._unitOfWork.ProfileRepository.Save(Profile.CreateProfile(model.Name));
            await this._unitOfWork.SaveAsync();
            return Ok();
        }

    }

    public class ProfileDto
    {
        public string Name { get; set; }
    }
}

