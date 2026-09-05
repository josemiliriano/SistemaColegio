using Application.Usuario;
using Application.Usuario.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]    
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userAppService.GetAllUser();

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userAppService.GetUserById(id);

            if (user == null)
            {
                return NotFound(new
                {
                    mensaje = "El usuario no existe."
                });
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newUser = await _userAppService.AddUser(user);

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedUser = await _userAppService.UpdateUser(id, user);

                if (updatedUser == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El usuario no existe."
                    });
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new {mensaje = ex.Message});
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedUser = await _userAppService.DeleteUser(id);

                if (deletedUser == null)
                {
                    return NotFound(new{mensaje = "El usuario no existe."});
                }

                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/User/5/password
        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto passwordDto)
        {
            try
            {
                var result = await _userAppService.ChangePassword(
                    id,
                    passwordDto);

                if (!result)
                {
                    return BadRequest(new
                    {
                        mensaje = "La contraseña actual es incorrecta o el usuario no está activo."
                    });
                }

                return Ok(new
                {
                    mensaje = "La contraseña fue cambiada correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }
    }
}