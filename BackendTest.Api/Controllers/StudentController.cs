using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.Common;
using BackendTest.Domain.Dto;
using BackendTest.Domain.Entity;
using BackendTest.Domain.Enum;
using BackendTest.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassDetailStudentRepository _classDetailStudentRepository;
        public CustomLogger Logger = new CustomLogger();

        public StudentController(IStudentRepository studentRepository,
            IClassDetailStudentRepository classDetailStudentRepository)
        {
            _studentRepository = studentRepository;
            _classDetailStudentRepository = classDetailStudentRepository;
        }

        ///  <summary>
        ///  Get the list of students
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      GET
        ///      {
        ///      }
        ///  </remarks>
        [HttpGet("getStudents")]
        public JsonResult GetStudents()
        {
            WebApiPagedCollectionResponse<StudentDto> res;

            try
            {
                var studentsEntity = _studentRepository.GetAll();

                var lst = studentsEntity.Select(x => new StudentDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    Gpa = x.Gpa
                }).ToList();

                res = new WebApiPagedCollectionResponse<StudentDto>
                {
                    Result = lst,
                    TotalRecords = lst.Count,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiPagedCollectionResponse<StudentDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Get the a specific student by its Id
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      GET
        ///      {
        ///         id: 3
        ///      }
        ///  </remarks>
        [HttpGet("getStudentById")]
        public JsonResult GetStudentById(int id)
        {
            WebApiSimpleResponse<StudentDto> res;

            try
            {
                var studentEntity = _studentRepository.GetAll().FirstOrDefault(x => x.Id == id);
                if(studentEntity == null)
                    throw new Exception("Student not found");

                var dto = new StudentDto
                {
                    Id = studentEntity.Id,
                    FirstName = studentEntity.FirstName,
                    LastName = studentEntity.LastName,
                    Age = studentEntity.Age,
                    Gpa = studentEntity.Gpa
                };

                res = new WebApiSimpleResponse<StudentDto>
                {
                    Result = dto,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<StudentDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Save a single student
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "firstName": "Albert",
        ///          "lastName": "Smith",
        ///          "age": 20,
        ///          "gpa": 18.7
        ///      }
        ///  </remarks>
        [HttpPost("saveStudent")]
        public async Task<JsonResult> SaveStudent([FromBody] StudentDto dto)
        {
            WebApiSimpleResponse<Student> res;

            try
            {
                if (dto == null)
                    throw new Exception("Entity not found");

                var studentEntity = await _studentRepository.Insert(new Student
                {
                    Age = dto.Age,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Gpa = dto.Gpa,
                });

                res = new WebApiSimpleResponse<Student>
                {
                    Result = studentEntity,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<Student>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Update a single student by its Id
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "id": 2,
        ///          "firstName": "Albert",
        ///          "lastName": "Smith",
        ///          "age": 20,
        ///          "gpa": 18.7
        ///      }
        ///  </remarks>
        [HttpPost("updateStudent")]
        public async Task<JsonResult> UpdateStudent([FromBody] StudentDto dto)
        {
            WebApiSimpleResponse<Student> res;

            try
            {
                if (dto == null)
                    throw new Exception("Entity not found");

                await _studentRepository.Update(new Student
                {
                    Id = dto.Id,
                    Age = dto.Age,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Gpa = dto.Gpa,
                });

                res = new WebApiSimpleResponse<Student>
                {
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<Student>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }

        ///  <summary>
        ///  Delete a single student by its Id. The student is removed if and only if s/he is not enrolled in any class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "id": 3
        ///      }
        ///  </remarks>
        [HttpPost("deleteStudent")]
        public async Task<JsonResult> DeleteStudent([FromBody] StudentDto dto)
        {
            WebApiSimpleResponse<Student> res;

            try
            {
                if (dto == null)
                    throw new Exception("Entity not found");

                var isEnrolledInAnyClass = _classDetailStudentRepository.GetAll().Where(x => x.StudentId == dto.Id);
                if(isEnrolledInAnyClass.Count() > 0)
                    throw new Exception("The student is already enrolled in (a) class(es)!");

                await _studentRepository.Delete(new Student
                {
                    Id = dto.Id
                });

                res = new WebApiSimpleResponse<Student>
                {
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<Student>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }

    }
}