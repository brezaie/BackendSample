using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTest.Api.Controllers;
using BackendTest.Domain;
using BackendTest.Domain.Dto;
using BackendTest.Domain.Entity;
using BackendTest.Repository;
using BackendTest.Repository.ClassDetailStudent;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
    public class ClassControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SaveMasterClass()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "SaveMasterClassDatabase")
                .Options;

            #region NullFilter

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(null);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NoClassname

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NoLocation

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NoTeachername

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region CorrectSave

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreEqual(res.Result.ClassName, toBeSavedEntity.Classname);
                Assert.AreEqual(res.Result.Location, toBeSavedEntity.Location);
                Assert.AreEqual(res.Result.TeacherName, toBeSavedEntity.TeacherName);
            }

            #endregion

        }

        [Test]
        public async Task GetMasterClass()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMasterClassDatabase")
                .Options;

            var savedEntity = new ClassMaster();

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedEntity = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #region InvalidId

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var rawJson = classController.GetMasterClass(1000);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region GetById

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var rawJson = classController.GetMasterClass(savedEntity.Id);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreNotEqual(res.Result, null);
                Assert.AreNotEqual(res.Result.Id, 0);

            }

            #endregion


            #region InvalidContext

            using (var context = new BackendTestDbContext(options))
            {
                var classController = new ClassController(null, null, null, null);

                var rawJson = classController.GetMasterClasses();
                var res = (WebApiPagedCollectionResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region GetAll

            using (var context = new BackendTestDbContext(options))
            {
                var _classMasterRepository = new ClassMasterRepository(context);
                var _classDetailRepository = new ClassDetailRepository(context);
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(_classMasterRepository, _classDetailRepository,
                    _studentRepository, _classDetailStudentRepository);

                var rawJson = classController.GetMasterClasses();
                var res = (WebApiPagedCollectionResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreNotEqual(res.Result, null);
                Assert.AreEqual(res.TotalRecords, 1);
            }

            #endregion

        }

        [Test]
        public async Task UpdateMasterClass()
        {
            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateMasterClassDatabase")
                .Options;

            var savedEntity = new ClassMaster();

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedEntity = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }


            #region InvalidFilter

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = await classController.UpdateMasterClass(null);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            var toBeUpdatedEntity = new ClassMasterDto();

            #region InvalidId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = await classController.UpdateMasterClass(toBeUpdatedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeUpdatedEntity = new ClassMasterDto
                {
                    Id = 50
                };

                var rawJson = await classController.UpdateMasterClass(toBeUpdatedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region UpdateCorrectly

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeUpdatedEntity = new ClassMasterDto
                {
                    Id = savedEntity.Id,
                    Location = "New Location",
                    TeacherName = savedEntity.TeacherName,
                    Classname = savedEntity.ClassName
                };

                var rawJson = await classController.UpdateMasterClass(toBeUpdatedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = classController.GetMasterClass(toBeUpdatedEntity.Id);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreEqual(res.Result.Classname, toBeUpdatedEntity.Classname);
            }

            #endregion
        }

        [Test]
        public async Task DeleteMasterClass()
        {
            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteMasterClassDatabase")
                .Options;

            var savedMasterClass = new ClassMaster();
            var savedDetailClass = new ClassDetail();

            #region SaveMasterClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedMasterClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region SaveDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassMasterId = savedMasterClass.Id
                };

                var rawJson = await classController.SaveDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;
                savedDetailClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region DeleteWithInvalidFilter

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = await classController.DeleteMasterClass(null);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            var toBeDeletedEntity = new ClassMasterDto();

            #region DeleteWithNoId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = await classController.DeleteMasterClass(toBeDeletedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region DeleteWhileHavingDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeDeletedEntity = new ClassMasterDto
                {
                    Id = savedMasterClass.Id
                };

                var rawJson = await classController.DeleteMasterClass(toBeDeletedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region DeleteDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeDeletedDetailClassEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id
                };

                var rawJson = await classController.DeleteDetailClass(toBeDeletedDetailClassEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region NiceDelete

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeDeletedEntity = new ClassMasterDto
                {
                    Id = savedMasterClass.Id
                };

                var rawJson = await classController.DeleteMasterClass(toBeDeletedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region InvalidId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeDeletedEntity = new ClassMasterDto
                {
                    Id = savedMasterClass.Id
                };

                var rawJson = await classController.DeleteMasterClass(toBeDeletedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

        }

        [Test]
        public async Task SaveDetailClass()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "SaveDetailClassDatabase")
                .Options;

            var savedMasterClass = new ClassMaster();

            #region SaveMasterClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedMasterClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region InvalidFilter

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);
                
                var rawJson = await classController.SaveDetailClass(null);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidMasterClassId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = await classController.SaveDetailClass(new ClassDetailDto {ClassDetailId = 50});
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region SaveDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassMasterId = savedMasterClass.Id
                };

                var rawJson = await classController.SaveDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreEqual(toBeSavedEntity.ClassMasterId, res.Result.ClassMasterId);
            }

            #endregion
        }

        [Test]
        public async Task GetDetailClass()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDetailClassDatabase")
                .Options;

            var savedMasterClass = new ClassMaster();
            var savedDetailClass = new ClassDetail();

            #region SaveMasterClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedMasterClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion


            #region InvalidMasterId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = classController.GetDetailClass(50);
                var res = (WebApiSimpleResponse<ClassDetailDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion


            #region NoDetailIsDefinedForMaster

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = classController.GetDetailClass(savedMasterClass.Id);
                var res = (WebApiSimpleResponse<ClassDetailDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region SaveDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassMasterId = savedMasterClass.Id,
                };

                var rawJson = await classController.SaveDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;
                savedDetailClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion


            var student = new Student();

            #region SaveStudent

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Gpa = (decimal)15.2,
                    Age = 20
                };

                var rawJsonRes1 = await studentController.SaveStudent(toBeSavedStudent);
                student = ((WebApiSimpleResponse<Student>)rawJsonRes1.Value).Result;
                Assert.IsFalse(((WebApiSimpleResponse<Student>)rawJsonRes1.Value).ErrorFlag);
            }

            #endregion

            #region AddStudentToDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var r = classController.GetDetailClass(1);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region GetDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = classController.GetDetailClass(savedMasterClass.Id);
                var res = (WebApiSimpleResponse<ClassDetailDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreEqual(savedDetailClass.Id, res.Result.ClassDetailId);
            }

            #endregion
        }

        [Test]
        public async Task DeleteDetailClass()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteDetailClassDatabase")
                .Options;

            var savedMasterClass = new ClassMaster();
            var savedDetailClass = new ClassDetail();

            #region SaveMasterClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedMasterClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region SaveDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassMasterId = savedMasterClass.Id
                };

                var rawJson = await classController.SaveDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;
                savedDetailClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            var toBeDeletedEntity = new ClassDetailDto();

            #region NullFilter

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = await classController.DeleteDetailClass(null);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidClassDetailId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeDeletedEntity = new ClassDetailDto();
                    
                var rawJson = await classController.DeleteDetailClass(toBeDeletedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            var student = new Student();

            #region SaveStudent

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Gpa = (decimal)15.2,
                    Age = 20
                };

                var rawJsonRes1 = await studentController.SaveStudent(toBeSavedStudent);
                student = ((WebApiSimpleResponse<Student>)rawJsonRes1.Value).Result;
                Assert.IsFalse(((WebApiSimpleResponse<Student>)rawJsonRes1.Value).ErrorFlag);
            }

            #endregion

            #region AddStudentToDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region DeleteDetailClassWhileHavingStudent

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeDeletedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id
                };

                var rawJson = await classController.DeleteDetailClass(toBeDeletedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion


            #region RemoveStudentFromDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student.Id
                        }
                    }
                };

                var rawJson = await classController.RemoveStudentsFromDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region NiceDeleteDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                toBeDeletedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id
                };

                var rawJson = await classController.DeleteDetailClass(toBeDeletedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion
        }

        [Test]
        public async Task AddStudentsToDetailClass()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "AddStudentsToDetailClassDatabase")
                .Options;

            var savedMasterClass = new ClassMaster();
            var savedDetailClass = new ClassDetail();

            #region SaveMasterClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedMasterClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region SaveDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassMasterId = savedMasterClass.Id
                };

                var rawJson = await classController.SaveDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;
                savedDetailClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            var toBeDeletedEntity = new ClassDetailDto();

            var student1 = new Student();
            var student2 = new Student();

            #region SaveStudents

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Gpa = (decimal)15.2,
                    Age = 20
                };

                var rawJsonRes1 = await studentController.SaveStudent(toBeSavedStudent);
                student1 = ((WebApiSimpleResponse<Student>)rawJsonRes1.Value).Result;
                Assert.IsFalse(((WebApiSimpleResponse<Student>)rawJsonRes1.Value).ErrorFlag);

                toBeSavedStudent = new StudentDto
                {
                    FirstName = "Jack",
                    LastName = "Smith",
                    Gpa = (decimal)18.1,
                    Age = 21
                };

                rawJsonRes1 = await studentController.SaveStudent(toBeSavedStudent);
                student2 = ((WebApiSimpleResponse<Student>)rawJsonRes1.Value).Result;
                Assert.IsFalse(((WebApiSimpleResponse<Student>)rawJsonRes1.Value).ErrorFlag);
            }

            #endregion

            #region NullFilter

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);
                
                var rawJson = await classController.AddStudentsToDetailClass(null);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;
                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidClassDetailId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = 50,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student1.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NoStudent

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = null
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidStudentId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = 50
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region DuplicateSurnames

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student1.Id
                        },
                        new StudentDto
                        {
                            Id = student2.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region AddStudentToDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student1.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region AddTheSameStudentToTheSameDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student1.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region AddTheStudentWithSimilarLastNameToTheSameDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student2.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion
        }

        [Test]
        public async Task RemoveStudentsToDetailClass()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveStudentsToDetailClassDatabase")
                .Options;

            var savedMasterClass = new ClassMaster();
            var savedDetailClass = new ClassDetail();

            #region SaveMasterClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassMasterDto
                {
                    Classname = "Computer Engineering",
                    Location = "Some where",
                    TeacherName = "Dr. Demberg"
                };

                var rawJson = await classController.SaveMasterClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMaster>)rawJson.Value;
                savedMasterClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region SaveDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassMasterId = savedMasterClass.Id
                };

                var rawJson = await classController.SaveDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetail>)rawJson.Value;
                savedDetailClass = res.Result;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            var student = new Student();

            #region SaveStudent

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Gpa = (decimal)15.2,
                    Age = 20
                };

                var rawJsonRes1 = await studentController.SaveStudent(toBeSavedStudent);
                student = ((WebApiSimpleResponse<Student>)rawJsonRes1.Value).Result;
                Assert.IsFalse(((WebApiSimpleResponse<Student>)rawJsonRes1.Value).ErrorFlag);
            }

            #endregion

            #region NullFilter

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var rawJson = await classController.RemoveStudentsFromDetailClass(null);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;
                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidClassDetailId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = 50,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student.Id
                        }
                    }
                };

                var rawJson = await classController.RemoveStudentsFromDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NoStudent

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = null
                };

                var rawJson = await classController.RemoveStudentsFromDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidStudentId

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = 50
                        }
                    }
                };

                var rawJson = await classController.RemoveStudentsFromDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NotRegisteredStudent

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student.Id
                        }
                    }
                };

                var rawJson = await classController.RemoveStudentsFromDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region AddStudentToDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student.Id
                        }
                    }
                };

                var rawJson = await classController.AddStudentsToDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassDetailStudent>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion

            #region RemoveStudentToDetailClass

            using (var context = new BackendTestDbContext(options))
            {
                var classMasterRepository = new ClassMasterRepository(context);
                var classDetailRepository = new ClassDetailRepository(context);
                var studentRepository = new StudentRepository(context);
                var classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var classController = new ClassController(classMasterRepository, classDetailRepository,
                    studentRepository, classDetailStudentRepository);

                var toBeSavedEntity = new ClassDetailDto
                {
                    ClassDetailId = savedDetailClass.Id,
                    StudentDtos = new List<StudentDto>
                    {
                        new StudentDto
                        {
                            Id = student.Id
                        }
                    }
                };

                var rawJson = await classController.RemoveStudentsFromDetailClass(toBeSavedEntity);
                var res = (WebApiSimpleResponse<ClassMasterDto>)rawJson.Value;

                Assert.IsFalse(res.ErrorFlag);
            }

            #endregion
        }

    }

}