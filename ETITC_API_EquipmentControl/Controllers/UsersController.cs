﻿using equipmentControl.Models;
using equipmentControl.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography;

namespace equipmentControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IUsersColletion db = new UsersColletion();
        public static Users UsersDto = new Users();

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await db.GetAllUsers());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(InternalUsers internalUsers)
        {
            if (internalUsers == null)
                return BadRequest();
            if (internalUsers.Name == string.Empty)
                ModelState.AddModelError("Name", "El nombre del usuario no puede ser vacia");
            if (internalUsers.LastName == null)
                ModelState.AddModelError("LastName", "El apellido del usuario no puede ser vacia");
            if (internalUsers.Email == null)
                ModelState.AddModelError("Email", "El email del usuario no puede ser vacia");
            if (internalUsers.Password == null)
                ModelState.AddModelError("Password", "la contraseña del usuario no puede ser vacia");

            CreatePasswordHash(internalUsers.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UsersDto.IdentificationNumber = internalUsers.IdentificationNumber;
            UsersDto.PasswordHash = passwordHash;
            UsersDto.PasswordSalt = passwordSalt;
            UsersDto.Name = internalUsers.Name;
            UsersDto.LastName = internalUsers.LastName;
            UsersDto.Email = internalUsers.Email;
            UsersDto.Telephone = internalUsers.Telephone;
            UsersDto.EntryDate = DateTime.Now;
            UsersDto.status = true;

            await db.CreateUser(UsersDto);
            return Created("Usuario creado", true);
        }

        [HttpGet("{IdentificationNumber}")]
        public async Task<IActionResult> ReadUser(string identificationNumber)
        {
            return Ok(await db.ReadUser(identificationNumber));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] Users user, string id)
        {
            if (user == null)
                return BadRequest();
            if (user.Name == string.Empty)
                ModelState.AddModelError("Name", "El nombre del usuario no puede ser vacia");
            if (user.LastName == null)
                ModelState.AddModelError("LastName", "El apellido del usuario no puede ser vacia");
            if (user.Email == null)
                ModelState.AddModelError("email", "El email del usuario no puede ser vacia");

            user.Id = new MongoDB.Bson.ObjectId(id);
            await db.UpdateUser(user);
            return Created("Actulizado", true);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }

}
