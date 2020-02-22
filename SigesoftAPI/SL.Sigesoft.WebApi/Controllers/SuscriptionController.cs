using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SL.Sigesoft.Common;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;
using WebPush;

namespace SL.Sigesoft.WebApi.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class SuscriptionController : ControllerBase
    {
        private ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public SuscriptionController(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            this._subscriptionRepository = subscriptionRepository;
            this._mapper = mapper;
        }


        [HttpGet("ObtenerClavePublica")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Response<string>> GetKeyPublic()
        {
            var response = new Response<string>();
            try
            {
                var keyPublic = _subscriptionRepository.GetKeyPublic();
                response.Data = keyPublic;
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> Post(SuscriptionRegisterDto suscribeDto)
        {
            var response = new Response<bool>();
            try
            {
                var suscription = _mapper.Map<Suscription>(suscribeDto);

                var newSuscription = await _subscriptionRepository.AddAsync(suscription);
                if (newSuscription == null)
                {
                    return BadRequest();
                }
                
                response.Data = true;
                response.IsSuccess = true;
                response.Message = "Suscripción correcta";
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("sendNotification")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> Push(SuscriptionPushDto suscriptionPushDto)
        {
            //Usuarios Fans del restaurante
            var suscriptions = await _subscriptionRepository.GetAllAsync();          

            foreach (var item in suscriptions)
            {
                var objNotification = JsonConvert.DeserializeObject<keySubscription>(item.v_Body);
                var pushEndpoint = objNotification.endpoint;
                var p256dh = objNotification.Keys.p256dh;
                var auth = objNotification.Keys.auth;

                var payload = JsonConvert.SerializeObject(suscriptionPushDto);
                var options = new Dictionary<string, object>();
                options["vapidDetails"] = new VapidDetails("mailto:beto1826@hotmail.com", Constants.KEY_PUBLIC, Constants.KEY_PRIVATE);

                var webPushClient = new WebPushClient();

                Thread t = new Thread((a) =>
                {
                    try
                    {
                        webPushClient.SendNotification((PushSubscription)a,
                            payload, options);
                    }
                    catch (WebPushException exception)
                    {
                        Console.WriteLine("Http STATUS code" + exception.StatusCode);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
                var s = new PushSubscription(pushEndpoint, p256dh, auth);
                t.Start(s);
            }
            return Ok();
        }
    }
}