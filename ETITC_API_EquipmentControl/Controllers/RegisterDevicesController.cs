﻿using equipmentControl.Models;
using equipmentControl.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace equipmentControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDevicesController : Controller
    {
        private IRegisterDevicesColletion db = new RegisterDevicesColletion();

        [HttpGet]
        public async Task<IActionResult> GetAllRegisterDevices()
        {
            return Ok(await db.GetAllRegisterDevices());
        }


        [HttpPost]
        public async Task<IActionResult> CreateRegisterDevices([FromBody] RegisterDevices registerDevices)
        {
            if (registerDevices == null)
                return BadRequest();
            if (registerDevices.EntryDate != null )
                ModelState.AddModelError("Mark", "La marca del dispositivo no puede ser vacia");
            if (registerDevices.OutputDate == null)
                ModelState.AddModelError("Serial", "El serial del dispositivo no puede ser vacia");
            

            await db.CreateRegisterDevices(registerDevices);
            return Created("Registro creado", true);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadRegisterDevices(string Id)
        {
            return Ok(await db.ReadRegisterDevices(Id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegisterDevices([FromBody] RegisterDevices registerDevices, string Id)
        {
            /*if (device == null)
                return BadRequest();
            if (device.Mark == string.Empty)
                ModelState.AddModelError("Mark", "La marca del dispositivo no puede ser vacia");
            if (device.Serial == null)
                ModelState.AddModelError("Serial", "El serial del dispositivo no puede ser vacia");
            if (device.Img == null)
                ModelState.AddModelError("Img", "La imagen del dispositivo no puede ser vacia");
            if (device.Observation == null)
                ModelState.AddModelError("Observation", "La observacion del dispositivo no puede ser vacia");
            */

            registerDevices.Id = new MongoDB.Bson.ObjectId(Id);
            await db.UpdateRegisterDevices(registerDevices);
            return Created("Actulizaste el registro", true);
        }
/*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(string Id)
        {
            await db.DeleteDevice(Id);
            return NoContent();
        }*/


    }
}
