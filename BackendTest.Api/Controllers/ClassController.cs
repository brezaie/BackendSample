using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.Common;
using BackendTest.Domain.Dto;
using BackendTest.Domain.Entity;
using BackendTest.Domain.Enum;
using BackendTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : CustomController
    {
        private readonly IClassMasterRepository _classMasterRepository;
        private readonly IClassDetailRepository _classDetailRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassDetailStudentRepository _classDetailStudentRepository;

        public ClassController(IClassMasterRepository classMasterRepository,
            IClassDetailRepository classDetailRepository,
            IStudentRepository studentRepository, IClassDetailStudentRepository classDetailStudentRepository)
        {
            _classMasterRepository = classMasterRepository;
            _classDetailRepository = classDetailRepository;
            _studentRepository = studentRepository;
            _classDetailStudentRepository = classDetailStudentRepository;
        }

        ///  <summary>
        ///  Get the list of master classes
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      GET
        ///      {
        ///      }
        ///  </remarks>
        [HttpGet("getMasterClasses")]
        public JsonResult GetMasterClasses()
        {
            WebApiPagedCollectionResponse<ClassMasterDto> res;

            try
            {
                var classMasterEntity = _classMasterRepository.GetAll().ToList();

                var resDto = classMasterEntity.Select(x => new ClassMasterDto
                {
                    Id = x.Id,
                    Classname = x.ClassName,
                    Location = x.Location,
                    TeacherName = x.TeacherName,
                }).ToList();

                res = new WebApiPagedCollectionResponse<ClassMasterDto>
                {
                    Result = resDto,
                    TotalRecords = resDto.Count,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiPagedCollectionResponse<ClassMasterDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Get the list of master classes
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      GET
        ///      {
        ///         "id": 1
        ///      }
        ///  </remarks>
        [HttpGet("getMasterClass")]
        public JsonResult GetMasterClass(int id)
        {
            WebApiSimpleResponse<ClassMasterDto> res;

            try
            {
                var classMasterEntity = _classMasterRepository.GetAll().FirstOrDefault(x => x.Id == id);
                if (classMasterEntity == null)
                {
                    var ex = new Exception("Class not found");
                    throw ex;
                }

                var resDto = new ClassMasterDto
                {
                    Id = classMasterEntity.Id,
                    Classname = classMasterEntity.ClassName,
                    Location = classMasterEntity.Location,
                    TeacherName = classMasterEntity.TeacherName,
                };

                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    Result = resDto,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Save master class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "className": "Some major",
        ///          "location": "some where",
        ///          "teacherName": "Dr. Somebody"
        ///      }
        ///  </remarks>
        [HttpPost("saveMasterClass")]
        public async Task<JsonResult> SaveMasterClass([FromBody] ClassMasterDto dto)
        {
            WebApiSimpleResponse<ClassMaster> res;

            try
            {
                if (dto == null)
                {
                    var ex = new Exception("Entity not found");
                    throw ex;
                }

                var entity = new ClassMaster
                {
                    ClassName = dto.Classname,
                    Location = dto.Location,
                    TeacherName = dto.TeacherName
                };

                var resultedEntity = await _classMasterRepository.Insert(entity);
                
                res = new WebApiSimpleResponse<ClassMaster>
                {
                    Result = resultedEntity,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassMaster>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Update master class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "id": 2,
        ///          "className": "Some major",
        ///          "location": "some where",
        ///          "teacherName": "Dr. Somebody"
        ///      }
        ///  </remarks>
        [HttpPost("updateMasterClass")]
        public async Task<JsonResult> UpdateMasterClass([FromBody] ClassMasterDto dto)
        {
            WebApiSimpleResponse<ClassMasterDto> res;

            try
            {
                if (dto == null)
                {
                    var ex = new Exception("Entity not found");
                    throw ex;
                }

                var entity = new ClassMaster
                {
                    Id = dto.Id,
                    ClassName = dto.Classname,
                    Location = dto.Location,
                    TeacherName = dto.TeacherName
                };

                await _classMasterRepository.Update(entity);

                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Delete master class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///         "id": 1
        ///      }
        ///  </remarks>
        [HttpPost("deleteMasterClass")]
        public async Task<JsonResult> DeleteMasterClass([FromBody] ClassMasterDto dto)
        {
            WebApiSimpleResponse<ClassMasterDto> res;

            try
            {
                if (dto == null)
                {
                    var ex = new Exception("Entity not found");
                    throw ex;
                }

                var entity = new ClassMaster
                {
                    Id = dto.Id
                };

                var doesHaveDetailClass = _classDetailRepository.GetAll().Where(x => x.ClassMasterId == dto.Id);
                if(doesHaveDetailClass.Count() > 0)
                    throw new Exception("The selected master class contains detail class(es)!");

                await _classMasterRepository.Delete(entity);

                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Get the the detail class of a specific master class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      GET
        ///      {
        ///         "masterClassId": 1
        ///      }
        ///  </remarks>
        [HttpGet("getDetailClass")]
        public JsonResult GetDetailClass(int masterClassId)
        {
            WebApiSimpleResponse<ClassDetailDto> res;

            try
            {
                var classMasterEntity = _classMasterRepository.GetAll().FirstOrDefault(x => x.Id == masterClassId);
                if (classMasterEntity == null)
                {
                    var ex = new Exception("Class not found");
                    throw ex;
                }

                var classDetailEntity = _classDetailRepository.GetAll().FirstOrDefault(x => x.ClassMasterId == masterClassId);
                if (classDetailEntity == null)
                {
                    var ex = new Exception("Class not found");
                    throw ex;
                }

                var classDetailStudentEntity = _classDetailStudentRepository.GetAll()
                    .Where(x => x.ClassDetailId == classDetailEntity.Id).Select(x => x.Student).ToList();


                var lst = new ClassDetailDto
                {
                    ClassDetailId = classDetailEntity.Id,
                    ClassName = classMasterEntity.ClassName,
                    ClassMasterId = classMasterEntity.Id,
                    StudentDtos = new List<StudentDto>()
                };

                if(classDetailStudentEntity.Count > 0)
                    foreach (var classDetailStudent in classDetailStudentEntity)
                    {
                        lst.StudentDtos.Add(new StudentDto
                        {
                            Id = classDetailStudent.Id,
                            LastName = classDetailStudent.LastName,
                            FirstName = classDetailStudent.FirstName,
                            Gpa = classDetailStudent.Gpa,
                            Age = classDetailStudent.Age
                        });
                    }

                res = new WebApiSimpleResponse<ClassDetailDto>
                {
                    Result = lst,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassDetailDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }

        ///  <summary>
        ///  Create a detail class for a single master class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "masterClassId": 1
        ///      }
        ///  </remarks>
        [HttpPost("saveDetailClass")]
        public async Task<JsonResult> SaveDetailClass([FromBody] ClassDetailDto dto)
        {
            WebApiSimpleResponse<ClassDetail> res;

            try
            {
                if (dto == null)
                    throw new Exception("Entity not found");

                var masterClassEntity = _classMasterRepository.GetAll().FirstOrDefault(x => x.Id == dto.ClassMasterId);
                if (masterClassEntity == null)
                    throw new Exception("Master class not found");
                
                var detailClassEntity = await _classDetailRepository.Insert(new ClassDetail
                {
                    ClassMasterId = dto.ClassMasterId,
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now
                });

                res = new WebApiSimpleResponse<ClassDetail>
                {
                    Result = detailClassEntity,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassDetail>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Delete detail class. It will be deleted if and only if there is no student enrolled in the class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "classDetailId": 1
        ///      }
        ///  </remarks>
        [HttpPost("deleteDetailClass")]
        public async Task<JsonResult> DeleteDetailClass([FromBody] ClassDetailDto dto)
        {
            WebApiSimpleResponse<ClassDetail> res;

            try
            {
                if (dto == null)
                    throw new Exception("Entity not found");

                var detailClassEntity = _classDetailRepository.GetAll().FirstOrDefault(x => x.Id == dto.ClassDetailId);
                if(detailClassEntity == null)
                    throw new Exception("Invalid detail class is selected");

                var studentsInClass = _classDetailStudentRepository.GetAll()
                    .Where(x => x.ClassDetailId == dto.ClassDetailId);
                if(studentsInClass.Count() > 0)
                    throw new Exception("The given class has some students");

                await _classDetailRepository.Delete(new ClassDetail
                {
                    Id = dto.ClassDetailId
                });

                res = new WebApiSimpleResponse<ClassDetail>
                {
                    Result = detailClassEntity,
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassDetail>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Add a number of students to a specific detail class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "classDetailId": 1,
        ///          "studentDtos": [
        ///              {
        ///                  "id": 2
        ///              }
        ///          ]
        ///      }
        ///  </remarks>
        [HttpPost("addStudentsToDetailClass")]
        public async Task<JsonResult> AddStudentsToDetailClass([FromBody] ClassDetailDto dto)
        {
            WebApiSimpleResponse<ClassDetailStudent> res;

            try
            {
                if (dto == null)
                    throw new Exception("Entity not found");

                if (dto.StudentDtos == null || dto.StudentDtos.Count <= 0)
                {
                    throw new Exception("No student is selcted!");
                }
                
                var detailClassEntity = _classDetailRepository.GetAll().FirstOrDefault(x => x.Id == dto.ClassDetailId);
                if (detailClassEntity == null)
                    throw new Exception("Detail class not found");

                var detailClassStudents = _classDetailStudentRepository.GetAll().Where(x => x.ClassDetailId == dto.ClassDetailId);

                var toBeSavedEntities = new List<ClassDetailStudent>();
                var newStudents = new List<Student>();
                foreach (var studentDto in dto.StudentDtos)
                {
                    var studentEntity = _studentRepository.GetAll().FirstOrDefault(x => x.Id == studentDto.Id);
                    if (studentEntity == null)
                        throw new Exception("Student not found");

                    var detailClassStudentEntity =
                        detailClassStudents.FirstOrDefault(x => x.StudentId == studentDto.Id);
                    if (detailClassStudentEntity != null)
                        throw new Exception("The given student is already registered in the given class");

                    var doesStudentWithSameSurnameExist =
                        detailClassStudents.Count(x => x.Student.LastName == studentEntity.LastName);
                    if(doesStudentWithSameSurnameExist > 0)
                        throw new Exception("Students with duplicate surnames in the same class!");

                    var toBeSavedDuplicateSurname =
                        newStudents.FirstOrDefault(x => x.LastName == studentEntity.LastName);
                    if(toBeSavedDuplicateSurname != null)
                        throw new Exception("Students with duplicate surnames in the same class!");

                    toBeSavedEntities.Add(new ClassDetailStudent
                    {
                        StudentId = studentDto.Id,
                        ClassDetailId = detailClassEntity.Id,
                        CreationDate = DateTime.Now,
                        ModificationDate = DateTime.Now
                    });

                    newStudents.Add(studentEntity);
                }

                foreach (var classDetailStudent in toBeSavedEntities)
                {
                    await _classDetailStudentRepository.Insert(classDetailStudent);
                }

                res = new WebApiSimpleResponse<ClassDetailStudent>
                {
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassDetailStudent>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


        ///  <summary>
        ///  Remove a number of students from a specific detail class
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        /// 
        ///      POST
        ///      {
        ///          "classDetailId": 1,
        ///          "studentDtos": [
        ///              {
        ///                  "id": 2
        ///              }
        ///          ]
        ///      }
        ///  </remarks>
        [HttpPost("removeStudentsFromDetailClass")]
        public async Task<JsonResult> RemoveStudentsFromDetailClass([FromBody] ClassDetailDto dto)
        {
            WebApiSimpleResponse<ClassMasterDto> res;

            try
            {
                if (dto == null)
                    throw new Exception("Entity not found");

                var detailClassEntity = _classDetailRepository.GetAll().FirstOrDefault(x => x.Id == dto.ClassDetailId);
                if (detailClassEntity == null)
                    throw new Exception("Detail class not found");

                if (dto.StudentDtos == null || dto.StudentDtos.Count <= 0)
                    throw new Exception("No student is selcted!");

                var toBeRemovedEntities = new List<ClassDetailStudent>();
                foreach (var studentDto in dto.StudentDtos)
                {
                    var studentEntity = _studentRepository.GetAll().FirstOrDefault(x => x.Id == studentDto.Id);
                    if (studentEntity == null)
                        throw new Exception("Student not found");

                    var detailClassStudentEntity = _classDetailStudentRepository.GetAll()
                        .FirstOrDefault(x => x.ClassDetailId == detailClassEntity.Id && x.StudentId == studentDto.Id);
                    if (detailClassStudentEntity == null)
                        throw new Exception("The given student is not registered in the given class");

                    toBeRemovedEntities.Add(detailClassStudentEntity);
                }

                foreach (var classDetailStudent in toBeRemovedEntities)
                {
                    await _classDetailStudentRepository.Delete(classDetailStudent);
                }

                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    Message = ResponseMessage.OperationSucceeded.GetDescription()
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                res = new WebApiSimpleResponse<ClassMasterDto>
                {
                    ErrorFlag = true,
                    Message = ResponseMessage.OperationFailed.GetDescription()
                };
            }

            return new JsonResult(res);
        }


    }
}