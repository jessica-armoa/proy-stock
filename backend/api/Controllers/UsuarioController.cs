using api.Models;
using api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Usuario;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [ApiController]
  [Route("api/usuario")]
  public class UsuariosController : ControllerBase
  {
    private readonly UserManager<Usuarios> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<Usuarios> _signInManager;

    public UsuariosController(UserManager<Usuarios> userManager, ITokenService tokenService, SignInManager<Usuarios> signInManager)
    {
      _userManager = userManager;
      _tokenService = tokenService;
      _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto){
      if(!ModelState.IsValid){
        return BadRequest(ModelState);
      }
      var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

      if(user == null) return Unauthorized("Username invalido");

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if(!result.Succeeded) return Unauthorized("Usuario y/o contraseña incorrectos");

      return Ok(
        new NuevoUsuarioDto{
          UserName = user.UserName,
          Email = user.Email,
          Token = _tokenService.CreateToken(user)
        }
      );
    }

    [HttpPost("register")]
    public async Task<ActionResult> CrearUsuario([FromBody] CreateUsuarioDto CreateUsuarioDto)
    {
      try{
        if(!ModelState.IsValid)
        return BadRequest(ModelState);

        var usuario = new Usuarios
        {
          UserName = CreateUsuarioDto.UserName,
          Email = CreateUsuarioDto.Email,
        };

        var result = await _userManager.CreateAsync(usuario, CreateUsuarioDto.Password);

        if (result.Succeeded)
        {
          var roleResult = await _userManager.AddToRoleAsync(usuario, "User");
          if (roleResult.Succeeded)
          {
            return Ok(
              new NuevoUsuarioDto{
                UserName = usuario.UserName,
                Email = usuario.Email,
                Token = _tokenService.CreateToken(usuario)
              }
            );
          }
          else
          {
            return StatusCode(500, roleResult.Errors);
          }
        }
        else
        {
          return StatusCode(500, result.Errors);
        }

      } catch (Exception e){
        return StatusCode(500, e);
      }

    }
  }
}
