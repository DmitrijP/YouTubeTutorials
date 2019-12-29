﻿using Microsoft.AspNetCore.Mvc;
using BasicClientServerApp.Server.Entities;
using BasicClientServerApp.Server.Entities.Employee;
using BasicClientServerApp.Server.Mappers;
using BasicClientServerApp.Server.Models.Employee;
using BasicClientServerApp.Server.Stores;
using System.Collections.Generic;
using System.Linq;

namespace BasicClientServerApp.Server.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeStore _employeeStore;
        private readonly EmployeeMapper _employeeMapper;

        public EmployeeController(EmployeeStore employeeStore, EmployeeMapper employeeMapper)
        {
            _employeeStore = employeeStore;
            _employeeMapper = employeeMapper;
        }

        [HttpGet]
        [Route("{action}")]
        public IEnumerable<EmployeeQueryModel> GetAll()
        {
            IEnumerable<EmployeeEntity> employeeEntityList = _employeeStore.GetAll();
            return _employeeMapper.Map(employeeEntityList);
        }

        [HttpGet]
        [Route("{action}/{userName}")]
        public IEnumerable<EmployeeQueryModel> Find(string userName)
        {
            IEnumerable<EmployeeEntity> employeeEntityList = _employeeStore.Find(userName);
            return _employeeMapper.Map(employeeEntityList);
        }

        [HttpGet]
        [Route("{action}/{id:int}")]
        public EmployeeQueryModel Find(int id)
        {
            EmployeeEntity entity = _employeeStore.Find(id);
            return _employeeMapper.Map(entity);
        }

        [HttpPost]
        [Route("{action}")]
        public EmployeeEntity Create(EmployeeCreationModel model)
        {
            EmployeeEntity entity = _employeeMapper.Map(model);
            return _employeeStore.Insert(entity);
        }

        [HttpDelete]
        [Route("{action}/{id:int}")]
        public object Delete(int id)
        {
            bool result = _employeeStore.Delete(id);
            return new
            {
                DeleteSuccessfull = result
            };
        }

    }
}