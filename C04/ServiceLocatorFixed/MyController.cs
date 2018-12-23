﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocatorFixed
{
    public class MyController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public MyController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        [Route("/")]
        public IActionResult Get()
        {
            using (var myService = _serviceProvider.GetService<IMyService>())
            {
                myService.Execute();
                return Ok();
            }
        }
    }

    public class MethodInjectionController : ControllerBase
    {
        [Route("/method-injection")]
        public IActionResult GetUsingMethodInjection([FromServices]IMyService myService)
        {
            if (myService == null) { throw new ArgumentNullException(nameof(myService)); }
            myService.Execute();
            return Ok();
        }
    }

    public class ConstructorInjectionController : ControllerBase
    {
        private readonly IMyService _myService;

        public ConstructorInjectionController(IMyService myService)
        {
            _myService = myService ?? throw new ArgumentNullException(nameof(myService));
        }

        [Route("/constructor-injection")]
        public IActionResult GetUsingConstructorInjection()
        {
            _myService.Execute();
            return Ok();
        }
    }
}
