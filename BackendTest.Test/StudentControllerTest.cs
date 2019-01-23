using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StudentControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public async Task SaveStudent()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "SaveStudentDatabase")
                .Options;

            #region NullFilter

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var savedStudent = await studentController.SaveStudent(null);
                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;
                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NullFirstName

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    //FirstName = "John",
                    LastName = "Smith",
                    Gpa = (decimal)15.2,
                    Age = 20
                };

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);

                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region NullLastName

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "John",
                    //LastName = "Smith",
                    Gpa = (decimal)15.2,
                    Age = 20
                };

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);

                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidAge

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
                    Age = 250
                };

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);

                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

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
                    Age = -25
                };

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);

                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region InvalidGPA

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Gpa = (decimal)23.5,
                    Age = 25
                };

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);
                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;
                Assert.IsTrue(res.ErrorFlag);
            }

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Gpa = (decimal)-15.2,
                    Age = 25
                };

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);
                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;
                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion

            #region CorrectSave

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeSavedStudent = new StudentDto
                {
                    FirstName = "Alex",
                    LastName = "Turing",
                    Gpa = (decimal)15.2,
                    Age = 20
                };

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);

                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreEqual(res.Result.FirstName, toBeSavedStudent.FirstName);
                Assert.AreEqual(res.Result.LastName, toBeSavedStudent.LastName);
                Assert.AreEqual(res.Result.Age, toBeSavedStudent.Age);
                Assert.AreEqual(res.Result.Gpa, toBeSavedStudent.Gpa);
                Assert.IsNotEmpty(res.Result.FullName);
            }

            #endregion
        }


        [Test]
        public async Task GetStudents()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "GetStudentDatabase")
                .Options;

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

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);
                Assert.IsFalse(((WebApiSimpleResponse<Student>)savedStudent.Value).ErrorFlag);

                toBeSavedStudent = new StudentDto
                {
                    FirstName = "Jenny",
                    LastName = "Ferra",
                    Gpa = (decimal)18.2,
                    Age = 23
                };

                savedStudent = await studentController.SaveStudent(toBeSavedStudent);
                Assert.IsFalse(((WebApiSimpleResponse<Student>)savedStudent.Value).ErrorFlag);
            }

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var allStudents = studentController.GetStudents();
                var res = (WebApiPagedCollectionResponse<StudentDto>)allStudents.Value;

                Assert.IsFalse(res.ErrorFlag);
                Assert.AreEqual(res.TotalRecords, 2);
            }

            #region NoContextIsGiven

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(null);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var allStudents = studentController.GetStudents();
                var res = (WebApiPagedCollectionResponse<StudentDto>)allStudents.Value;

                Assert.IsTrue(res.ErrorFlag);
            }

            #endregion
        }


        [Test]
        public async Task GetStudentById()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "GetStudentByIdDatabase")
                .Options;

            var studentId = 0;
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

                var savedStudent = await studentController.SaveStudent(toBeSavedStudent);
                var res = (WebApiSimpleResponse<Student>)savedStudent.Value;
                studentId = res.Result.Id;
                Assert.IsFalse(res.ErrorFlag);
                Assert.AreNotEqual(res.Result, null);
            }

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var studentById = studentController.GetStudentById(studentId);
                var getByIdRes = (WebApiSimpleResponse<StudentDto>)studentById.Value;
                Assert.IsFalse(getByIdRes.ErrorFlag);
                Assert.AreNotEqual(getByIdRes.Result, null);
                Assert.AreNotEqual(getByIdRes.Result.Id, 0);
            }
                
        }

        [Test]
        public async Task UpdateStudent()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteStudentDatabase")
                .Options;

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
                    FirstName = "Jenny",
                    LastName = "Ferra",
                    Gpa = (decimal)18.2,
                    Age = 23
                };

                var rawJsonRes2 = await studentController.SaveStudent(toBeSavedStudent);
                student2 = ((WebApiSimpleResponse<Student>)rawJsonRes2.Value).Result;
                Assert.IsFalse(((WebApiSimpleResponse<Student>)rawJsonRes2.Value).ErrorFlag);

                student2.LastName = "Smith";
                var toBeUpdatedStudent = new StudentDto
                {
                    LastName = student2.LastName,
                    FirstName = student2.FirstName,
                    Id = student2.Id,
                    Gpa = student2.Gpa,
                    Age = student2.Age
                };
                var rawJsonUpdate = await studentController.UpdateStudent(toBeUpdatedStudent);

                Assert.IsTrue(((WebApiSimpleResponse<Student>)rawJsonUpdate.Value).ErrorFlag);
            }

            #endregion


            #region NullFilter

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var rawJsonUpdate = await studentController.UpdateStudent(null);
                var updateStudentRes = (WebApiSimpleResponse<Student>)rawJsonUpdate.Value;

                Assert.IsTrue(updateStudentRes.ErrorFlag);
            }

            #endregion

            #region CorrectUpdate

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                student1.Age = 25;
                student1.Gpa = (decimal)17.3;

                var toBeUpdatedStudent = new StudentDto
                {
                    LastName = student1.LastName,
                    FirstName = student1.FirstName,
                    Id = student1.Id,
                    Gpa = student1.Gpa,
                    Age = student1.Age
                };
                var rawJsonUpdate = await studentController.UpdateStudent(toBeUpdatedStudent);
                var updateStudentRes = (WebApiSimpleResponse<Student>)rawJsonUpdate.Value;

                var rawJsonGetStudentById = studentController.GetStudentById(toBeUpdatedStudent.Id);
                var getStudentByIdRes = (WebApiSimpleResponse<StudentDto>)rawJsonGetStudentById.Value;

                Assert.IsFalse(updateStudentRes.ErrorFlag);
                Assert.AreEqual(toBeUpdatedStudent.Age, getStudentByIdRes.Result.Age);
                Assert.AreEqual(toBeUpdatedStudent.Gpa, getStudentByIdRes.Result.Gpa);
            }

            #endregion

            #region InvalidId

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                student1.Age = 25;
                student1.Gpa = (decimal)17.3;

                var toBeUpdatedStudent = new StudentDto
                {
                    LastName = student1.LastName,
                    FirstName = student1.FirstName,
                    Id = 50,
                    Gpa = student1.Gpa,
                    Age = student1.Age
                };
                var rawJsonUpdate = await studentController.UpdateStudent(toBeUpdatedStudent);
                var updateStudentRes = (WebApiSimpleResponse<Student>)rawJsonUpdate.Value;

                var rawJsonGetStudentById = studentController.GetStudentById(toBeUpdatedStudent.Id);
                var getStudentByIdRes = (WebApiSimpleResponse<StudentDto>)rawJsonGetStudentById.Value;

                Assert.IsTrue(updateStudentRes.ErrorFlag);
            }

            #endregion

        }

        [Test]
        public async Task DeleteStudent()
        {

            var options = new DbContextOptionsBuilder<BackendTestDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateStudentDatabase")
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

            #region DeleteWithNullValue

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);
                
                var rawJsonDelete = await studentController.DeleteStudent(null);
                var deleteStudentRes = (WebApiSimpleResponse<Student>)rawJsonDelete.Value;
                Assert.IsTrue(deleteStudentRes.ErrorFlag);
            }

            #endregion

            #region DeleteAStudentEnrolledInAClass

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeDeletedStudent = new StudentDto
                {
                    Id = student.Id,
                };

                var rawJsonDelete = await studentController.DeleteStudent(toBeDeletedStudent);
                var deleteStudentRes = (WebApiSimpleResponse<Student>)rawJsonDelete.Value;
                Assert.IsTrue(deleteStudentRes.ErrorFlag);
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

            #region NiceStudentDelete

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeDeletedStudent = new StudentDto
                {
                    Id = student.Id,
                };

                var rawJsonDelete = await studentController.DeleteStudent(toBeDeletedStudent);
                var deleteStudentRes = (WebApiSimpleResponse<Student>)rawJsonDelete.Value;
                Assert.IsFalse(deleteStudentRes.ErrorFlag);

                var rawJsonGetStudentById = studentController.GetStudentById(toBeDeletedStudent.Id);
                var getStudentByIdRes = (WebApiSimpleResponse<StudentDto>)rawJsonGetStudentById.Value;

                Assert.IsTrue(getStudentByIdRes.ErrorFlag);
            }

            #endregion

            #region InvalidId

            using (var context = new BackendTestDbContext(options))
            {
                var _studentRepository = new StudentRepository(context);
                var _classDetailStudentRepository = new ClassDetailStudentRepository(context);
                var studentController = new StudentController(_studentRepository, _classDetailStudentRepository);

                var toBeDeletedStudent = new StudentDto
                {
                    Id = student.Id,
                };

                var rawJsonDelete = await studentController.DeleteStudent(toBeDeletedStudent);
                var deleteStudentRes = (WebApiSimpleResponse<Student>)rawJsonDelete.Value;
                Assert.IsTrue(deleteStudentRes.ErrorFlag);
            }

            #endregion

        }

    }
}