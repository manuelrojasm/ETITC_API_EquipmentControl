﻿using equipmentControl.Models;
using equipmentControl.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace equipmentControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : Controller
    {
        private IDevicesColletion db = new DevicesColletion();

        [HttpGet]
        public async Task<IActionResult> GetAllDevices()
        {
            return Ok(await db.GetAllDevices());
        }


        [HttpPost]
        public async Task<IActionResult> CreateDevices([FromBody] Devices devices)
        {
            if (devices == null)
                return BadRequest();
            if (devices.Mark == string.Empty)
                ModelState.AddModelError("Mark", "La marca del dispositivo no puede ser vacia");
            if (devices.Serial == null)
                ModelState.AddModelError("Serial", "El serial del dispositivo no puede ser vacia");
            if (devices.Img == null)
                ModelState.AddModelError("Img", "La imagen del dispositivo no puede ser vacia");
            if (devices.Observation == null)
                ModelState.AddModelError("Observation", "La observacion del dispositivo no puede ser vacia");

            await db.CreateDevices(devices);
            return Created("Usuario creado", true);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadDevices(string Id)
        {
            return Ok(await db.ReadDevices(Id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevices([FromBody] Devices device, string Id)
        {
            if (device == null)
                return BadRequest();
            if (device.Mark == string.Empty)
                ModelState.AddModelError("Mark", "La marca del dispositivo no puede ser vacia");
            if (device.Serial == null)
                ModelState.AddModelError("Serial", "El serial del dispositivo no puede ser vacia");
            if (device.Img == null)
                ModelState.AddModelError("Img", "La imagen del dispositivo no puede ser vacia");
            if (device.Observation == null)
                ModelState.AddModelError("Observation", "La observacion del dispositivo no puede ser vacia");


            device.Id = new MongoDB.Bson.ObjectId(Id);
            await db.UpdateDevices(device);
            return Created("Actulizaste el dispositivo", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(string Id)
        {
            await db.DeleteDevice(Id);
            return NoContent();
        }


    }
}
