using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BackendTest.Domain;
using BackendTest.Domain.Entity;
using BackendTest.Domain.Enum;
using BackendTest.Repository;
using BackendTest.Repository.ClassDetailStudent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace BackendTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<BackendTestDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BackendTestConnectionString")));


            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IClassMasterRepository, ClassMasterRepository>();
            services.AddTransient<IClassDetailRepository, ClassDetailRepository>();
            services.AddTransient<IClassDetailStudentRepository, ClassDetailStudentRepository>();

            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Backend Test API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("BackendTestConnectionString"));
            loggerFactory.AddNLog();

            app.UseCors("AllowAllOrigins");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend Test API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            //PopulateDatabase();
        }

        private void PopulateDatabase()
        {
            //using (var context = new BackendTestDbContext())
            //{
            //    context.Database.EnsureCreated();

            //    #region Student

            //    var student = context.Student.FirstOrDefault(x => x.FirstName == "David" && x.LastName == "Jackson");
            //    if (student == null)
            //    {
            //        context.Student.Add(new Student
            //        {
            //            FirstName = "David",
            //            LastName = "Jackson",
            //            Age = 21,
            //            Gpa = (decimal) 3.24,
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        });
            //    }

            //    student = context.Student.FirstOrDefault(x => x.FirstName == "Peter" && x.LastName == "Parker");
            //    if (student == null)
            //    {
            //        context.Student.Add(new Student
            //        {
            //            FirstName = "Peter",
            //            LastName = "Parker",
            //            Age = 20,
            //            Gpa = (decimal) 2.46,
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        });
            //    }

            //    student = context.Student.FirstOrDefault(x => x.FirstName == "Robert" && x.LastName == "Smith");
            //    if (student == null)
            //    {
            //        context.Student.Add(new Student
            //        {
            //            FirstName = "Robert",
            //            LastName = "Smith",
            //            Age = 22,
            //            Gpa = (decimal) 2.3,
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        });
            //    }

            //    context.SaveChanges();

            //    #endregion

            //    #region ClassMaster

            //    var classMaster = context.ClassMaster.FirstOrDefault(x =>
            //        x.ClassName == "Computer - Software Engineering" && x.TeacherName == "Sanderson");
            //    if (classMaster == null)
            //    {
            //        context.ClassMaster.Add(new ClassMaster
            //        {
            //            ClassName = "Computer - Software Engineering",
            //            Location = "Building 3 Room 321",
            //            TeacherName = "Mr. Sanderson",
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        });
            //    }

            //    classMaster = context.ClassMaster.FirstOrDefault(x =>
            //        x.ClassName == "Computer - Artificial Intelligence" && x.TeacherName == "Sanderson");
            //    if (classMaster == null)
            //    {
            //        context.ClassMaster.Add(new ClassMaster
            //        {
            //            ClassName = "Computer - Artificial Intelligence",
            //            Location = "Building 3 Room 321",
            //            TeacherName = "Mr. Sanderson",
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        });
            //    }

            //    context.SaveChanges();

            //    #endregion

            //    #region ClassDetail

            //    var aiClassMaster =
            //        context.ClassMaster.FirstOrDefault(x => x.ClassName == "Computer - Artificial Intelligence" && x.TeacherName == "Mr. Sanderson");

            //    var classDetail = context.ClassDetail.FirstOrDefault(x => x.ClassMasterId == aiClassMaster.Id);
            //    if (classDetail == null)
            //    {
            //        context.ClassDetail.Add(new ClassDetail
            //        {
            //            ClassMaster = aiClassMaster,
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        });
            //    }

            //    context.SaveChanges();

            //    #endregion

            //    #region ClassDetailStudent

            //    var robertSmithStudent =
            //        context.Student.FirstOrDefault(x => x.FirstName == "Robert" && x.LastName == "Smith");

            //    var peterParkerStudent =
            //        context.Student.FirstOrDefault(x => x.FirstName == "Peter" && x.LastName == "Parker");
                
            //    var aiClassDetail = context.ClassDetail.FirstOrDefault(x => x.ClassMasterId == aiClassMaster.Id);

            //    context.ClassDetailStudent.AddRange(new List<ClassDetailStudent>
            //    {
            //        new ClassDetailStudent
            //        {
            //            ClassDetail = aiClassDetail,
            //            Student = robertSmithStudent,
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        },
            //        new ClassDetailStudent
            //        {
            //            ClassDetail = aiClassDetail,
            //            Student = peterParkerStudent,
            //            CreationDate = DateTime.Now,
            //            ModificationDate = DateTime.Now
            //        }
            //    });

            //    context.SaveChanges();

            //    #endregion

            //}
        }
    }
}
