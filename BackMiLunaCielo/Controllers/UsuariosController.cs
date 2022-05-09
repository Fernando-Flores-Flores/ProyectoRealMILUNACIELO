#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackMiLunaCielo.Data;
using BackMiLunaCielo.Models;
using BackMiLunaCielo.Repository.IRepository;
using BackMiLunaCielo.Models.Dtos;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BackMiLunaCielo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;



        public UsuariosController(IUsuarioRepository userRepo, IMapper mapper, IConfiguration config)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<Categoria>> GetUsuarios()
        {
            var listaUsuarios = _userRepo.GetUsuarios();
            var listaUsuariosDto = new List<UsuarioDto>();
            foreach (var lista in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }
            return Ok(listaUsuariosDto);
        }

        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario = _userRepo.GetUsuario(usuarioId);
            if (itemUsuario == null)
            {
                return NotFound();
            }
            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Registro(UsuarioAuthDto usuarioAuthDto)
        {
            usuarioAuthDto.Usuario = usuarioAuthDto.Usuario.ToLower();
            if (_userRepo.ExisteUsuario(usuarioAuthDto.Usuario))
            { 
                return BadRequest("El usuario ya existe");
            }
            var usuarioCrear = new Usuario
            {
                UsuarioA = usuarioAuthDto.Usuario
            };
            var usuarioCreado = _userRepo.Registro(usuarioCrear, usuarioAuthDto.Password);
            return Ok(usuarioCreado);            
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioAuthLoginDto usuarioAuthLoginDto)
        {
            var usuarioDesdeRepo = _userRepo.Login(usuarioAuthLoginDto.Usuario, usuarioAuthLoginDto.Password);
            if (usuarioDesdeRepo==null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,usuarioDesdeRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,usuarioDesdeRepo.UsuarioA.ToString())
            };
            //GENERACION DEL TOKEN
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credenciales = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(1),
                SigningCredentials= credenciales


            };
             var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            { 
                token= tokenHandler.WriteToken(token)
            });
        }

        //[HttpPost("Login")]
        //public IActionResult Login(UsuarioAuthLoginDto usuarioAuthLoginDto)
        //{
        //    //throw new Exception("Error generado");

        //    var usuarioDesdeRepo = _userRepo.Login(usuarioAuthLoginDto.Usuario, usuarioAuthLoginDto.Password);

        //    if (usuarioDesdeRepo == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var claims = new[]
        //    {
        //    new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.Id.ToString()),
        //    new Claim(ClaimTypes.Name, usuarioDesdeRepo.UsuarioA.ToString())
        //};

        //    //Generación de token
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        //    var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.Now.AddDays(1),
        //        SigningCredentials = credenciales
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    return Ok(new
        //    {
        //        token = tokenHandler.WriteToken(token)
        //    });
        //}
    }
}
