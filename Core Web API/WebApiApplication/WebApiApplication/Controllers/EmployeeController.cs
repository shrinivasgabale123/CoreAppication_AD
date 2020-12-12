using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.DTO;

namespace WebApiApplication.Controllers
{
    [ApiController]
    [Route("api")]
    public class EmployeeController : ControllerBase
    {

        [Route("SaveEmployeeData")]
        [HttpPost]
        public DTOEmployee SaveEmployeeData([FromBody] DTOEmployee dTOEmployee)
        {
            try 
            {
                Save(dTOEmployee);
                return dTOEmployee;
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public void Save(DTOEmployee dTOEmployee)
        {
            var curDir = Directory.GetCurrentDirectory();
            using (StreamWriter sw = new StreamWriter(curDir + "\\EmployeeData.csv",append:true))
            {
                string[] values = { dTOEmployee.Id.ToString(), dTOEmployee.FirstName, dTOEmployee.LastName, dTOEmployee.Password,
                dTOEmployee.ConfirmPassword,dTOEmployee.Email,dTOEmployee.Phone,dTOEmployee.SecurityQue,dTOEmployee.Answer,
                dTOEmployee.Gender};
                string line = String.Join(",", values);
                sw.WriteLine(line);
                sw.Flush();
            }
        }
    }
}
