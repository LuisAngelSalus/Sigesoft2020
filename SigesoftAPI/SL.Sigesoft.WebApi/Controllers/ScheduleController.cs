﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.WebApi.Controllers
{
    [Authorize(Roles = "Administrador,Recepción,Sistemas,Cliente")]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            this._scheduleRepository = scheduleRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> Post(List<ScheduleRegisterDto> listDtos)
        {
            try
            {
                var response = new Response<bool>();

                var schedules = _mapper.Map<List<Schedule>>(listDtos);
                var result = await _scheduleRepository.DoSchedule(schedules);

                response.Data = result;
                response.IsSuccess = result;
                response.Message = "Se grabó correctamente";

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

    }
}